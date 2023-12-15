using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QRisto.Persistence.Entity;
using QRisto.Persistence.Entity.Auth;
using QRisto.Persistence.Entity.Provider;

namespace QRisto.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public virtual DbSet<AddressEntity> Addresses { get; set; }
    
    public virtual DbSet<ProviderEntity> Providers { get; set; }
    
    public virtual DbSet<ServiceEntity> Services { get; set; }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();

        var added = ChangeTracker.Entries().Where(w => w.State == EntityState.Added).Select(s => s.Entity).ToList();

        foreach (var entry in added.Where(entry => entry is IEntity))
        {
            ((IEntity)entry).CreatedDate = DateTime.Now;
            ((IEntity)entry).ModificationDate = DateTime.Now;
        }

        var updated = ChangeTracker.Entries().Where(w => w.State == EntityState.Modified).Select(s => s.Entity)
            .ToList();

        foreach (var entry in updated.OfType<IEntity>())
        {
            entry.ModificationDate = DateTime.Now;
        }

        return base.SaveChanges();
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        return Task.Run(SaveChanges, cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ServiceEntity>(e =>
        {
            e.HasOne(x => x.Address);
        });
    }
}