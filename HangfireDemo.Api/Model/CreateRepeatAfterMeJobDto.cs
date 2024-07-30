namespace HangfireDemo.Api.Model;

public record CreateRepeatAfterMeJobDto(
    string Message,
    uint Delay,
    string? JobId
);