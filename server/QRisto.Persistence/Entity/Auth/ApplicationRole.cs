using Microsoft.AspNetCore.Identity;

namespace QRisto.Persistence.Entity.Auth;

public class ApplicationRole : IdentityRole<Guid>, IEntity
{
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime ModificationDate { get; set; }
}