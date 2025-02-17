using Azure.Monitor.OpenTelemetry.AspNetCore;
using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

const string serviceName = "test-openT";
const string _connestionString = "InstrumentationKey=a2ea3c3f-ad8d-4414-abc7-fd8f89fa3809;IngestionEndpoint=https://canadacentral-1.in.applicationinsights.azure.com/;LiveEndpoint=https://canadacentral.livediagnostics.monitor.azure.com/;ApplicationId=9a84767e-fdd7-4022-979e-d6f709e13627"; ;
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
//{
//    ConnectionString = _connestionString
//});
//builder.Services.AddLogging();

builder.Services.AddHttpClient();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddAspNetCoreInstrumentation(options => 
    {
        options.EnrichWithHttpRequest = (activity, httpRequest) =>
        {
            //if (eventName == "OnStartActivity" && rawObject is HttpRequest httpRequest)
            //{
            activity.DisplayName = $"{httpRequest.Method} {httpRequest.Path}";
            activity.SetTag("Operation_Path", $"{httpRequest.Method} {httpRequest.Path}");
            //}
        };
    }).AddHttpClientInstrumentation())
    .UseAzureMonitor(opt => opt.ConnectionString = _connestionString);

builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true;
    logging.IncludeScopes = true;
    logging.ParseStateValues = true;
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseMiddleware<EndpointLoggingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
