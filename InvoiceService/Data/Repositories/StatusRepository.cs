using InvoiceService.Data.Contexts;
using InvoiceService.Data.Entities;

namespace InvoiceService.Data.Repositories;

public class StatusRepository(DataContext context) : BaseRepository<StatusEntity>(context)
{
}
