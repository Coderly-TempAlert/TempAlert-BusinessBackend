using Core.Entities;

namespace Core.Interfaces;

public interface IStoreProductRepository : IGenericRepository<StoreProducts>
{
    public Task<(int totallyRegister, IEnumerable<StoreProducts> registers)> GetProductWithPaginationAsync(int pageIndex, int pageSize, Guid id);
}
