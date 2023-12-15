using QRisto.Persistence.Entity.Provider;

namespace QRisto.Persistence.Repositories.ReservationDetails;

public interface IReservationDetailsRepository : IGenericRepository<ReservationDetailsEntity>
{
    Task<ReservationDetailsEntity> GetByReservationIdAsync(Guid reservationId);

    Task UpdateCustomerContactInfoAsync(Guid detailsId, string newPhone, string newEmail);

    Task<IEnumerable<ReservationDetailsEntity>> FindByCustomerNameAsync(string customerName);
}