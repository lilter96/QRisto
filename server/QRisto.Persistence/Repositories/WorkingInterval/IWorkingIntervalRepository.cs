using QRisto.Persistence.Entity.Provider;

namespace QRisto.Persistence.Repositories.WorkingInterval;

public interface IWorkingIntervalRepository : IGenericRepository<WorkingIntervalEntity>
{
    Task<List<WorkingIntervalEntity>> GetByTableAndDayAsync(Guid tableId, DateOnly date);
}