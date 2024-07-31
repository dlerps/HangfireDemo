using Hangfire;
using HangfireDemo.Server.Configuration;
using HangfireDemo.Server.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(c => c.AddJsonFile("appsettings.json", false, false)
        .AddJsonFile("appsettings.local.json", true, false)
        .AddEnvironmentVariables()
    )
    .ConfigureServices((context, services) =>
    {
        ConfigurationHelper.PrintConfiguration(context.Configuration);
        services
            .Configure<HangfireServerConfig>(context.Configuration.GetSection("Hangfire"))
            .Configure<HangfireConfig>(context.Configuration.GetSection("Hangfire"))
            .AddLogging(log => log.ClearProviders().AddConsole())
            .AddHangfire(HangfireConfig.Configure)
            .AddHangfireServer((provider, options) =>
            {
                var hfServerConfig = provider.GetRequiredService<IOptions<HangfireServerConfig>>();
                
                options.HeartbeatInterval = TimeSpan.FromSeconds(30);
                options.ServerName = hfServerConfig.Value.Name;
                options.Queues = hfServerConfig.Value.Queues
                    .Select(q => q.ToString())
                    .ToArray();
            });
    })
    .Build();

try
{
    await host.RunAsync();
}
catch (OperationCanceledException)
{
    Console.WriteLine("Stopping job server...");
}