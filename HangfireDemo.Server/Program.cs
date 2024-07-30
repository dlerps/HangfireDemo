using Hangfire;
using HangfireDemo.Jobs.Configuration;
using HangfireDemo.Server.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder
            .AddJsonFile("appsettings.json", false, false)
            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, false)
            .AddJsonFile($"appsettings.local.json", true, false)
            .AddEnvironmentVariables();
    })
    .ConfigureServices(services =>
    {
        services.AddHangfire(HangfireConfig.Configure);
        services.AddHangfireServer((provider, options) =>
        {
            var hfServerConfig = provider.GetRequiredService<HangfireServerConfig>();
            options.Queues = hfServerConfig.Queues
                .Select(q => q.ToString())
                .ToArray();
        });
    })
    .Build();
    
try
{
    await host.StartAsync();
}
catch (OperationCanceledException)
{
    Console.WriteLine("Stopping job server...");
}