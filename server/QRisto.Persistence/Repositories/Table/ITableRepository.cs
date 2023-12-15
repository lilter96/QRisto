using QRisto.Persistence.Entity.Provider;

namespace QRisto.Persistence.Repositories.Table;

public interface ITableRepository : IGenericRepository<TableEntity>
{
    Task<IEnumerable<TableEntity>> GetAvailableTablesAsync(DateTime reservationTime, int duration);

    Task AddReservationAsync(Guid tableId, ReservationEntity reservation);

    Task UpdateTableSeatsAsync(Guid tableId, int newSeats);

    Task<bool> TryDeleteTableAsync(Guid tableId);
}