using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceService.Data.Entities;

public class StatusEntity
{
    [Key]
    public int Id { get; set; }
    public string IsPaid { get; set; } = null!;
    public ICollection<InvoiceEntity> Invoices { get; set; } = null!;
}
