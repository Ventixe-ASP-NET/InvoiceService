using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data.Entities;

public class InvoiceEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string BookingId { get; set; } = null!;
    public string InvoiceTitel { get; set; } = null!;

    //public bool IsPaid { get; set; }

    //public DateTime BookingDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal TotalPrice { get; set; }

    //Invoice Details should be the same as the Booking Details (exists in another API)

    [ForeignKey(nameof(Status))]
    public int StatusId { get; set; }
    public StatusEntity Status { get; set; } = null!;
}
