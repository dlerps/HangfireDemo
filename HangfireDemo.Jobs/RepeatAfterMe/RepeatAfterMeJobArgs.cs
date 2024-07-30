namespace HangfireDemo.Jobs.RepeatAfterMe;

public record RepeatAfterMeJobArgs(string Message, uint Delay, DateTimeOffset ScheduleTimestamp);