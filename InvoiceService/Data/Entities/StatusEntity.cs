using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Data.Entities;

public class StatusEntity
{
    [Key]
    public int Id { get; set; }
    public string IsPaid { get; set; } = null!;
    public ICollection<InvoiceEntity> Invoices { get; set; } = null!;
}
