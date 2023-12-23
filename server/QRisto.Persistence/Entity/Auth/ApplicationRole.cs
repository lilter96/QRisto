using Microsoft.AspNetCore.Identity;

namespace QRisto.Persistence.Entity.Auth;

public class ApplicationRole : IdentityRole<Guid>, IEntity
{
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public Guid? DeletedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public DateTime? DeletedDate { get; set; }

    public DateTime ModificationDate { get; set; }
}

public static class ApplicationRoles
{
    public const string Admin = "Admin";

    public const string Provider = "Provider";

    public const string Default = "Default";

    public const string AllRoles = Default + ", " + Provider + ", " + Admin;
}