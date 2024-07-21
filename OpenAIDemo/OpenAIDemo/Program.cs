using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var secretProvider = config.Providers.First();

secretProvider.TryGet("OpenAI_API_KEY", out var apiKey);

HttpClient httpClient = new HttpClient();
const string ApiKey = "your-api-key";
const string ApiUrl = "https://api.openai.com/v1/chat/completions";
const string CompanyName = "Microsoft Corporation";
const string CompanyWebsite = "www.microsoft.com";


httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
httpClient.Timeout = TimeSpan.FromSeconds(120);

Console.WriteLine("Please enter your questions (type 'exit' to quit):");
Console.WriteLine();
Console.WriteLine("==>");

List<Message> messages = new()
        {
            new Message("system", $"You are an assistant that only provides information about {CompanyName} and its service and its website, {CompanyWebsite}. Do not talk about anything else. If you are asked to diverge, simply use variations on this text: 'I am sorry but I can only talk about {CompanyName}, its services, and its website.")
        };

while (true)
{
    string userInput = Console.ReadLine();
    if (userInput.ToLower() == "exit") break;

    messages.Add(new Message("user", userInput));

    var data = new
    {
        model = "gpt-3.5-turbo",
        messages,
        max_tokens = 1000,
        temperature = 0.3,
    };

    HttpResponseMessage response = await httpClient.PostAsJsonAsync(ApiUrl, data);
    if (response.IsSuccessStatusCode)
    {
        JsonElement jsonResponse = await JsonSerializer.DeserializeAsync<JsonElement>(await response.Content.ReadAsStreamAsync());
        string answer = jsonResponse.GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString()?
            .Trim() ?? string.Empty;

        messages.Add(new Message("assistant", answer));

    }
    else
    {
        Console.WriteLine("Error: " + response.StatusCode);
    }

    Console.WriteLine(response);
    Console.WriteLine();
    Console.WriteLine("==>");
}

record Message(string Role, string Content);
