using FluentValidation;

namespace SerilogAspNetCoreStructuredLogging.Contract.Requests;

public static class CreateOrder
{
    public record Request(
        string Customer,
        string ItemNumber);

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.ItemNumber)
                .MinimumLength(2)
                .NotEmpty();
            
            RuleFor(x => x.Customer)
                .MinimumLength(5)
                .NotEmpty();
        }
    }
}

