using InvoiceService.Models;
using InvoiceService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace InvoiceService.Functions;

public class UpdateInvoice
{
    private readonly ILogger<UpdateInvoice> _logger;
    private readonly InvoiceAppService _invoiceAppService;

    public UpdateInvoice(ILogger<UpdateInvoice> logger, InvoiceAppService invoiceAppService)
    {
        _logger = logger;
        _invoiceAppService = invoiceAppService;
    }

    [Function("UpdateInvoice")]
    public async Task<HttpResponseData> Update(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "invoices/{id}")] HttpRequestData req,
        string id)
    {
        var formData = await req.ReadFromJsonAsync<UpdateInvoiceFormData>();
        if (formData == null)
        {
            var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badRequestResponse.WriteStringAsync("Invalid or missing request body.");
            return badRequestResponse;
        }

        // Garante que o Id do parâmetro seja usado
        formData.Id = id;

        var result = await _invoiceAppService.UpdateInvoiceAsync(formData);
        var response = req.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        return response;
    }
}