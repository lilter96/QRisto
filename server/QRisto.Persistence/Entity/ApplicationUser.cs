using Microsoft.AspNetCore.Identity;

namespace QRisto.Persistence.Entity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }
}