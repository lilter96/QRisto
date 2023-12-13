using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRisto.Application.Models.Request.User;
using QRisto.Application.Models.Response.User;
using QRisto.Application.Services.User;
using QRisto.Application.Utils;

namespace QRisto.Presentation.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(
        IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequestModel registerUserDto)
    {
        var result = await _userService.RegisterAsync(registerUserDto);

        return result.Match<IActionResult, LoginResponseModel>(
            onSuccess: Ok,
            onFailure: BadRequest);
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequestModel loginUserDto)
    {
        var result = await _userService.LoginAsync(loginUserDto);

        return result.Match<IActionResult, LoginResponseModel>(
            onSuccess: Ok,
            onFailure: BadRequest);
    }
    
    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> GenerateToken(RefreshAccessTokenRequest refreshTokenDto)
    {
        var result = await _userService.RefreshTokenAsync(refreshTokenDto);

        return result.Match<IActionResult, LoginResponseModel>(
            onSuccess: Ok,
            onFailure: error => StatusCode(StatusCodes.Status500InternalServerError, error));
    }
}