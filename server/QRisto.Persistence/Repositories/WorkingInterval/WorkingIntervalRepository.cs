using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Persistence.Repositories.WorkingInterval;

public class WorkingIntervalRepository : GenericRepository<WorkingIntervalEntity>, IWorkingIntervalRepository
{
    public WorkingIntervalRepository(ApplicationDbContext context) : base(context)
    {
    }
}