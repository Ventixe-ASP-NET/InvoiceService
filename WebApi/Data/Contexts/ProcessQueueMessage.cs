using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace WebApi.Data.Contexts;

    public class ProcessQueueMessage
{
    [FunctionName("ProcessQueueMessage")]
    public static void Run(
        [ServiceBusTrigger("invoice-queue", Connection = "ServiceBusConnection")] string message,
        ILogger log)
    {
        log.LogInformation($"Message: {message}");
    }
}