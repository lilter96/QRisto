using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRisto.Application.Models.Request.User;
using QRisto.Application.Models.Response.User;
using QRisto.Application.Services.User;
using QRisto.Application.Utils;
using QRisto.Presentation.ClientApp;

namespace QRisto.Presentation.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequestModel registerUserDto)
    {
        var result = await _userService.RegisterAsync(registerUserDto);

        return result.Match<IActionResult>(
            Ok,
            BadRequest);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequestModel loginUserDto)
    {
        var result = await _userService.LoginAsync(loginUserDto);

        if (result.IsSuccess)
        {
            SetAuthCookie(result.Value.AccessToken);
        }

        return result.Match<IActionResult, LoginResponseModel>(
            Ok,
            BadRequest);
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> GenerateToken(RefreshAccessTokenRequest refreshTokenDto)
    {
        var result = await _userService.RefreshTokenAsync(refreshTokenDto);

        return result.Match<IActionResult, LoginResponseModel>(
            Ok,
            error => StatusCode(StatusCodes.Status500InternalServerError, error));
    }

    private void SetAuthCookie([NotNull] string token)
    {
        Response.Cookies.Append(
            AuthOptions.CookieName, token, new CookieOptions { MaxAge = TimeSpan.FromDays(77 * 365) });
    }
}