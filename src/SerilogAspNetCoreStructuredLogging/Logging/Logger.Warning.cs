using Microsoft.Extensions.Logging;

namespace Whds.Packing.Mainframe.Integration.Logging;

public static partial class Log
{
    //Warning 21,200-21,299
    
    [LoggerMessage(
        EventId = 21_200,
        Level = LogLevel.Warning,
        Message = "[{Request}] failed as the data service was unavailable")]
    public static partial void LogDependencyUnavailableWarning(
        this ILogger logger,
        string request);
    
    [LoggerMessage(
        EventId = 21_201,
        Level = LogLevel.Warning,
        Message = "[GetOrder] request for order {OrderId} failed since the order was not found")]
    public static partial void LogOrderNotFoundWarning(
        this ILogger logger,
        Guid orderId);


}