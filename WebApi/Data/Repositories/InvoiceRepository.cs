using WebApi.Data.Contexts;
using WebApi.Data.Entities;

namespace WebApi.Data.Repositories;

public class InvoiceRepository(DataContext context) : BaseRepository<InvoiceEntity>(context)
{
}
