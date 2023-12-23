using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QRisto.Persistence.Converters;
using QRisto.Persistence.Entity;
using QRisto.Persistence.Entity.Auth;
using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Seed;

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

    public virtual DbSet<TableEntity> Tables { get; set; }

    public virtual DbSet<ReservationEntity> Reservations { get; set; }

    public virtual DbSet<ReservationDetailsEntity> ReservationDetailEntities { get; set; }

    public virtual DbSet<WorkingIntervalEntity> WorkingIntervals { get; set; }

    public virtual DbSet<OperatingScheduleEntity> OperatingSchedules { get; set; }
    
    public virtual DbSet<CommentEntity> Comments { get; set; }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();

        var added = ChangeTracker.Entries().Where(w => w.State == EntityState.Added).Select(s => s.Entity).ToList();

        foreach (var entry in added.Where(entry => entry is IEntity))
        {
            ((IEntity)entry).CreatedDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            ((IEntity)entry).ModificationDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        }

        var updated = ChangeTracker.Entries().Where(w => w.State == EntityState.Modified).Select(s => s.Entity)
            .ToList();

        foreach (var entry in updated.OfType<IEntity>())
        {
            entry.ModificationDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        }

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        return Task.Run(
            SaveChanges,
            cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ServiceEntity>(
            e =>
            {
                e.HasIndex(r => r.Email).IsUnique();
                e.HasIndex(r => r.PhoneNumber).IsUnique();
                e.HasOne(x => x.Address);
                e
                    .HasMany(r => r.Tables)
                    .WithOne(t => t.Service)
                    .HasForeignKey(t => t.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        builder.Entity<TableEntity>(
            e =>
            {
                e
                    .HasMany(t => t.Reservations)
                    .WithOne(r => r.Table)
                    .HasForeignKey(r => r.TableId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        builder.Entity<ReservationEntity>(
            e =>
            {
                e
                    .HasIndex(r => new { r.TableId, r.ReservationTime })
                    .IsUnique();
                e
                    .HasOne(r => r.ReservationDetails)
                    .WithOne(d => d.Reservation)
                    .HasForeignKey<ReservationDetailsEntity>(x => x.ReservationDetailsId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        builder.Entity<OperatingScheduleEntity>(
            entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.ServiceId, e.Date }).IsUnique();
                entity.Property(e => e.Date).HasColumnType("date");
                entity.HasMany(e => e.WorkingIntervals)
                    .WithOne(w => w.OperatingSchedule)
                    .HasForeignKey(w => w.OperatingScheduleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        builder.Entity<WorkingIntervalEntity>(
            entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StartTime).HasColumnType("time");
                entity.Property(e => e.EndTime).HasColumnType("time");
                entity.HasIndex(e => new { e.OperatingScheduleId, e.StartTime, e.EndTime }).IsUnique();
                entity.Property(e => e.Type).HasConversion<string>();
            });

        builder.Entity<CommentEntity>(
            entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ServiceId);
                entity.HasIndex(e => e.UserId);
                entity
                    .HasOne(e => e.Service)
                    .WithMany(w => w.Comments)
                    .HasForeignKey(e => e.ServiceId);
                entity
                    .HasOne(e => e.User)
                    .WithMany(e => e.Comments)
                    .HasForeignKey(e => e.UserId);
            });

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var deletedTimeProperty = entityType.FindProperty("DeletedDate");
            if (deletedTimeProperty == null || deletedTimeProperty.ClrType != typeof(DateTime?))
            {
                continue;
            }

            var parameter = Expression.Parameter(entityType.ClrType);
            var property = Expression.Property(parameter, deletedTimeProperty.PropertyInfo!);
            var condition = Expression.Equal(property, Expression.Constant(null, typeof(DateTime?)));
            var lambda = Expression.Lambda(condition, parameter);

            var entityMethod = typeof(ModelBuilder).GetMethods()
                .Single(m => m.Name == "Entity" && m.GetParameters().Length == 0)
                .MakeGenericMethod(entityType.ClrType);
            var entityBuilder = entityMethod.Invoke(builder, null) as EntityTypeBuilder;
            entityBuilder?.HasQueryFilter(lambda);
        }
        
        SeedData.Seed(builder);
        
        builder.ApplyUtcDateTimeConverter();
    }
}