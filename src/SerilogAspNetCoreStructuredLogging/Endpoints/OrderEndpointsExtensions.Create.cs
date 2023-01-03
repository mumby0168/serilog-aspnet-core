using FluentValidation;
using SerilogAspNetCoreStructuredLogging.Application;
using SerilogAspNetCoreStructuredLogging.Contract.Requests;
using SerilogAspNetCoreStructuredLogging.Exceptions;
using SerilogAspNetCoreStructuredLogging.Logging;

namespace SerilogAspNetCoreStructuredLogging.Endpoints;

public partial class OrderEndpointsExtensions
{
    private static async Task<IResult> CreateOrders(
        CreateOrder.Request request,
        IValidator<CreateOrder.Request> validator,
        ILogger<OrderEndpoints> logger,
        IOrdersService ordersService)
    {
        var validationResult = await validator.ValidateAsync(request);
        
        logger.LogCreateOrderRequestReceived(request.ItemNumber);

        if (!validationResult.IsValid)
        {
            var validationErrors = validationResult.ToString();
            
            logger.LogCreateOrderRequestInvalid(validationErrors);
            
            return Results.Problem(validationErrors, statusCode: 400);
        }

        try
        {
            var order = await ordersService.CreateAsync(
                request.Customer,
                request.ItemNumber);
            
            logger.LogCreateOrderRequestProcessed(
                order.ItemNumber,
                order.Id,
                order.Customer);

            return Results.Ok(order.Id);
        }
        catch (DependencyUnavailableException)
        {
            logger.LogDependencyUnavailableWarning(nameof(CreateOrder));
            return Results.StatusCode(503);
        }
        catch (Exception e)
        {
            logger.LogCreateOrderRequestFailed(e);
            return Results.StatusCode(500);
        }
    } 
}