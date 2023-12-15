using Microsoft.EntityFrameworkCore;
using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Persistence.Repositories.Table;

public class TableRepository : GenericRepository<TableEntity>, ITableRepository
{
    public TableRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TableEntity>> GetAvailableTablesAsync(DateTime reservationTime, int duration)
    {
        return await DbSet
            .Where(
                table => !table.Reservations.Any(
                    reservation =>
                        reservation.ReservationTime < reservationTime.AddMinutes(duration) &&
                        reservationTime < reservation.ReservationTime.AddMinutes(reservation.DurationInMinutes)))
            .ToListAsync();
    }

    public async Task AddReservationAsync(Guid tableId, ReservationEntity reservation)
    {
        var table = await DbSet.FindAsync(tableId);
        if (table != null)
        {
            table.Reservations.Add(reservation);
            await Context.SaveChangesAsync();
        }
    }

    public async Task UpdateTableSeatsAsync(Guid tableId, int newSeats)
    {
        var table = await DbSet.FindAsync(tableId);
        if (table != null)
        {
            table.Seats = newSeats;
            await Context.SaveChangesAsync();
        }
    }

    public async Task<bool> TryDeleteTableAsync(Guid tableId)
    {
        var table = await DbSet.Include(t => t.Reservations)
            .FirstOrDefaultAsync(t => t.Id == tableId);

        if (table == null || table.Reservations.Any())
        {
            return false;
        }

        DbSet.Remove(table);
        await Context.SaveChangesAsync();
        return true;
    }
}