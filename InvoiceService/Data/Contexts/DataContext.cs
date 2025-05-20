using InvoiceService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<InvoiceEntity> Invoices { get; set; } = null!;
    public DbSet<StatusEntity> Statuses { get; set; } = null!;
}

// First, we need to confirm that the `DataContext` class is correctly set up to use the `InvoiceEntity` and `StatusEntity` classes.
// Then, we need to run the migrations to create the database schema.
// Will the database be located in the same project as the API, or will it be a separate project?
