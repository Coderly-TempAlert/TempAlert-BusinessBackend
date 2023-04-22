using Core.Interfaces;
using Infraestructure.Data;
using Infraestructure.Repositories;

namespace Infraestructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly BusinessContext _context;
    private IProductRepository _products;
    private IStoreRepository _stores;

    public UnitOfWork(BusinessContext context)
    {
        _context = context;
    }

    public IProductRepository Products
    {
        get
        {
            if (_products == null)
            {
                _products = new ProductRepository(_context);
            }
            return _products;
        }
    }

    public IStoreRepository Stores
    {
        get
        {
            if (_stores == null)
            {
                _stores = new StoreRepository(_context);
            }
            return _stores;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
