namespace HangfireDemo.Server.Configuration;

public record HangfireServerConfig : HangfireConfig
{
    public ColorQueue[] Queues { get; set; } = [ ];
    public string Name => $"{String.Join('-', Queues.Select(q => q.ToString()))}-{Guid.NewGuid()}";
}