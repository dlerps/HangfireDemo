using System.Text;
using Microsoft.Extensions.Configuration;

namespace HangfireDemo.Server.Helper;

public static class ConfigurationHelper
{
    private static void PrintConfigChildren(IEnumerable<IConfigurationSection> sections, StringBuilder sb)
    {
        foreach (var child in sections)
        {
            var children = child.GetChildren().ToList();
            if (children.Any())
                PrintConfigChildren(children, sb);
            else
                sb.AppendLine($"{child.Path} = {child.Value}");
        }
    }

    public static void PrintConfiguration(IConfiguration configuration)
    {
        var sb = new StringBuilder();
        PrintConfigChildren(configuration.GetChildren(), sb);
        Console.WriteLine(sb.ToString());
    }
}