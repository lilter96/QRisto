using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QRisto.Persistence.Entity.Auth;

namespace QRisto.Persistence.Seed;

public class SeedData
{
    private static readonly Guid AdminRoleId = Guid.Parse("042a9760-e09b-4043-ae26-9c3eb5b693d5");
    private static readonly Guid ProviderRoleId = Guid.Parse("9e63669b-8417-4b92-8268-44787689e272");
    private static readonly Guid DefaultRoleId = Guid.Parse("6854d47e-833d-4b16-8298-8c5976315366");
    private static readonly Guid AdminUserId = Guid.Parse("17043a77-6320-4401-9cae-d4197798e395");
    private static readonly Guid AdminUserSecurityStamp = Guid.Parse("f715f502-0ed6-4f30-a762-ac47ddc6fd86");
    
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {
                Id = AdminRoleId,
                Name = ApplicationRoles.Admin,
                NormalizedName = ApplicationRoles.Admin.ToUpper()
            },
            new ApplicationRole
            {
                Id = ProviderRoleId,
                Name = ApplicationRoles.Provider,
                NormalizedName = ApplicationRoles.Provider.ToUpper()
            },
            new ApplicationRole
            {
                Id = DefaultRoleId,
                Name = ApplicationRoles.Default,
                NormalizedName = ApplicationRoles.Default.ToUpper()
            }
        );

        var adminUser = new ApplicationUser
        {
            Id = AdminUserId,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@example.com",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            SecurityStamp = AdminUserSecurityStamp.ToString(),
            EmailConfirmed = true,
            LockoutEnabled = false
        };

        var passwordHasher = new PasswordHasher<ApplicationUser>();
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin");

        builder.Entity<ApplicationUser>().HasData(adminUser);

        builder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid> { RoleId = AdminRoleId, UserId = AdminUserId }
        );
    }
}