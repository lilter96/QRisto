namespace QRisto.Application.Models.Request.User;

public record RegisterRequestModel(
    string UserName,
    string Email,
    string Password
);