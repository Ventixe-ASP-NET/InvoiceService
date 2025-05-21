using InvoiceService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace InvoiceService.Functions;

public class DeleteInvoice
{
    private readonly ILogger<DeleteInvoice> _logger;
    private readonly InvoiceAppService _invoiceAppService;

    public DeleteInvoice(ILogger<DeleteInvoice> logger, InvoiceAppService invoiceAppService)
    {
        _logger = logger;
        _invoiceAppService = invoiceAppService;
    }

    [Function("DeleteInvoice")]
    public async Task<HttpResponseData> Delete(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "invoices/{id}")] HttpRequestData req,
        string id)
    {
        if (string.IsNullOrEmpty(id))
            return req.CreateResponse(HttpStatusCode.BadRequest);

        var result = await _invoiceAppService.DeleteInvoiceAsync(id);
        var response = req.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        return response;
    }
}