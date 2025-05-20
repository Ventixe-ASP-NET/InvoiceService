using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace InvoiceService.Data.Contexts;

public class ProcessQueueMessage
{
    private readonly ILogger<ProcessQueueMessage> _logger;

    public ProcessQueueMessage(ILogger<ProcessQueueMessage> logger)
    {
        _logger = logger;
    }

    [Function("ProcessQueueMessage")]
    public void Run(
        [ServiceBusTrigger("invoice-queue", Connection = "ServiceBusConnection")] string message)
    {
        _logger.LogInformation($"Message: {message}");
    }
}
