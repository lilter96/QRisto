using QRisto.Persistence.Entity;
using QRisto.Persistence.Entity.Provider;

namespace QRisto.Persistence.Repositories.Implementations;

public class UnitOfWork : IDisposable
{
    private readonly ApplicationDbContext _context;
    private GenericRepository<ProviderEntity> providerRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public GenericRepository<ProviderEntity> ProviderRepository
    {
        get
        {
            return providerRepository ?? (providerRepository = new GenericRepository<ProviderEntity>(_context));
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    private bool disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}