using Azure.AI.TextAnalytics;
using Azure;
using Microsoft.Extensions.Configuration;
using Azure.AI.Vision.ImageAnalysis;
using System.IO;
using System.Drawing;
using System.Net;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string? endpoint = config["AIServicesEndpoint"];
string? key = config["AIServicesKey"];
string text = """
    След разигралите се ексцесии снощи Левски бе наказан с 11 300 лева.
     На полувремето срещу Ботев (Пловдив)  запалянковци от Сектор Б нахлуха на пистата и тръгнаха
     към гостите.  "Канарчетата" пък са наказани с 11 000 лева и предупреждение
     за лишаване от домакинство. Също така "жълто-черните" ще трябва да възстановят щетите
     на стадион „Георги Аспарухов“, причинени по време на равенството 1:1.
     ЦСКА ще трябва да плати 4300 лева за поведението на феновете в мача срещу Септември.
""";
//Console.WriteLine(GetLanguage(text, key,endpoint));
//AnalyzeImage(endpoint, key);
ExtractKeyPhases(endpoint, key);
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

    BinaryData imageData = BinaryData.FromBytes(File.ReadAllBytes(@"../../../../../../../../Downloads/Text_entropy.png"));

    ImageAnalysisResult result = client.Analyze(imageData,
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

static void ExtractKeyPhases(string aiEndpoint, string key)
{
    string document = """
                Liverpool are six points away from sealing a 20th Premier League title after Virgil van Dijk went from zero to hero with a late header to seal a 2-1 win over West Ham.
        With Liverpool winning 1-0 thanks to Luis Diaz's first-half opener from a stunning Mohamed Salah assist, Van Dijk got involved in an awful mix-up with Andy Robertson for the latter to score an own goal in the 86th minute.
        But moments later captain Van Dijk rose highest above West Ham substitute Niclas Fullkrug from a corner in front of the Kop to establish Liverpool a 13-point lead at the top of the Premier League.
        There may have been a push by Van Dijk on the Hammers forward, which VAR did not punish. "Nice little shove isn't there?" said Roy Keane. "I thought VAR would look at that but obviously not!"
         Should Arsenal lose to Ipswich next weekend on Super Sunday, Liverpool will know victory over Leicester, also live on Sky Sports, will mathematically secure the title with five games to spare.
        Asked if Liverpool can now "touch the title", Salah said: "We can say that now. We owe the fans one, especially as when we won it, we won it in lockdown. So let's go for it and win it!"
        Liverpool were ultimately lucky to come away with the win, with Jamie Carragher calling the Reds "lethargic" and "distinctively average" after their opener.
        """;
    // Create client using endpoint and key
    AzureKeyCredential credentials = new AzureKeyCredential(key);
    Uri endpoint = new Uri(aiEndpoint);
    var client = new TextAnalyticsClient(endpoint, credentials);
    Response<KeyPhraseCollection> response = client.ExtractKeyPhrases(document);
    KeyPhraseCollection keyPhrases = response.Value;

    Console.WriteLine($"Extracted {keyPhrases.Count} key phrases:");
    foreach (string keyPhrase in keyPhrases)
    {
        Console.WriteLine($"  {keyPhrase}");
    }

}