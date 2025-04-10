using Azure.AI.TextAnalytics;
using Azure;
using Microsoft.Extensions.Configuration;
using Azure.AI.Vision.ImageAnalysis;

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
//Console.WriteLine(GetLanguage(text, key,endpoint));
AnalyzeImage(endpoint, key);
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

static void AnalyzeImage(string endpoint, string key)
{

    ImageAnalysisClient client = new ImageAnalysisClient(
        new Uri(endpoint),
        new AzureKeyCredential(key));
    ImageAnalysisResult result = client.Analyze(new Uri("https://portal.vision.cognitive.azure.com/dist/assets/ImageTaggingSample3-f5d27a36.jpg"),
        VisualFeatures.Caption | VisualFeatures.Read,
        new ImageAnalysisOptions { GenderNeutralCaption = true });

    Console.WriteLine("Image analysis results:");
    Console.WriteLine(" Caption:");
    Console.WriteLine($"   '{result.Caption.Text}', Confidence {result.Caption.Confidence:F4}");

    foreach (DetectedTextBlock block in result.Read.Blocks)
        foreach (DetectedTextLine line in block.Lines)
        {
            Console.WriteLine($"   Line: '{line.Text}");
            foreach (DetectedTextWord word in line.Words)
            {
                Console.WriteLine($"     Word: '{word.Text}', Confidence {word.Confidence.ToString("#.####")}");
            }
        }
}
