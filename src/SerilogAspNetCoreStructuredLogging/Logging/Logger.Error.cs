namespace SerilogAspNetCoreStructuredLogging.Logging;

public static partial class Log
{
    //Error 21,300-21,399
    
    [LoggerMessage(
        EventId = 21_300,
        Level = LogLevel.Error,
        Message = "[CreateOrder] request failed to process because of a unhandled error")]
    public static partial void LogCreateOrderRequestFailed(
        this ILogger logger,
        Exception e);
}