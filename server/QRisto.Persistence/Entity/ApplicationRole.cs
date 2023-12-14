using Microsoft.AspNetCore.Identity;

namespace QRisto.Persistence.Entity;

public class ApplicationRole : IdentityRole<Guid>
{
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}