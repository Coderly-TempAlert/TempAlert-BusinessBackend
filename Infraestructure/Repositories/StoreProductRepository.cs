using Core.Entities;
using Core.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories;

public class StoreProductRepository : GenericRepository<StoreProducts>, IStoreProductRepository
{

    public StoreProductRepository(BusinessContext context) : base(context)
    {
    }

    public async Task<(int totallyRegister, IEnumerable<StoreProducts> registers)> GetProductWithPaginationAsync(int pageIndex, int pageSize, Guid id)
    {
        var consultation = _context.StoreProducts as IQueryable<StoreProducts>;

        consultation = consultation.Where(p => p.StoreId == id);


        var totallyRegister = await consultation.CountAsync();

        var register = await consultation
                                .Include(u => u.Product)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .Include(u => u.Store)
                                .ToListAsync();

        return (totallyRegister, register);
    }
}
