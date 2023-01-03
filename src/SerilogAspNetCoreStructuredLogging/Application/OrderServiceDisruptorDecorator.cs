using System.Security.Cryptography;
using SerilogAspNetCoreStructuredLogging.Domain;
using SerilogAspNetCoreStructuredLogging.Exceptions;

namespace SerilogAspNetCoreStructuredLogging.Services;

public class OrderServiceDisruptorDecorator : IOrdersService
{
    private readonly IOrdersService _ordersService;
    private readonly bool _isEnabled;

    public OrderServiceDisruptorDecorator(
        IConfiguration configuration,
        IOrdersService ordersService)
    {
        _isEnabled = configuration.GetValue<bool>("IsDisruptorEnabled");
        _ordersService = ordersService;
    }

    private static void Disrupt()
    {
        if(RandomNumberGenerator.GetInt32(0, 50) % 2 == 0)
        {
            throw new DependencyUnavailableException();
        }
    }

    public Task<Order> CreateAsync(string customer, string itemNumber)
    {
        if (_isEnabled)
        {
            Disrupt();
        }

        return _ordersService.CreateAsync(customer, itemNumber);
    }

    public Task<Order?> GetAsync(Guid id)
    {
        if (_isEnabled)
        {
            Disrupt();
        }

        return _ordersService.GetAsync(id);
    }

    public Task<IReadOnlyList<Order>> GetAsync()
    {
        if (_isEnabled)
        {
            Disrupt();
        }

        return _ordersService.GetAsync();
    }
}