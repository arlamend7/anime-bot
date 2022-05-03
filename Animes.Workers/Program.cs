using CDL.Integration.Workers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System.Diagnostics;

public static class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().CreateLogger();

        try
        {
            Log.Information("Iniciando aplicação...");

            var build = BuildWebHost(args);

            var browserExecutable = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";
            Process.Start(browserExecutable, "http://localhost:5000");
            build.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host encerrado de maneira inesperada!");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IWebHost BuildWebHost(string[] args)
    {
        var builder = WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();

        return builder.Build();

    }
    
}

