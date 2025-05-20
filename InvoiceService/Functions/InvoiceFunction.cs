using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using InvoiceService.Services;
using InvoiceService.Models;

namespace InvoiceService.Functions;

public class InvoiceFunction
{
    private readonly ILogger<InvoiceFunction> _logger;
    private readonly InvoiceAppService _invoiceAppService;

    public InvoiceFunction(ILogger<InvoiceFunction> logger, InvoiceAppService invoiceAppService)
    {
        _logger = logger;
        _invoiceAppService = invoiceAppService;
    }

    [Function("CreateInvoice")]
    public async Task<HttpResponseData> Create(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoices")] HttpRequestData req)
    {
        var formData = await req.ReadFromJsonAsync<AddInvoiceFormData>();
        if (formData == null)
            return req.CreateResponse(HttpStatusCode.BadRequest);

        var result = await _invoiceAppService.AddInvoiceAsync(formData);
        var response = req.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        return response;
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

    [Function("UpdateInvoice")]
    public async Task<HttpResponseData> Update(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "invoices")] HttpRequestData req)
    {
        var formData = await req.ReadFromJsonAsync<UpdateInvoiceFormData>();
        if (formData == null)
            return req.CreateResponse(HttpStatusCode.BadRequest);

        var result = await _invoiceAppService.UpdateInvoiceAsync(formData);
        var response = req.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        return response;
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
