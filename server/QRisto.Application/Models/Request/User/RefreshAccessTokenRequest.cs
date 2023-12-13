namespace QRisto.Application.Models.Request.User;

public record RefreshAccessTokenRequest(
    string AccessToken,
    string RefreshToken); 
    