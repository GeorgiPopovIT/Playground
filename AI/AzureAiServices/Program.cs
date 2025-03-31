using Azure.AI.TextAnalytics;
using Azure;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string? endpoint = config["AIServicesEndpoint"];
string? key = config["AIServicesKey"];
string text  = """
    След разигралите се ексцесии снощи Левски бе наказан с 11 300 лева.
     На полувремето срещу Ботев (Пловдив)  запалянковци от Сектор Б нахлуха на пистата и тръгнаха
     към гостите.  "Канарчетата" пък са наказани с 11 000 лева и предупреждение
     за лишаване от домакинство. Също така "жълто-черните" ще трябва да възстановят щетите
     на стадион „Георги Аспарухов“, причинени по време на равенството 1:1.
     ЦСКА ще трябва да плати 4300 лева за поведението на феновете в мача срещу Септември.
""";
Console.WriteLine(GetLanguage(text, key,endpoint));
static string GetLanguage(string text, string key, string aiEndpoint)
{

    // Create client using endpoint and key
    AzureKeyCredential credentials = new AzureKeyCredential(key);
    Uri endpoint = new Uri(aiEndpoint);
    var client = new TextAnalyticsClient(endpoint, credentials);

    // Call the service to get the detected language
    DetectedLanguage detectedLanguage = client.DetectLanguage(text);
    return (detectedLanguage.Name);

}
