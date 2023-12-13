namespace QRisto.Application.Models.Request.User;

public record LoginRequestModel(
    string UserName,
    string Password
);