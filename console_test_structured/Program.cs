using System;
using Serilog;

class Program
{ 
    static async Task Main()
    {
        /*
        - assign a LoggerConfiguration instance to the Log's Logger property - this is the root Logger
        - done at application start up 
        - causes the logger to be globally accessible - it is a static property
        - the below is the configuration of a log event sink, i.e. where output is logged to
        - it creates an interface for creating a logging pipeline
         */
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            // adds the console sink + file sink to the pipeline, meaning it will output to console + file
            // this is why we install the package Serilog.Sinks.Console + Serilog.Sinks.File
            .WriteTo.Console() 
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            // assembles the pipeline and returns a Logger object which becomes assigned to Log.Logger
            .CreateLogger();

        // the equivalent of something like Console.WriteLine
        // lets you output text to the log, which can be captured in a sink
        Log.Information("Hello, world!");

        int a = 10, b = 0;
        try
        {
            // debug is the 5th highest priority message, used to show the internal state of the app during development
            Log.Debug("Dividing {A} by {B}", a, b);
            Console.WriteLine(a / b);
        }
        // exception class - this is created when the try block failed
        catch (Exception ex)
        {
            // error is the 2nd highest priority message logged from Serilog - for parts that stop the app working as expected
            // "ex" will include info such as the stack trace and the message from system - serilog is just the messenger
            Log.Error(ex, "Something went wrong");
        }
        finally
        {
            // await - asynchronously waits for a thread to finish
            await Log.CloseAndFlushAsync();
        }
    }
}