namespace Core.Interfaces;

public interface IUnitOfWork
{
    IStoreRepository Stores { get; }
    IProductRepository Products { get; }
    public Task<int> SaveAsync();
}
