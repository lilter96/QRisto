using QRisto.Application.Models.Request.User;
using QRisto.Application.Models.Response.User;
using QRisto.Application.Utils;

namespace QRisto.Application.Services.User;

public interface IUserService
{
    public Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel loginUserDto);

    public Task<Result<LoginResponseModel>> RegisterAsync(RegisterRequestModel registerUserDto);

    public Task<Result<LoginResponseModel>> RefreshTokenAsync(RefreshAccessTokenRequest refreshTokenDto);
}