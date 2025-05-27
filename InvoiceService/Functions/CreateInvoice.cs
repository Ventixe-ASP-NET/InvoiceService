using InvoiceService.Models;
using InvoiceService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace InvoiceService.Functions;

public class CreateInvoice
{
    private readonly ILogger<CreateInvoice> _logger;
    private readonly InvoiceAppService _invoiceAppService;

    public CreateInvoice(ILogger<CreateInvoice> logger, InvoiceAppService invoiceAppService)
    {
        _logger = logger;
        _invoiceAppService = invoiceAppService;
    }

    [Function("CreateInvoice")]
    public async Task<HttpResponseData> Create(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoices")] HttpRequestData req)
    {
        if (req.Body == null || req.Body.Length == 0)
        {
            var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badRequestResponse.WriteStringAsync("Request body is empty.");
            return badRequestResponse;
        }

        var formData = await req.ReadFromJsonAsync<AddInvoiceFormData>();
        if (formData == null)
        {
            var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badRequestResponse.WriteStringAsync("Invalid JSON body.");
            return badRequestResponse;
        }

        var result = await _invoiceAppService.AddInvoiceAsync(formData);
        var response = req.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        return response;
    }
}