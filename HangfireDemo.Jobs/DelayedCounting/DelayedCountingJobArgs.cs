namespace HangfireDemo.Jobs.DelayedCounting;

public record DelayedCountingJobArgs(uint Delay, uint Count);