using Hangfire;
using Microsoft.Extensions.Logging;

namespace HangfireDemo.Jobs.DelayedCounting;

[Queue("Green")]
public class DelayedCountingJob(ILogger<DelayedCountingJob> logger)
{
    public async Task Run(DelayedCountingJobArgs args)
    {
        for (var count = 0; count <= args.Count; count++)
        {
            await Task.Delay((int)args.Delay);
            logger.LogInformation("The count is: {Count}", count);
        }
    }
}