using Hangfire;
using HangfireDemo.Api.Model;
using HangfireDemo.Jobs.DelayedCounting;
using HangfireDemo.Jobs.RepeatAfterMe;
using Microsoft.AspNetCore.Mvc;

namespace HangfireDemo.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class JobsController
    : ControllerBase
{
    [HttpPost("DelayedCounting")]
    public async Task<IActionResult> PostDelayedCounting(
        [FromBody] CreateDelayedCountingJobDto dto,
        [FromServices] IBackgroundJobClient jobClient)
    {
        await Task.Yield();
        
        var jobId = jobClient.Enqueue<DelayedCountingJob>(
            job => job.Run(new DelayedCountingJobArgs(dto.Count, dto.Delay))
        );
        
        return Accepted(jobId);
    }
    
    [HttpPut("RepeatAfterMe")]
    public async Task<IActionResult> PostRepeatAfterMe(
        [FromBody] CreateRepeatAfterMeJobDto dto,
        [FromServices] IRecurringJobManager jobManager)
    {
        await Task.Yield();
        
        var jobId = dto.JobId ?? new Random((int)DateTime.UtcNow.Ticks).Next().ToString();
        jobManager.AddOrUpdate<RepeatAfterMeJob>(
            jobId,
            job => job.Run(new RepeatAfterMeJobArgs(dto.Message, dto.Delay, DateTimeOffset.UtcNow)),
            dto.CronPattern ?? Cron.Minutely()
        );
        
        return Ok(jobId);
    }
    
    [HttpDelete("RepeatAfterMe/{jobId}")]
    public IActionResult DeleteRepeatAfterMe(
        string jobId,
        [FromServices] IRecurringJobManager jobManager)
    {
        jobManager.RemoveIfExists(jobId);
        return NoContent();
    }
}