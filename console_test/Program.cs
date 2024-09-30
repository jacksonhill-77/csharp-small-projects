using System;
using Serilog;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(new Serilog.Formatting.Json.JsonFormatter())
            .WriteTo.File(
            // JSON makes it structured
                new Serilog.Formatting.Json.JsonFormatter(),
                "logs/myapp.json",
                rollingInterval: RollingInterval.Day)
            .CreateLogger();

        Log.Information("Hello, world!");

        int a = 10, b = 0;
        try
        {
            Log.Debug("Dividing {A} by {B}", a, b);
            Console.WriteLine(a / b);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Something went wrong");
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}
