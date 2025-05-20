using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using InvoiceService.Services;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace InvoiceService.Functions;

public class StatusFunction
{
    private readonly ILogger<StatusFunction> _logger;
    private readonly StatusAppService _statusAppService;

    public StatusFunction(ILogger<StatusFunction> logger, StatusAppService statusAppService)
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