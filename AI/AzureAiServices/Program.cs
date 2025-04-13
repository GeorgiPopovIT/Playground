using Azure.AI.TextAnalytics;
using Azure;
using Microsoft.Extensions.Configuration;
using Azure.AI.Vision.ImageAnalysis;
using System.IO;
using System.Drawing;
using System.Net;
using System.Reflection.Metadata;

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
//ExtractKeyPhases(endpoint, key);
GetSummary(endpoint,key);
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

static async Task GetSummary(string aiEndpoint, string key)
{
    AzureKeyCredential credentials = new AzureKeyCredential(key);
    Uri endpoint = new Uri(aiEndpoint);
    var client = new TextAnalyticsClient(endpoint, credentials);

    string document = """
                A coalition of governments has published a list of legitimate-looking Android apps that were actually spyware and were used to target civil society that may oppose China’s state interests.
        On Tuesday, the U.K.’s National Cyber Security Centre, or NCSC, which is part of intelligence agency GCHQ, along with government agencies from Australia, Canada, Germany, New Zealand, and the United States, published separate advisories on two families of spyware, known as BadBazaar and Moonshine.
        These two spywares hid inside legitimate-looking Android apps, acting essentially as “Trojan” malware, with surveillance capabilities such as the ability to access the phone’s cameras, microphone, chats, photos, and location data, the NCSC wrote in a press release on Wednesday.  
        BadBazaar and Moonshine, which have been previously analyzed by cybersecurity firms like Lookout, Trend Micro, and Volexity, as well as the digital rights nonprofit Citizen Lab, were used to target Uyghurs, Tibetans, and Taiwanese communities, as well as civil society groups, according to the NCSC. 
        Uyghurs are a Muslim-minority group largely in China that has for years faced detention, surveillance, and discrimination from the Chinese government, and thus has frequently been the target of hacking campaigns. 
        “The apps specifically target individuals internationally who are connected to topics that are considered by the Chinese state to pose a threat to its stability, with some designed to appeal directly to victims or imitate popular apps,” the NCSC said Wednesday. “The individuals most at risk include anyone connected to Taiwanese independence; Tibetan rights; Uyghur Muslims and other ethnic minorities in or from China’s Xinjiang Uyghur Autonomous Region; democracy advocacy, including Hong Kong, and the Falun Gong spiritual movement.”
        In one of the two documents published by the NCSC on Wednesday, there is a list of the malicious apps, which includes more than 100 Android apps masquerading as Muslim and Buddhist prayer apps; chat apps like Signal, Telegram, and WhatsApp; other popular apps like Adobe Acrobat PDF reader; and utility apps. 
        The NCSC also mentions one iOS app called TibetOne, which was listed on Apple’s App Store in 2021. 
        """;

    // Perform the text analysis operation.
    AbstractiveSummarizeOperation operation = client.AbstractiveSummarize(WaitUntil.Completed, [document]);

     await foreach (AbstractiveSummarizeResultCollection documentsInPage in operation.Value)
    {
        Console.WriteLine($"Abstractive Summarize, model version: \"{documentsInPage.ModelVersion}\"");
        Console.WriteLine();

        foreach (AbstractiveSummarizeResult documentResult in documentsInPage)
        {
            if (documentResult.HasError)
            {
                Console.WriteLine($"  Error!");
                Console.WriteLine($"  Document error code: {documentResult.Error.ErrorCode}");
                Console.WriteLine($"  Message: {documentResult.Error.Message}");
                continue;
            }

            Console.WriteLine($"  Produced the following abstractive summaries:");
            Console.WriteLine();

            foreach (AbstractiveSummary summary in documentResult.Summaries)
            {
                Console.WriteLine($"  Text: {summary.Text.Replace("\n", " ")}");
                Console.WriteLine($"  Contexts:");

                foreach (AbstractiveSummaryContext context in summary.Contexts)
                {
                    Console.WriteLine($"    Offset: {context.Offset}");
                    Console.WriteLine($"    Length: {context.Length}");
                }

                Console.WriteLine();
            }
        }
    }

}