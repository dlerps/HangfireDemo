using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.DependencyInjection;

namespace HangfireDemo.Jobs.Configuration;

public record HangfireConfig
{
    public static void Configure(IServiceProvider services, IGlobalConfiguration globalConfiguration)
    {
        var config = services.GetRequiredService<HangfireConfig>();
        globalConfiguration
            .UsePostgreSqlStorage(opt => opt.UseNpgsqlConnection(config.Database.ConnectionString))
            .UseRecommendedSerializerSettings()
            .UseSimpleAssemblyNameTypeSerializer()
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
    }
    
    public DbConfig Database { get; set; } = new();
}