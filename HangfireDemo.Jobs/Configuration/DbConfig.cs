namespace HangfireDemo.Jobs.Configuration;

public record DbConfig
{
    public string Host { get; set; } = "localhost";
    public string Database { get; set; } = "hangfire";
    public string Username { get; set; } = "user";
    public string Password { get; set; } = "password";
    public int Port { get; set; } = 5432;
    
    public string ConnectionString => $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
}