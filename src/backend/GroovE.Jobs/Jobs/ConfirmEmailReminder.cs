using GroovE.Jobs.Common;
using Microsoft.Extensions.Logging;

namespace GroovE.Jobs.Jobs;

internal class ConfirmEmailReminder(ILogger<ConfirmEmailReminder> logger) : IJob
{
    public async Task RunAsync()
    {
        logger.LogInformation("Running ConfirmEmailReminder job!");
        await Task.CompletedTask;
    }
}
