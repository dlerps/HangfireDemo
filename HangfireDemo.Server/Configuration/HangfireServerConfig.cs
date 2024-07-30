using HangfireDemo.Jobs.Configuration;

namespace HangfireDemo.Server.Configuration;

public record HangfireServerConfig : HangfireConfig
{
    public ColorQueue[] Queues { get; set; } = [ ColorQueue.Blue, ColorQueue.Green ];
}