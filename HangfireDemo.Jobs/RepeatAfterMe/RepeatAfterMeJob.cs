using Hangfire;
using Microsoft.Extensions.Logging;

namespace HangfireDemo.Jobs.RepeatAfterMe;

[Queue("Blue")]
public class RepeatAfterMeJob(ILogger<RepeatAfterMeJob> logger)
{
    public async Task Run(RepeatAfterMeJobArgs args)
    {
        await Task.Delay((int)args.Delay);
        
        logger.LogInformation(
            "The message from {ScheduledTimestamp} is: {Message}",
            args.ScheduleTimestamp,
            args.Message
        );
    }
}