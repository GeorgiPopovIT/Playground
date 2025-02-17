namespace WebApplication1;

public class EndpointLoggingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, ILogger<EndpointLoggingMiddleware> logger)
    {
        // Get the endpoint name (route path)
        var endpoint = context.GetEndpoint();
        var endpointName = endpoint?.DisplayName ?? "Unknown Endpoint";

        // Add the endpoint name to the logging scope
        using (logger.BeginScope(new Dictionary<string, object> { ["EndpointName"] = endpointName }))
        {
            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
