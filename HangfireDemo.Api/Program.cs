using Hangfire;
using HangfireDemo.Jobs.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddRouting();
builder.Services.AddLogging(log => log.ClearProviders().AddConsole());
builder.Services.Configure<HangfireConfig>(builder.Configuration.GetSection("Hangfire"));

// Enable Hangfire Jobs
builder.Services.AddHangfire(HangfireConfig.Configure);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHangfireDashboard(
    "/jobs",
    new DashboardOptions
    {
        DashboardTitle = "Hangfire Demo",
        DarkModeEnabled = true,
        DisplayStorageConnectionString = true,
        IgnoreAntiforgeryToken = true,
        Authorization = [],
        AsyncAuthorization = []
    }
);

app.UseRouting();
app.UseEndpoints(ep => ep.MapControllers());
app.Run();
