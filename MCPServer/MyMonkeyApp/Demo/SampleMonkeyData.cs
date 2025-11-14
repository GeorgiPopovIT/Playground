using MyMonkeyApp.Models;
using MyMonkeyApp.Helpers;

namespace MyMonkeyApp.Demo;

/// <summary>
/// Provides sample monkey data for demonstration purposes.
/// </summary>
public static class SampleMonkeyData
{
    /// <summary>
    /// Gets a collection of sample monkey data that matches the MonkeyMCP server structure.
    /// </summary>
    /// <returns>A collection of sample monkeys.</returns>
    public static IEnumerable<Monkey> GetSampleMonkeys()
    {
        return new List<Monkey>
        {
            new("Baboon", "Africa & Asia", "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/baboon.jpg", 10000, -8.783195, 34.508523),
            
            new("Capuchin Monkey", "Central & South America", "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/capuchin.jpg", 23000, 12.769013, -85.602364),
            
            new("Blue Monkey", "Central and East Africa", "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/bluemonkey.jpg", 12000, 1.957709, 37.297204),
            
            new("Squirrel Monkey", "Central & South America", "The squirrel monkeys are the New World monkeys of the genus Saimiri. They are the only genus in the subfamily Saimirinae. The name of the genus Saimiri is of Tupi origin, and was also used as an English name by early researchers.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/saimiri.jpg", 11000, -8.783195, -55.491477),
            
            new("Golden Lion Tamarin", "Brazil", "The golden lion tamarin also known as the golden marmoset, is a small New World monkey of the family Callitrichidae.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/tamarin.jpg", 19000, -14.235004, -51.92528),
            
            new("Howler Monkey", "South America", "Howler monkeys are among the largest of the New World monkeys. Fifteen species are currently recognised. Previously classified in the family Cebidae, they are now placed in the family Atelidae.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/alouatta.jpg", 8000, -8.783195, -55.491477),
            
            new("Japanese Macaque", "Japan", "The Japanese macaque, is a terrestrial Old World monkey species native to Japan. They are also sometimes known as the snow monkey because they live in areas where snow covers the ground for months each", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/macasa.jpg", 1000, 36.204824, 138.252924),
            
            new("Mandrill", "Southern Cameroon, Gabon, and Congo", "The mandrill is a primate of the Old World monkey family, closely related to the baboons and even more closely to the drill. It is found in southern Cameroon, Gabon, Equatorial Guinea, and Congo.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/mandrill.jpg", 17000, 7.369722, 12.354722),
            
            new("Proboscis Monkey", "Borneo", "The proboscis monkey or long-nosed monkey, known as the bekantan in Malay, is a reddish-brown arboreal Old World monkey that is endemic to the south-east Asian island of Borneo.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/borneo.jpg", 15000, 0.961883, 114.55485),
            
            new("Red-shanked douc", "Vietnam", "The red-shanked douc is a species of Old World monkey, among the most colourful of all primates. The douc is an arboreal and diurnal monkey that eats and sleeps in the trees of the forest.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/douc.jpg", 1300, 16.111648, 108.262122)
        };
    }

    /// <summary>
    /// Demonstrates the table display functionality with sample data.
    /// </summary>
    public static void DemonstrateTableDisplay()
    {
        var monkeys = GetSampleMonkeys();

        Console.WriteLine("🎯 DEMONSTRATION: Monkey Table Display");
        Console.WriteLine("=====================================");
        Console.WriteLine();
        Console.WriteLine("This demonstrates how the MonkeyMCP data would be displayed when the server is available.");
        Console.WriteLine();

        // Standard table display
        MonkeyTableDisplay.DisplayMonkeysTable(monkeys);

        Console.WriteLine();
        Console.WriteLine("Press any key to see the ASCII art table...");
        Console.ReadKey();

        // ASCII art table
        MonkeyTableDisplay.DisplayAsciiTable(monkeys);

        Console.WriteLine();
        Console.WriteLine("Press any key to see conservation status grouping...");
        Console.ReadKey();

        // Conservation status grouping
        MonkeyTableDisplay.DisplayMonkeysByConservationStatus(monkeys);

        Console.WriteLine();
        Console.WriteLine("Press any key to see a detailed monkey view...");
        Console.ReadKey();

        // Detailed view of first monkey
        var firstMonkey = monkeys.First();
        MonkeyTableDisplay.DisplayMonkeyDetails(firstMonkey);
    }
}
