using Microsoft.EntityFrameworkCore;
using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Persistence.Repositories.Provider;

public class ProviderRepository : GenericRepository<ProviderEntity>, IProviderRepository
{
    public ProviderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<ProviderEntity>> GetListAsync()
    {
        return await DbSet.ToListAsync();
    }

    public Task<ProviderEntity> GetByIdWithAddressAsync(Guid id)
    {
        return DbSet
            .Include(x => x.Address)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}