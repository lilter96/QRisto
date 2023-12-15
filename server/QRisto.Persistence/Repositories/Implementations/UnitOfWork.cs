using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QRisto.Persistence.Repositories.Address;
using QRisto.Persistence.Repositories.OperatingSchedule;
using QRisto.Persistence.Repositories.Provider;
using QRisto.Persistence.Repositories.Reservation;
using QRisto.Persistence.Repositories.ReservationDetails;
using QRisto.Persistence.Repositories.Service;
using QRisto.Persistence.Repositories.Table;
using QRisto.Persistence.Repositories.WorkingInterval;

namespace QRisto.Persistence.Repositories.Implementations;

public class UnitOfWork : IDisposable
{
    private readonly ApplicationDbContext _context;
    private IAddressRepository _addressRepository;
    private bool _disposed;
    private IOperatingScheduleRepository _operatingScheduleRepository;
    private IProviderRepository _providerRepository;
    private IReservationDetailsRepository _reservationDetailsRepository;
    private IReservationRepository _reservationRepository;
    private IServiceRepository _serviceRepository;
    private ITableRepository _tableRepository;
    private IDbContextTransaction _transaction;
    private IWorkingIntervalRepository _workingIntervalRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IProviderRepository ProviderRepository
    {
        get { return _providerRepository ??= new ProviderRepository(_context); }
    }

    public IServiceRepository ServiceRepository
    {
        get { return _serviceRepository ??= new ServiceRepository(_context); }
    }

    public IAddressRepository AddressRepository
    {
        get { return _addressRepository ??= new AddressRepository(_context); }
    }

    public ITableRepository TableRepository
    {
        get { return _tableRepository ??= new TableRepository(_context); }
    }

    public IReservationRepository ReservationRepository
    {
        get { return _reservationRepository ??= new ReservationRepository(_context); }
    }

    public IReservationDetailsRepository ReservationDetailsRepository
    {
        get { return _reservationDetailsRepository ??= new ReservationDetailsRepository(_context); }
    }

    public IOperatingScheduleRepository OperatingScheduleRepository
    {
        get { return _operatingScheduleRepository ??= new OperatingScheduleRepository(_context); }
    }

    public IWorkingIntervalRepository WorkingIntervalRepository
    {
        get { return _workingIntervalRepository ??= new WorkingIntervalRepository(_context); }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    _transaction.Dispose();
                    _transaction = null;
                }

                _context.Dispose();
            }
        }

        _disposed = true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _transaction = await _context.Database.BeginTransactionAsync(isolationLevel);
        return _transaction;
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction in progress.");
        }

        try
        {
            await SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}