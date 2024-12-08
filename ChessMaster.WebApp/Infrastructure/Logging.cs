using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace ChessMaster.WebApp.Infrastructure;

public static class Logging
{
    private static LoggingLevelSwitch LoggingLevelSwitch { get; } = new ();

    private static bool IsLoggerInitialized { get; set; }

    public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
    {
        var logger = CreateLogger();
        TrySwitchLogLevelFromConfiguration(builder.Configuration);
        builder.Host.UseSerilog(logger);
        return builder;
    }

    private static ILogger CreateLogger()
    {
        var loggingPath = CreateLoggingPath();
        var logger = new LoggerConfiguration().MinimumLevel.ControlledBy(LoggingLevelSwitch)
                                              .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                              .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                                              .WriteTo.File(loggingPath, rollingInterval: RollingInterval.Day)
                                              .WriteTo.Console()
                                              .CreateLogger();
        IsLoggerInitialized = true;
        Log.Logger = logger;
        return logger;
    }

    public static ILogger GetStartupEmergencyLogger() => IsLoggerInitialized ?
        Log.Logger :
        new LoggerConfiguration().WriteTo.File("startupError.log").WriteTo.Console().CreateLogger();

    private static string CreateLoggingPath()
    {
        var loggingDirectory = Path.Combine(Path.GetDirectoryName(typeof(Logging).Assembly.Location)!, "logs");
        var loggingPath = Path.Combine(loggingDirectory, "ChessMaster.log");
        return loggingPath;
    }

    private static void TrySwitchLogLevelFromConfiguration(this IConfiguration configuration)
    {
        if (Enum.TryParse(configuration["logLevel"], true, out LogEventLevel logLevel))
            LoggingLevelSwitch.MinimumLevel = logLevel;
    }
}