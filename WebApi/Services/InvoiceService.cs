using WebApi.Data.Entities;
using WebApi.Data.Repositories;
using WebApi.Models;

namespace WebApi.Services;

public class InvoiceService(InvoiceRepository invoiceRepository)
{
    private readonly InvoiceRepository _invoiceRepository = invoiceRepository;

    public async Task<bool> AddInvoiceAsync(AddInvoiceFormData invoiceFormData)
    {
        if (invoiceFormData == null)
            return false;

        var entity = new InvoiceEntity
        {
            BookingId = invoiceFormData.BookingId,
            InvoiceTitel = invoiceFormData.InvoiceTitel,
            PaymentDate = invoiceFormData.PaymentDate,
            TotalPrice = invoiceFormData.TotalPrice,
            StatusId = 2, // Default status ID for unpaid invoices
        };
        var result = await _invoiceRepository.AddAsync(entity);
        return result;
    }

    public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync(bool orderByDescending = false)
    {
        var entities = await _invoiceRepository.GetAllAsync();
        var invoices = entities.Select(i => new Invoice
            {
                Id = i.Id,
                BookingId = i.BookingId,
                InvoiceTitel = i.InvoiceTitel,
                PaymentDate = i.PaymentDate,
                TotalPrice = i.TotalPrice,
                Status = new Status
                {
                    Id = i.Status.Id,
                    IsPaid = i.Status.IsPaid,
                }

            });
        return orderByDescending
                    ? invoices.OrderByDescending(e => e.InvoiceTitel)
                    : invoices.OrderBy(e => e.InvoiceTitel);
    }

    public async Task<Invoice?> GetInvoiceByIdAsync(string id)
    {
        var entity = await _invoiceRepository.GetAsync(e => e.Id == id);
        return entity == null ? null : new Invoice
        {
            Id = entity.Id,
            BookingId = entity.BookingId,
            InvoiceTitel = entity.InvoiceTitel,
            PaymentDate = entity.PaymentDate,
            TotalPrice = entity.TotalPrice,
            Status = new Status
            {
                Id = entity.Status.Id,
                IsPaid = entity.Status.IsPaid,
            }
        };
    }

    public async Task<bool> UpdateInvoiceAsync(UpdateInvoiceFormData invoiceFormData)
    {
        if (invoiceFormData == null)
            return false;
        var entity = new InvoiceEntity
        {
            Id = invoiceFormData.Id,
            BookingId = invoiceFormData.BookingId,
            InvoiceTitel = invoiceFormData.InvoiceTitel,
            PaymentDate = DateTime.Now,
            TotalPrice = invoiceFormData.TotalPrice,
            StatusId = 1, // Default status ID for paid invoices
        };
        var result = await _invoiceRepository.UpdateAsync(entity);
        return result;
    }

    //Just for testing purposes
    public async Task<bool> DeleteInvoiceAsync(string id)
    {
        var result = await _invoiceRepository.DeleteAsync(e => e.Id == id);
        return result;
    }

}
