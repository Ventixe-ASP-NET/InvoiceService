using InvoiceService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace InvoiceService.Functions;

public class GetAllInvoices
{
    private readonly ILogger<GetAllInvoices> _logger;
    private readonly InvoiceAppService _invoiceAppService;

    public GetAllInvoices(ILogger<GetAllInvoices> logger, InvoiceAppService invoiceAppService)
    {
        _logger = logger;
        _invoiceAppService = invoiceAppService;
    }

    [Function("GetAllInvoices")]
    public async Task<HttpResponseData> GetAll(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoices")] HttpRequestData req)
    {
        var invoices = await _invoiceAppService.GetAllInvoicesAsync(orderByDescending: true);
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(invoices);
        return response;
    }
}