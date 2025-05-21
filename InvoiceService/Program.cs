using InvoiceService.Data.Contexts;
using InvoiceService.Data.Repositories;
using InvoiceService.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);
var connectionstring = Environment.GetEnvironmentVariable("DefaultConnection");
builder.Services
    .AddDbContext<DataContext>(options =>
        options.UseSqlServer(
            connectionstring,
            sqlOptions => sqlOptions.EnableRetryOnFailure()
        ))
    .AddScoped<InvoiceRepository>()
    .AddScoped<StatusRepository>()
    .AddScoped<InvoiceAppService>()
    .AddScoped<StatusAppService>()
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
