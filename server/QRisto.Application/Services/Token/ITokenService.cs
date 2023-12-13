using System.Security.Claims;

namespace QRisto.Application.Services.Token;

public interface ITokenService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);

    public string GenerateRefreshToken();

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}