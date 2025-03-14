namespace Deukuway.Core.Ports;

/// <summary>
///  Port  for logging throughout the application
/// </summary>
public interface ILoggerService
{
    /// <summary>
    /// Logs information message
    /// </summary>
    /// <param name="serviceName">Name of the service/component logging the message</param>
    /// <param name="message">The log message</param>
    /// <param name="properties">Optional dictionary of additional properties</param>
    void LogInfo(string serviceName, string message, Dictionary<string, object>? properties = null);

    /// <summary>
    /// Logs warning message
    /// </summary>
    /// <param name="serviceName">Name of the service/component logging the message</param>
    /// <param name="message">The log message</param>
    /// <param name="properties">Optional dictionary of additional properties</param>
    void LogWarn(string serviceName, string message, Dictionary<string, object>? properties = null);

    /// <summary>
    /// Logs error message
    /// </summary>
    /// <param name="serviceName">Name of the service/component logging the message</param>
    /// <param name="message">The log message</param>
    /// <param name="error">Optional error object or stack trace</param>
    /// <param name="properties">Optional dictionary of additional properties</param>
    void LogError(string serviceName, string message, Exception? error = null, Dictionary<string, object>? properties = null);

    /// <summary>
    /// Logs debug message
    /// </summary>
    /// <param name="serviceName">Name of the service/component logging the message</param>
    /// <param name="message">The log message</param>
    /// <param name="properties">Optional dictionary of additional properties</param>
    void LogDebug(string serviceName, string message, Dictionary<string, object>? properties = null);
}