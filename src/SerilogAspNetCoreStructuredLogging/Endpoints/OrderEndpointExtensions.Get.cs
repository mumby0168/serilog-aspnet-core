using SerilogAspNetCoreStructuredLogging.Application;
using SerilogAspNetCoreStructuredLogging.Contract.Dtos;
using SerilogAspNetCoreStructuredLogging.Exceptions;
using SerilogAspNetCoreStructuredLogging.Logging;

namespace SerilogAspNetCoreStructuredLogging.Endpoints;

public partial class OrderEndpointsExtensions
{
    private static IResult GetOrders(
        ILogger<OrderEndpoints> logger,
        IOrdersService ordersService)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
        return Results.Ok();
    }

    private static async Task<IResult> GetOrder(
        Guid id,
        ILogger<OrderEndpoints> logger,
        IOrdersService ordersService)
    {
        logger.LogGetOrderRequestReceived(id);
        
        try
        {
            var order = await ordersService.GetAsync(id);

            if (order is null)
            {
                logger.LogOrderNotFoundWarning(id);
                return Results.NotFound();
            }

            return Results.Ok(new OrderDto(
                order.Id,
                order.Customer,
                order.ItemNumber));
        }
        catch (DependencyUnavailableException)
        {
            logger.LogDependencyUnavailableWarning(nameof(GetOrder));
            return Results.StatusCode(503);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.StatusCode(500);
        }
    }
}