using InvoiceService.Data.Repositories;
using InvoiceService.Models;

namespace InvoiceService.Services;

public class StatusAppService(StatusRepository statusRepository)
{
    private readonly StatusRepository _statusRepository = statusRepository;

    public async Task<IEnumerable<Status>> GetAllStatusesAsync()
    {
        var entities = await _statusRepository.GetAllAsync();
        var statuses = entities.Select(e => new Status
        {
            Id = e.Id,
            IsPaid = e.IsPaid,
        });
        return statuses;
    }

    public async Task<Status?> GetStatusByIdAsync(int id)
    {
        var entity = await _statusRepository.GetAsync(e => e.Id == id);
        return entity == null ? null : new Status
        {
            Id = entity.Id,
            IsPaid = entity.IsPaid,
        };
    }
}
