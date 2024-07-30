namespace HangfireDemo.Api.Model;

public record CreateDelayedCountingJobDto(
    uint Count,
    uint Delay
);