using QRisto.Persistence.Entity.Provider;

namespace QRisto.Persistence.Repositories.OperatingSchedule;

public interface IOperatingScheduleRepository : IGenericRepository<OperatingScheduleEntity>
{
    Task<OperatingScheduleEntity> GetOperatingScheduleForDayAsync(DateOnly day);
}