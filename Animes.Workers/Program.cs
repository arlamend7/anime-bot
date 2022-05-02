using CDL.Integration.Workers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

public static class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().CreateLogger();

        try
        {
            Log.Information("Iniciando aplicação...");

            BuildWebHost(args).Run();
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

