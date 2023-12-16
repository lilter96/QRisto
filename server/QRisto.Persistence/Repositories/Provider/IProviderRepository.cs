using QRisto.Persistence.Entity.Provider;

namespace QRisto.Persistence.Repositories.Provider;

public interface IProviderRepository : IGenericRepository<ProviderEntity>
{
    Task<List<ProviderEntity>> GetListAsync();
    
    Task<ProviderEntity> GetByIdWithAddressAsync(Guid id);
}