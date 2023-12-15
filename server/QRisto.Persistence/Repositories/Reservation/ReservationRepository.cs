using Microsoft.EntityFrameworkCore;
using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Persistence.Repositories.Reservation;

public class ReservationRepository : GenericRepository<ReservationEntity>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ReservationEntity>> GetAllForDateAsync(DateTime date)
    {
        var startOfDay = date.Date;
        var endOfDay = startOfDay.AddDays(1);

        return await DbSet
            .Where(
                reservation => reservation.ReservationTime >= startOfDay &&
                               reservation.ReservationTime < endOfDay)
            .ToListAsync();
    }

    public async Task<IEnumerable<ReservationEntity>> GetByCustomerAsync(string customerEmail)
    {
        return await DbSet
            .Include(d => d.ReservationDetails)
            .Where(reservation => reservation.ReservationDetails.CustomerEmail == customerEmail)
            .ToListAsync();
    }

    public async Task<bool> CancelReservationAsync(Guid reservationId)
    {
        var reservation = await DbSet.FindAsync(reservationId);
        if (reservation == null)
        {
            return false;
        }

        reservation.Status = ReservationStatus.Declined;
        await Context.SaveChangesAsync();
        return true;
    }

    public async Task UpdateStatusAsync(Guid reservationId, ReservationStatus newStatus)
    {
        var reservation = await DbSet.FindAsync(reservationId);
        if (reservation != null)
        {
            reservation.Status = newStatus;
            await Context.SaveChangesAsync();
        }
    }

    public async Task<List<ReservationEntity>> GetReservationsForDayAsync(Guid serviceId, DateOnly date)
    {
        var targetDate = date.ToDateTime(new TimeOnly());

        var query = DbSet.Where(reservation => reservation.ReservationTime.Date == targetDate);

        return await query.ToListAsync();
    }
}