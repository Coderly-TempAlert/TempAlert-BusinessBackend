using Core.Entities;
using Core.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories;

public class StoreRepository : GenericRepository<Store>, IStoreRepository
{
    public StoreRepository(BusinessContext context) : base(context)
    {
    }

    public override async Task<(int totallyRegister, IEnumerable<Store> registers)> GetAllWithPaginationAsync(int pageIndex, int pageSize, string search)
    {
        var consultation = _context.Stores as IQueryable<Store>;

        if (!String.IsNullOrEmpty(search))
        {
            consultation = consultation.Where(p => p.Name.ToLower().Contains(search));
        }


        var totallyRegister = await consultation.CountAsync();

        var register = await consultation
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return (totallyRegister, register);
    }
}
