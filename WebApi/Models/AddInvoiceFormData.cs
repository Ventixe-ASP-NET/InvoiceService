namespace WebApi.Models
{
    public class AddInvoiceFormData
    {
        public string BookingId { get; set; } = null!;
        public string InvoiceTitel { get; set; } = null!;
        public DateTime PaymentDate { get; set; }
        public decimal TotalPrice { get; set; }

        //Invoice Details should be the same as the Booking Details (exists in another API)

        public int StatusId { get; set; }
    }
}
