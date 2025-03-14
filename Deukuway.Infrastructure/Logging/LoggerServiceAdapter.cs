using Deukuway.Core.Ports;
using Serilog;
using Serilog.Events;


namespace Deukuway.Infrastructure.Logging;

public class LoggerServiceAdapter : ILoggerService
{
    private readonly ILogger _logger;

    public LoggerServiceAdapter(ILogger logger)
    {
        _logger = logger;
    }

    public void LogInfo(string serviceName, string message,
        Dictionary<string, object>? properties = null)
    {
        LogWithContext(LogEventLevel.Information, serviceName, message, null,
            properties);
    }

    public void LogWarn(string serviceName, string message,
        Dictionary<string, object>? properties = null)
    {
        LogWithContext(LogEventLevel.Warning, serviceName, message, null,
            properties);
    }

    public void LogError(string serviceName, string message,
        Exception? error = null, Dictionary<string, object>? properties = null)
    {
        LogWithContext(LogEventLevel.Error, serviceName, message, error,
            properties);
    }

    public void LogDebug(string serviceName, string message,
        Dictionary<string, object>? properties = null)
    {
        LogWithContext(LogEventLevel.Debug, serviceName, message, null,
            properties);
    }

    private void LogWithContext(LogEventLevel level, string serviceName,
        string message,
        Exception? exception = null,
        Dictionary<string, object>? properties = null)
    {
        // Start with a logger context that includes the service name
        var contextLogger = _logger.ForContext("ServiceName", serviceName);

        // Add all additional properties if provided
        if (properties != null)
        {
            foreach (var property in properties)
            {
                contextLogger =
                    contextLogger.ForContext(property.Key, property.Value);
            }
        }

        // Log with the appropriate level
        switch (level)
        {
            case LogEventLevel.Debug:
                contextLogger.Debug(message);
                break;
            case LogEventLevel.Information:
                contextLogger.Information(message);
                break;
            case LogEventLevel.Warning:
                contextLogger.Warning(message);
                break;
            case LogEventLevel.Error:
                if (exception != null)
                    contextLogger.Error(exception, message);
                else
                    contextLogger.Error(message);
                break;
            default:
                contextLogger.Write(level, message);
                break;
        }
    }
}