using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Persistence.Repositories.Provider;

public class ProviderRepository : GenericRepository<ProviderEntity>, IProviderRepository
{
    public ProviderRepository(ApplicationDbContext context) : base(context)
    {
    }
}