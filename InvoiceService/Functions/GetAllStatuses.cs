using InvoiceService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace InvoiceService.Functions;

public class GetAllStatuses
{
    private readonly ILogger<GetAllStatuses> _logger;
    private readonly StatusAppService _statusAppService;

    public GetAllStatuses(ILogger<GetAllStatuses> logger, StatusAppService statusAppService)
    {
        _logger = logger;
        _statusAppService = statusAppService;
    }

    [Function("GetAllStatuses")]
    public async Task<HttpResponseData> GetAll(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "statuses")] HttpRequestData req)
    {
        var statuses = await _statusAppService.GetAllStatusesAsync();
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(statuses);
        return response;
    }
}