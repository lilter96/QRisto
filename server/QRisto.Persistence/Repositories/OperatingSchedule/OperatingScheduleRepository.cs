using Microsoft.EntityFrameworkCore;
using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Persistence.Repositories.OperatingSchedule;

public class OperatingScheduleRepository : GenericRepository<OperatingScheduleEntity>, IOperatingScheduleRepository
{
    public OperatingScheduleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<OperatingScheduleEntity> GetOperatingScheduleForDayAsync(DateOnly day)
    {
        var result = await DbSet
            .Include(x => x.WorkingIntervals)
            .FirstOrDefaultAsync(schedule => schedule.Date == day);

        return result;
    }
}