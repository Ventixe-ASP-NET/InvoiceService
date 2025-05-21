using InvoiceService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace InvoiceService.Functions;

public class GetInvoiceById
{
    private readonly ILogger<GetInvoiceById> _logger;
    private readonly InvoiceAppService _invoiceAppService;

    public GetInvoiceById(ILogger<GetInvoiceById> logger, InvoiceAppService invoiceAppService)
    {
        _logger = logger;
        _invoiceAppService = invoiceAppService;
    }

    [Function("GetInvoiceById")]
    public async Task<HttpResponseData> GetById(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoices/{id}")] HttpRequestData req,
        string id)
    {
        var invoice = await _invoiceAppService.GetInvoiceByIdAsync(id);
        var response = req.CreateResponse(invoice == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);
        if (invoice != null)
            await response.WriteAsJsonAsync(invoice);
        return response;
    }
}