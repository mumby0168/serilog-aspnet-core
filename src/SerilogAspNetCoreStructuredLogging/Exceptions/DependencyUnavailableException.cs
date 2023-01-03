namespace SerilogAspNetCoreStructuredLogging.Exceptions;

public class DependencyUnavailableException : Exception
{
    public DependencyUnavailableException() : base("The data persistence service is unavailable")
    {
        
    }
}