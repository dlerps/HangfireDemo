using Hangfire;
using HangfireDemo.Jobs.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddRouting();

// Enable Hangfire Jobs
builder.Services.AddHangfire(HangfireConfig.Configure);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

app.MapControllers();
app.Run();
