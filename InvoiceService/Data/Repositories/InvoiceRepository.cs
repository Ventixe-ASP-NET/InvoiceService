using InvoiceService.Data.Contexts;
using InvoiceService.Data.Entities;

namespace InvoiceService.Data.Repositories;

public class InvoiceRepository(DataContext context) : BaseRepository<InvoiceEntity>(context)
{
}
