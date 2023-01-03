namespace SerilogAspNetCoreStructuredLogging.Endpoints;

public static partial class OrderEndpointsExtensions
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder builder)
    {
        var ordersEndpoints = builder.MapGroup("/orders");

        ordersEndpoints.MapGet(
            "/",
            GetOrders);
        
        ordersEndpoints.MapGet(
            "/{id}",
            GetOrder);

        ordersEndpoints.MapPost(
            "/",
                CreateOrders);

        return builder;
    }

    private record OrderEndpoints;
}