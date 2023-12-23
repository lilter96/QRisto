using Microsoft.AspNetCore.Identity;
using QRisto.Persistence.Entity.Provider;

namespace QRisto.Persistence.Entity.Auth;

public class ApplicationUser : IdentityUser<Guid>, IEntity
{
    public virtual ICollection<CommentEntity> Comments { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }

    public Guid? DeletedBy { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime? DeletedDate { get; set; }

    public DateTime ModificationDate { get; set; }
    
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}