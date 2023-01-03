using SerilogAspNetCoreStructuredLogging.Domain;

namespace SerilogAspNetCoreStructuredLogging.Services;

public class InMemoryOrdersService : IOrdersService
{
    private static readonly Dictionary<Guid, Order> Orders = new();

    public Task<Order> CreateAsync(string customer, string itemNumber)
    {
        var order = new Order(customer, itemNumber);
        Orders.Add(order.Id, order);
        return Task.FromResult(order);
    }

    public Task<Order?> GetAsync(Guid id) => Orders.TryGetValue(id, out var order)
        ? Task.FromResult<Order?>(order)
        : Task.FromResult((Order?) null);

    public Task<IReadOnlyList<Order>> GetAsync() =>
        Task.FromResult(
            (IReadOnlyList<Order>) Orders.Values.ToList());
}