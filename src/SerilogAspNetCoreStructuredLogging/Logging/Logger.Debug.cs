using Microsoft.Extensions.Logging;

namespace Whds.Packing.Mainframe.Integration.Logging;

public static partial class Log
{
    //Debug/Verbose 21,000-21,099
    
    [LoggerMessage(
        EventId = 21_000,
        Level = LogLevel.Debug,
        Message = "[CreateOrder] request received for item {ItemNumber}")]
    public static partial void LogCreateOrderRequestReceived(
        this ILogger logger,
        string itemNumber);
    
    [LoggerMessage(
        EventId = 21_001,
        Level = LogLevel.Debug,
        Message = "[GetOrder] request received for order {OrderId}")]
    public static partial void LogGetOrderRequestReceived(
        this ILogger logger,
        Guid orderId);
}