namespace QRisto.Application.Models.Response.User;

public class LoginResponseModel
{
    public string AccessToken { get; init; }
    
    public string RefreshToken { get; init; }
}