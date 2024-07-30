using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HangfireDemo.Jobs.Configuration;

public record HangfireConfig
{
    public static void Configure(IServiceProvider services, IGlobalConfiguration globalConfiguration)
    {
        var config = services.GetRequiredService<IOptions<HangfireConfig>>();
        var logger = services.GetRequiredService<ILogger<HangfireConfig>>();
                
        logger.LogInformation("Using connection string: {0}", config.Value.Database.ConnectionString);

        globalConfiguration
            .UsePostgreSqlStorage(
                opt => opt.UseNpgsqlConnection(config.Value.Database.ConnectionString))
            .UseRecommendedSerializerSettings()
            .UseSimpleAssemblyNameTypeSerializer()
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
    }
    
    public DbConfig Database { get; set; } = new();
}