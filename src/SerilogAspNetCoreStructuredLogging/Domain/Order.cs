namespace SerilogAspNetCoreStructuredLogging.Domain;

public class Order
{
    public Guid Id { get; }
    
    public string Customer { get; }
    
    public string ItemNumber { get; }

    public Order(string customer, string itemNumber)
    {
        Id = Guid.NewGuid();
        Customer = customer;
        ItemNumber = itemNumber;
    }
}