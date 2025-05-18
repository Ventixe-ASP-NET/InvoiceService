using WebApi.Data.Contexts;
using WebApi.Data.Entities;

namespace WebApi.Data.Repositories;

public class StatusRepository(DataContext context) : BaseRepository<StatusEntity>(context)
{
}
