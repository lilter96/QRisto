using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Persistence.Repositories.Service;

public class ServiceRepository : GenericRepository<ServiceEntity>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext context) : base(context)
    {
    }
}