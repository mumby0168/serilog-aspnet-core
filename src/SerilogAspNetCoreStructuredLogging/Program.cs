using FluentValidation;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using SerilogAspNetCoreStructuredLogging.Application;
using SerilogAspNetCoreStructuredLogging.Endpoints;

Log.Logger = new LoggerConfiguration()
    .WriteTo
    .Console()
    .CreateBootstrapLogger();

Log.Information("Application starting");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // setup application insights default telemetry
    builder.Services.AddApplicationInsightsTelemetry();

    builder.Host.UseSerilog((_, serviceProvider, loggerConfiguration) =>
    {
        // write to application insights as trace logs
        // recommended approach to re-use the same instance of TelemetryConfiguration as the AI SDK.
        loggerConfiguration
            .WriteTo
            .ApplicationInsights(
                serviceProvider.GetRequiredService<TelemetryConfiguration>(),
                TelemetryConverter.Traces);

        loggerConfiguration
            .WriteTo
            .Console();

        // read serilog config block from IConfiguration
        // reads things such as log levels and filter rules
        loggerConfiguration
            .ReadFrom
            .Configuration(builder.Configuration);
    });

    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen(options => options.CustomSchemaIds(s => s.FullName?.Replace("+", ".")));

    builder.Services
        .AddHealthChecks()
        .AddCheck("default", _ => HealthCheckResult.Healthy());

    builder.Services
        .AddValidatorsFromAssembly(typeof(Program).Assembly);

    builder.Services
        .AddSingleton<IOrdersService, InMemoryOrdersService>()
        .Decorate<IOrdersService, OrderServiceDisruptorDecorator>();

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    app
        .MapGet("/", () => Results.Redirect("/swagger"))
        .ExcludeFromDescription();

    app
        .MapOrderEndpoints();

    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Application failed to start");
}