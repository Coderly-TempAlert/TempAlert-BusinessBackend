namespace Core.Interfaces;

public interface IUnitOfWork
{
    IStoreRepository Stores { get; }
    IProductRepository Products { get; }
    IStoreProductRepository StoreProducts { get; }
    public Task<int> SaveAsync();
}
