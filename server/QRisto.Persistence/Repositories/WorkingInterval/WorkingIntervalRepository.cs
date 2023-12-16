using Microsoft.EntityFrameworkCore;
using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Persistence.Repositories.WorkingInterval;

public class WorkingIntervalRepository : GenericRepository<WorkingIntervalEntity>, IWorkingIntervalRepository
{
    public WorkingIntervalRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<WorkingIntervalEntity>> GetByTableAndDayAsync(Guid tableId, DateOnly date)
    {
        var query = DbSet
            .Include(interval => interval.OperatingSchedule)
            .ThenInclude(schedule => schedule.Service)
            .ThenInclude(service => service.Tables)
            .Where(interval => interval.OperatingSchedule.Service.Tables.Any(table => table.Id == tableId))
            .Where(interval => interval.OperatingSchedule.Date == date);

        return await query.ToListAsync();
    }
}