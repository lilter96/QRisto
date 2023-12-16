using Microsoft.AspNetCore.Identity;

namespace QRisto.Persistence.Entity.Auth;

public class ApplicationUser : IdentityUser<Guid>, IEntity
{
    public DateTime RefreshTokenExpiryTime { get; set; }

    public Guid DeletedBy { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime? DeletedDate { get; set; }

    public DateTime ModificationDate { get; set; }
    
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}