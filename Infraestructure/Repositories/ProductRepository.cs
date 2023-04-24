using Core.Entities;
using Core.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(BusinessContext context) : base(context)
    {
    }

    public override async Task<(int totallyRegister, IEnumerable<Product> registers)> GetAllWithPaginationAsync(int pageIndex, int pageSize, string search)
    {
        var consultation = _context.Products as IQueryable<Product>;

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
