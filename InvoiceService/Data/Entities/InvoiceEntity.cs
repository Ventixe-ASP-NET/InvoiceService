using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Data.Entities;

public class InvoiceEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string BookingId { get; set; } = null!;
    public string InvoiceTitel { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime PaymentDate { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalPrice { get; set; }

    //Invoice Details should be the same as the Booking Details (exists in another API)

    [ForeignKey(nameof(Status))]
    public int StatusId { get; set; }
    public StatusEntity Status { get; set; } = null!;
}
