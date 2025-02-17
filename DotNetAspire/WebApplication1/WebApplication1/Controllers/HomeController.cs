using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       // private readonly TelemetryClient telemetryClient;
        private readonly IHttpClientFactory httpClientFactory;
        public HomeController(ILogger<HomeController> logger,
           // TelemetryClient telemetryClient, 
            IHttpClientFactory httpClientFactory)
        {
            this._logger = logger;
           // this.telemetryClient = telemetryClient;
            this.httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("In Home Controller");
            return View();
        }

        public IActionResult Privacy()
        {
            Dictionary<string, string> dict = new()
            {
                {"Provider", "Paypal" },
                {"Status" , "1" },
                {"ClientId", "1423" }
            };

          // telemetryClient.TrackTrace("Privacy", SeverityLevel.Information, dict);

            return View();
        }

        public IActionResult Contact()
        {
            var client = httpClientFactory.CreateClient();
            var response = client.GetAsync("https://www.google.com").Result;

            _logger.LogInformation(response.StatusCode.ToString(),response);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
