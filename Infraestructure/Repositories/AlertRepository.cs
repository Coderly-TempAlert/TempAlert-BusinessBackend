using Core.Entities;
using Core.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories;

public class AlertRepository : GenericRepository<Alert>, IAlertRepository
{
    public AlertRepository(BusinessContext context) : base(context)
    {
    }

    public override async Task<(int totallyRegister, IEnumerable<Alert> registers)> GetAllWithPaginationAsync(int pageIndex, int pageSize, string search = "")
    {
        var consultation = _context.Alerts as IQueryable<Alert>;


        var totallyRegister = await consultation.CountAsync();

        var register = await consultation
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return (totallyRegister, register);
    }
}
