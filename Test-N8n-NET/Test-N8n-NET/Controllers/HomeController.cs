using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Test_N8n_NET.Models;

namespace Test_N8n_NET.Controllers
{
    public class HomeController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        //soon should be invalid
        private const string WebhookUrl = "";
        private const string ChatWebhookUrl = "";

        public IActionResult Index()
        {
            return View(new EmailMessageModel(string.Empty, string.Empty, string.Empty));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(EmailMessageModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var httpClient = _httpClientFactory.CreateClient();

                    var payload = new EmailMessageModel(model.Email, model.Subject, model.Message);

                    var jsonContent = JsonSerializer.Serialize(payload);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(WebhookUrl, httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.SuccessMessage = $"Message sent successfully to {model.Email}! Webhook response: {response.StatusCode}";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"Failed to send message. Webhook response: {response.StatusCode}";
                    }
                }
                catch (HttpRequestException ex)
                {
                    ViewBag.ErrorMessage = $"Network error occurred: {ex.Message}";
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                }

                return View(new EmailMessageModel(string.Empty, string.Empty, string.Empty)); // Reset form
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendChatMessage(ChatMessageDto chatMessage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var httpClient = _httpClientFactory.CreateClient();

                    var payload = new ChatWebhookPayloadDto
                    {
                        ChatInput = chatMessage.Message
                    };

                    var jsonContent = JsonSerializer.Serialize(payload);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(ChatWebhookUrl, httpContent);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.SuccessMessage = $"Message sent successfully! Status: {response.StatusCode}";
                        if (!string.IsNullOrEmpty(responseContent))
                        {
                            ViewBag.WebhookResponse = responseContent;
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"Failed to send message. Status: {response.StatusCode}";
                        ViewBag.WebhookResponse = responseContent;
                    }
                }
                catch (HttpRequestException ex)
                {
                    ViewBag.ErrorMessage = $"Network error: {ex.Message}";
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Error occurred: {ex.Message}";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please provide a valid message.";
            }

            return View("Chat");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
