using InvoiceService.Data.Contexts;
using InvoiceService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Data.Repositories;

public class InvoiceRepository(DataContext context) : BaseRepository<InvoiceEntity>(context)
{
    public override async Task<IEnumerable<InvoiceEntity>> GetAllAsync()
    {
        return await _context.Invoices
            .Include(i => i.Status)
            .ToListAsync();
    }

    public override async Task<InvoiceEntity?> GetAsync(System.Linq.Expressions.Expression<Func<InvoiceEntity, bool>> expression)
    {
        return await _context.Invoices
            .Include(i => i.Status)
            .FirstOrDefaultAsync(expression);
    }
}
