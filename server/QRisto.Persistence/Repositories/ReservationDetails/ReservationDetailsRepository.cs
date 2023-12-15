using Microsoft.EntityFrameworkCore;
using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Persistence.Repositories.ReservationDetails;

public class ReservationDetailsRepository : GenericRepository<ReservationDetailsEntity>, IReservationDetailsRepository
{
    public ReservationDetailsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ReservationDetailsEntity> GetByReservationIdAsync(Guid reservationId)
    {
        return await DbSet
            .Where(details => details.Reservation.Id == reservationId)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateCustomerContactInfoAsync(Guid detailsId, string newPhone, string newEmail)
    {
        var details = await DbSet.FindAsync(detailsId);
        if (details != null)
        {
            details.CustomerPhone = newPhone;
            details.CustomerEmail = newEmail;
            await Context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ReservationDetailsEntity>> FindByCustomerNameAsync(string customerName)
    {
        return await DbSet
            .Where(details => details.CustomerName.Contains(customerName))
            .ToListAsync();
    }
}