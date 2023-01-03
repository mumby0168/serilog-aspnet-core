using SerilogAspNetCoreStructuredLogging.Domain;

namespace SerilogAspNetCoreStructuredLogging.Application;

public interface IOrdersService
{
    Task<Order> CreateAsync(
        string customer,
        string itemNumber);

    Task<Order?> GetAsync(
        Guid id);

    Task<IReadOnlyList<Order>> GetAsync();
}