using Deukuway.Core.Ports;

namespace Deukuway.Infrastructure.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.IO;
using Deukuway.Infrastructure.Logging;

/// <summary>
/// Extension methods for configuring logging
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Configures Serilog logging service
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configuration">The application configuration</param>
    public static void ConfigureLoggingService(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Ensure logs directory exists
        // Load logs path from .env or use default
        string logsPath = Environment.GetEnvironmentVariable("LOGS_PATH") 
                          ?? configuration["LogsPath"] 
                          ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        Console.WriteLine($"LogsPath: {logsPath}");
        if (!Directory.Exists(logsPath))
        {
            Directory.CreateDirectory(logsPath);
            Console.WriteLine($"Created logs directory: {logsPath}");
        }
        
        // Create Serilog logger configuration
        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithThreadId();

        // Get environment
        var env =
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
            "Development";

        // Console sink (pretty-printed in Development, JSON in Production)
        if (env.Equals("Development", StringComparison.OrdinalIgnoreCase))
        {
            loggerConfig.WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level:u3}] ({ServiceName}) {Message:lj}{NewLine}{Exception}");
        }
        else
        {
            loggerConfig.WriteTo.Console(new JsonFormatter());
        }

        // File sink for JSON logs
        if (!Directory.Exists(logsPath))
            Directory.CreateDirectory(logsPath);

        loggerConfig.WriteTo.File(
            new JsonFormatter(),
            Path.Combine(logsPath, "deukuway-.log"),
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 30);

        // Add Seq Sink for structured logging to centralized server if configured
        string seqServerUrl = Environment.GetEnvironmentVariable("SEQ_URL") ??
                              configuration["Seq:ServerUrl"] ??
                              "http://localhost:5341";

        if (!string.IsNullOrEmpty(seqServerUrl))
        {
            loggerConfig.WriteTo.Seq(seqServerUrl);
            Console.WriteLine($"Configured Seq logging to: {seqServerUrl}");
        }

        // Create the logger instance
        var logger = loggerConfig.CreateLogger();

        // Set as the static logger
        Log.Logger = logger;

        // Register the logger service
        services.AddSingleton<Serilog.ILogger>(logger);
        services.AddScoped<ILoggerService, LoggerServiceAdapter>();
    }

    /// <summary>
    /// Closes and flushes the logger
    /// </summary>
    public static void CloseAndFlush()
    {
        Log.CloseAndFlush();
    }
}