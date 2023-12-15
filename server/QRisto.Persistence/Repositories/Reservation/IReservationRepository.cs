using QRisto.Persistence.Entity.Provider;

namespace QRisto.Persistence.Repositories.Reservation;

public interface IReservationRepository : IGenericRepository<ReservationEntity>
{
    Task<IEnumerable<ReservationEntity>> GetAllForDateAsync(DateTime date);

    Task<IEnumerable<ReservationEntity>> GetByCustomerAsync(string customerEmail);

    Task<bool> CancelReservationAsync(Guid reservationId);

    Task UpdateStatusAsync(Guid reservationId, ReservationStatus newStatus);

    Task<List<ReservationEntity>> GetReservationsForDayAsync(Guid serviceId, DateOnly date);
}