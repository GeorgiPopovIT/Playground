using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Logging.AddConsole();

//builder.Logging.AddOpenTelemetry(logging =>
//{
//    logging.IncludeScopes = true;
//    logging.IncludeFormattedMessage = true;

//    logging.AddOtlpExporter(options =>
//           {
//               options.Endpoint = new Uri("https://otlp-gateway-prod-eu-central-0.grafana.net/otlp");
//               options.Headers = "Authorization=Basic MTMxMDE4NDpnbGNfZXlKdklqb2lNVFEzTnpNMk1DSXNJbTRpT2lKMFpYTjBJaXdpYXlJNklrdzFOWFp2U0ZGa05qSTJlbTlRV1Rac05UWXpkRFYwUWlJc0ltMGlPbnNpY2lJNkluQnliMlF0WlhVdFkyVnVkSEpoYkMwd0luMTk=";
//           });
//});

//builder.Services.AddOpenTelemetry()
//    .WithTracing(tracingBuilder =>
//    {
//        tracingBuilder.AddAspNetCoreInstrumentation()
//                      .AddHttpClientInstrumentation()
//                      .AddSource("GrafanaOpenTelemetry")
//                      .SetResourceBuilder(ResourceBuilder.CreateDefault()
//                        .AddService("GrafanaOpenTelemetryService"))
//                      .AddOtlpExporter(options =>
//                        {
//                            options.Endpoint = new Uri("https://otlp-gateway-prod-eu-central-0.grafana.net/otlp");
//                            options.Headers = "Authorization=Basic MTMxMDE4NDpnbGNfZXlKdklqb2lNVFEzTnpNMk1DSXNJbTRpT2lKMFpYTjBJaXdpYXlJNklrdzFOWFp2U0ZGa05qSTJlbTlRV1Rac05UWXpkRFYwUWlJc0ltMGlPbnNpY2lJNkluQnliMlF0WlhVdFkyVnVkSEpoYkMwd0luMTk=";
//                            options.Protocol = OtlpExportProtocol.Grpc;
//                        });
//    })
//    .WithMetrics(metricsBuilder =>
//    {
//        metricsBuilder.AddAspNetCoreInstrumentation()
//                      .AddHttpClientInstrumentation()
//                      .AddOtlpExporter(options =>
//                      {
//                          options.Endpoint = new Uri("https://otlp-gateway-prod-eu-central-0.grafana.net/otlp");
//                          options.Headers = "Authorization=Basic MTMxMDE4NDpnbGNfZXlKdklqb2lNVFEzTnpNMk1DSXNJbTRpT2lKMFpYTjBJaXdpYXlJNklrdzFOWFp2U0ZGa05qSTJlbTlRV1Rac05UWXpkRFYwUWlJc0ltMGlPbnNpY2lJNkluQnliMlF0WlhVdFkyVnVkSEpoYkMwd0luMTk=";
//                          options.Protocol = OtlpExportProtocol.Grpc;
//                      });
//    })
//    ;//.UseOtlpExporter();

builder.Services
    .AddOpenTelemetry()
    //.ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation()
            .AddRedisInstrumentation();
           // .AddNpgsql();

        tracing.AddOtlpExporter();
    });

builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeScopes = true;
    logging.IncludeFormattedMessage = true;

    logging.AddOtlpExporter();
});



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (ILogger<Program> logger) =>
{
    logger.LogInformation("Weather forecast requested at {Time}", DateTime.Now);
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
