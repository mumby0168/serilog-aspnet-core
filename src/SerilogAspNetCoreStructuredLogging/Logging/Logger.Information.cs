namespace SerilogAspNetCoreStructuredLogging.Logging;

public static partial class Log
{
    //Info 21,100-21,199
    
    [LoggerMessage(
        EventId = 21_100,
        Level = LogLevel.Information,
        Message = "[CreateOrder] request was invalid with errors {ValidationErrors}")]
    public static partial void LogCreateOrderRequestInvalid(
        this ILogger logger,
        string validationErrors);
    
    [LoggerMessage(
        EventId = 21_101,
        Level = LogLevel.Information,
        Message = "[CreateOrder] request processed for item {ItemNumber} created order {OrderId} for customer {Customer}")]
    public static partial void LogCreateOrderRequestProcessed(
        this ILogger logger,
        string itemNumber,
        Guid orderId,
        string customer);
}