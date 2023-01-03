namespace SerilogAspNetCoreStructuredLogging.Contract.Dtos;

public record OrderDto(
    Guid Id,
    string Customer,
    string ItemNumber);