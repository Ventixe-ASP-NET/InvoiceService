using InvoiceService.Data.Contexts;
using InvoiceService.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(builder =>
    {
    builder.UseMiddleware<CorsMiddleware>();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(
                hostContext.Configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()
            ));
        services.AddScoped<InvoiceAppService>();
        services.AddScoped<StatusAppService>();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();

// Middleware de CORS para Azure Functions dotnet-isolated
public class CorsMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var httpRequest = await context.GetHttpRequestDataAsync();
        if (httpRequest != null)
        {
            context.Items["__AddCorsHeaders"] = true;
        }

        await next(context);

        if (httpRequest != null && context.GetHttpResponseData() is { } response)
        {
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
        }
    }
}