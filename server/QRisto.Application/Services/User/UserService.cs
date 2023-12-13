using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QRisto.Application.Configuration;
using QRisto.Application.Models.Request.User;
using QRisto.Application.Models.Response.User;
using QRisto.Application.Services.Token;
using QRisto.Application.Utils;
using QRisto.Persistence.Entity;

namespace QRisto.Application.Services.User;

public class UserService : IUserService
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly JwtOptions _jwtOptions;
    public UserService(
        ITokenService tokenService,
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        JwtOptions jwtOptions)
    {
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
    }

    public async Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(loginRequestModel.UserName);
            
            if (user is null)
            {
                return Result<LoginResponseModel>.Failure(UserErrors.NotFound, $"User with user name {loginRequestModel.UserName} not found.");
            }

            var passwordVerificationResult = _userManager
                .PasswordHasher
                .VerifyHashedPassword(user, user.PasswordHash!, loginRequestModel.Password);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return Result<LoginResponseModel>.Failure(Error.None, "Can't complete password sign in");
            }

            var claims = GetClaims(user);
            var apiToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.AccessToken = apiToken;
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtOptions.RefreshTokenValidityInDays);

            var updateWithTokenResult = await _userManager.UpdateAsync(user);

            if (!updateWithTokenResult.Succeeded)
            {
                return GetFailureResultFromIdentityResult<LoginResponseModel>(updateWithTokenResult, UserErrors.UnableUpdate);
            }

            var userDto = _mapper.Map<LoginResponseModel>(user);

            return Result<LoginResponseModel>.Success(userDto);
        }
        catch (Exception ex)
        {
            return Result<LoginResponseModel>.Failure(UserErrors.UnableLogin, ex.ToString());
        }
    }

    public async Task<Result<LoginResponseModel>> RegisterAsync(RegisterRequestModel registerUserDto)
    {
        try
        {
            var user = _mapper.Map<ApplicationUser>(registerUserDto);
            var claims = GetClaims(user);
            var jwtToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.AccessToken = jwtToken;
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtOptions.RefreshTokenValidityInDays);
            var createResult = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (!createResult.Succeeded)
            {
                return GetFailureResultFromIdentityResult<LoginResponseModel>(createResult, UserErrors.UnableRegister);
            }

            var userDto = _mapper.Map<LoginResponseModel>(user);

            return Result<LoginResponseModel>.Success(userDto);
        }
        catch (Exception ex)
        {
            return Result<LoginResponseModel>.Failure(UserErrors.UnableRegister, ex.ToString());
        }
    }

    public async Task<Result<LoginResponseModel>> RefreshTokenAsync(RefreshAccessTokenRequest refreshTokenDto)
    {
        var accessToken = refreshTokenDto.AccessToken;
        var refreshToken = refreshTokenDto.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return Result<LoginResponseModel>.Failure(UserErrors.InvalidTokens, "Invalid access token or refresh token");
        }

        var username = principal.Identity!.Name;

        var user = await _userManager.FindByNameAsync(username!);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return Result<LoginResponseModel>.Failure(UserErrors.InvalidTokens, "Invalid access token or refresh token");
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

    private Result<T> GetFailureResultFromIdentityResult<T>(
        IdentityResult result,
        Error error)
    {
        var details = result.Errors.Select(e => e.Description).ToList();
        return Result<T>.Failure(error, details);
    }

    private List<Claim> GetClaims(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        return claims;
    }
}