using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QRisto.Application.Configuration;
using QRisto.Application.Models.Request.User;
using QRisto.Application.Models.Response.User;
using QRisto.Application.Services.Token;
using QRisto.Application.Utils;
using QRisto.Persistence;
using QRisto.Persistence.Entity;

namespace QRisto.Application.Services.User;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly JwtOptions _jwtOptions;
    private readonly IMapper _mapper;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(
        ITokenService tokenService,
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        JwtOptions jwtOptions,
        RoleManager<ApplicationRole> roleManager,
        ApplicationDbContext dbContext)
    {
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(loginRequestModel.UserName);

            if (user is null)
            {
                return Result<LoginResponseModel>.Failure(
                    UserErrors.NotFound,
                    $"User with user name {loginRequestModel.UserName} not found.");
            }

            var passwordVerificationResult = _userManager
                .PasswordHasher
                .VerifyHashedPassword(
                    user,
                    user.PasswordHash!,
                    loginRequestModel.Password);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return Result<LoginResponseModel>.Failure(
                    Error.None,
                    "Can't complete password sign in");
            }

            var role = await _userManager.GetRolesAsync(user);

            var claims = GetClaims(
                user,
                role);
            var apiToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.AccessToken = apiToken;
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtOptions.RefreshTokenValidityInDays);

            var updateWithTokenResult = await _userManager.UpdateAsync(user);

            if (!updateWithTokenResult.Succeeded)
            {
                return GetFailureResultFromIdentityResult<LoginResponseModel>(
                    updateWithTokenResult,
                    UserErrors.UnableUpdate);
            }

            var userDto = _mapper.Map<LoginResponseModel>(user);

            return Result<LoginResponseModel>.Success(userDto);
        }
        catch (Exception ex)
        {
            return Result<LoginResponseModel>.Failure(
                UserErrors.UnableLogin,
                ex.ToString());
        }
    }

    public async Task<Result<LoginResponseModel>> RefreshTokenAsync(RefreshAccessTokenRequest refreshTokenDto)
    {
        var accessToken = refreshTokenDto.AccessToken;
        var refreshToken = refreshTokenDto.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return Result<LoginResponseModel>.Failure(
                UserErrors.InvalidTokens,
                "Invalid access token or refresh token");
        }

        var username = principal.Identity!.Name;

        var user = await _userManager.FindByNameAsync(username!);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return Result<LoginResponseModel>.Failure(
                UserErrors.InvalidTokens,
                "Invalid access token or refresh token");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList());
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.AccessToken = newAccessToken;
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtOptions.RefreshTokenValidityInDays);
        await _userManager.UpdateAsync(user);

        var tokensResult = new LoginResponseModel { AccessToken = newAccessToken, RefreshToken = newRefreshToken };

        return Result<LoginResponseModel>.Success(tokensResult);
    }

    public async Task<Result> RegisterAsync(RegisterRequestModel registerUserModel)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var user = _mapper.Map<ApplicationUser>(registerUserModel);
            const string roleName = "default";
            var claims = GetClaims(
                user,
                new List<string> { roleName });
            var jwtToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.AccessToken = jwtToken;
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtOptions.RefreshTokenValidityInDays);

            var isRoleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!isRoleExists)
            {
                return Result.Failure($"Role with name {roleName} does not exist");
            }

            var createResult = await _userManager.CreateAsync(
                user,
                registerUserModel.Password);

            if (!createResult.Succeeded)
            {
                return GetFailureResultFromIdentityResult(
                    createResult,
                    UserErrors.UnableRegister);
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(
                user,
                roleName);

            await transaction.CommitAsync();

            return !addToRoleResult.Succeeded
                ? GetFailureResultFromIdentityResult(
                    addToRoleResult,
                    Error.GetUnspecified())
                : Result.Success();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result.Failure(
                UserErrors.UnableRegister,
                ex.ToString());
        }
    }

    private static Result<T> GetFailureResultFromIdentityResult<T>(
        IdentityResult result,
        Error error)
    {
        var details = result.Errors.Select(e => e.Description).ToList();
        return Result<T>.Failure(
            error,
            details);
    }

    private static Result GetFailureResultFromIdentityResult(
        IdentityResult result,
        Error error)
    {
        var details = result.Errors.Select(e => e.Description).ToList();
        return Result.Failure(
            error,
            details);
    }

    private List<Claim> GetClaims(
        ApplicationUser user,
        IList<string> userRoles)
    {
        var claims = new List<Claim>
        {
            new(
                ClaimTypes.Name,
                user.UserName!),
            new(
                ClaimTypes.Email,
                user.Email!),
            new(
                JwtRegisteredClaimNames.Sub,
                user.Id.ToString()),
            new(
                JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
        };

        claims.AddRange(
            userRoles.Select(
                userRole => new Claim(
                    ClaimTypes.Role,
                    userRole)));

        return claims;
    }
}