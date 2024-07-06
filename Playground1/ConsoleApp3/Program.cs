


//IList<IList<int>> Generate(int numRows)
//{
//    int[][] pascalTriangle = new int[numRows][];
//    for (int i = 0; i < numRows; i++)
//    {
//        pascalTriangle[i] = new int[i + 1];
//    }
//    pascalTriangle[0][0] = 1;
//    for (int i = 0; i < numRows; i++)
//    {
//        pascalTriangle[i][0] = 1;
//        pascalTriangle[i][pascalTriangle[i].Length - 1] = 1;
//        for (int j = 1; j < pascalTriangle[i].Length - 1; j++)
//        {
//            pascalTriangle[i][j] = pascalTriangle[i - 1][j]
//                + pascalTriangle[i - 1][j - 1];
//        }
//    }

//    return pascalTriangle;
//}


//Console.WriteLine(GetTotalCoinsFromSum(new List<int> { 1, 2, 3, 5, 10, 20, 50, 100 }, 81));
//static int GetTotalCoinsFromSum(IEnumerable<int> input, int target)
//{
//    var coins = new Queue<int>(input.OrderByDescending(x => x));

//    int totalCoins = 0;

//    while (target > 0 && coins.Count > 0)
//    {
//        var currentCoin = coins.Dequeue();

//        var count = target / currentCoin;

//        if (count == 0)
//        {
//            continue;
//        }

//        target %= currentCoin;

//        totalCoins += count;
//    }

//    return totalCoins;
//}

//foreach (var item in linkedlist)
//{
//    Console.WriteLine(item);
//}

//public class LinkedListNode<T>
//{
//    public T Value { get; set; }
//    public LinkedListNode<T> Next { get; set; }

//    public LinkedListNode(T value)
//    {
//        Value = value;
//        Next = null;
//    }
//}

//public class LinkedList<T>
//{
//    private LinkedListNode<T> head;
//    private LinkedListNode<T> tail;
//    private int count;

//    public int Count { get { return count; } }

//    public void AddLast(T value)
//    {
//        LinkedListNode<T> node = new LinkedListNode<T>(value);
//        if (head == null)
//        {
//            head = node;
//            tail = node;
//        }
//        else
//        {
//            tail.Next = node;
//            tail = node;
//        }
//        count++;
//    }

//    public void AddFirst(T value)
//    {
//        LinkedListNode<T> node = new LinkedListNode<T>(value);
//        if (head == null)
//        {
//            head = node;
//            tail = node;
//        }
//        else
//        {
//            node.Next = head;
//            head = node;
//        }
//        count++;
//    }

//    public void Remove(T value)
//    {
//        LinkedListNode<T> current = head;
//        LinkedListNode<T> previous = null;

//        while (current != null)
//        {
//            if (current.Value.Equals(value))
//            {
//                if (previous == null)
//                {
//                    head = current.Next;
//                }
//                else
//                {
//                    previous.Next = current.Next;
//                }

//                if (current == tail)
//                {
//                    tail = previous;
//                }

//                count--;
//                break;
//            }

//            previous = current;
//            current = current.Next;
//        }
//    }

//    public IEnumerator<T> GetEnumerator()
//    {
//        LinkedListNode<T> current = head;

//        while (current != null)
//        {
//            yield return current.Value;
//            current = current.Next;
//        }
//    }
//}


//var p1 = new Person("Georgi", "Popov");
//Console.WriteLine(p1.FirstName + " " + p1.LastName);
//public record Person(string FirstName, string LastName)
//{
//    public int Age { get; set; }
//    void Write()
//    {

//    }
//}


using ConsoleApp3;
using ConsoleApp3.MongoDbTest;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;


var mongoClient = new MongoClient(CustometDatabaseSettings.ConnectionString);

var dbContextOptions =
    new DbContextOptionsBuilder<AnimalDbContext>()
    .UseMongoDB(mongoClient, CustometDatabaseSettings.DatabaseName);

var db = new AnimalDbContext(dbContextOptions.Options);

db.Customers.Add(new Customer() { Name = "Misho", Order = "Pizza",Address= new()
{
    Street = "Bul. Maritsa 143",
    City = "Plovdiv"
}
});
db.SaveChanges();

var countDb = db.Customers.Count();

Console.WriteLine(countDb);

//using SpotifyAPI.Web;
//Cloudinary


//using CloudinaryDotNet;
//using CloudinaryDotNet.Actions;
//using Microsoft.VisualBasic;
//using Microsoft.Extensions.Configuration;

//using System.Globalization;
//using CsvHelper.Configuration;
//using CsvHelper;
//using CsvHelper.Configuration.Attributes;
//using System.IO;
//using System.Text;
//using MimeKit;
//using MailKit.Net.Smtp;

//var message = new MimeMessage();
//message.From.Add(MailboxAddress.Parse("gogopopo262003@gmail.com"));
//message.To.Add(MailboxAddress.Parse("mefixa3922@areosur.com"));
//message.Subject = "test mailkit";
//message.Body = new TextPart("plain")
//{
//    Text = "Hello my"
//};

//using var client = new SmtpClient();

//try
//{
//    client.Connect("smtp.gmail.com", 465, true);
//    client.AuthenticationMechanisms.Remove("XOAUTH2");
//    client.Authenticate("gogopopo262003@gmail.com", "qsmisawtfflnoknn");
//    client.Send(message);
//    client.Disconnect(true);

//    Console.WriteLine("Succsess");

//}
//catch (Exception e)
//{
//    Console.WriteLine("Error: " + e.Message);
//    throw;
//}


//MailMessage messageToSend = new MailMessage("gogopopo262003@gmail.com", "mefixa3922@areosur.com");
//messageToSend.Subject = "asda";
//messageToSend.Body = "adaddsad";

//using System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com");

//try
//{
//     client.Send(messageToSend, "qsmisawtfflnoknn");
//}
//catch (Exception)
//{
//    Console.WriteLine("Not send");
//    throw;
//}


//string s = "a";
//long n = 1000000000000;

//var sb = new StringBuilder();

//for (int i = 0; i < n; i++)
//{
//    //if (sb.Append(s).Length > n)
//    //{
//    //    var subStr = sb.ToString(0, Convert.ToInt32(n));
//    //    sb.Replace(sb.ToString(), subStr);

//    //    break;
//    //}
//    sb.Append(s);

//}

//Console.WriteLine((long)sb.ToString().Count(c => c == 'a'));




//return;
//C# 10 FEATURE

//Person p1 = new Person("Georgi", 11);

//if (p1 is { Name: "Georgi", Age: 11 })
//{
//    Console.WriteLine(true);
//    return;
//}
//Console.WriteLine(false);
//record Person(string Name, int Age);

class Address
{
    public string AddressName { get; init; } = "ff";
}
//Account account = new Account(
//"daxn3ngly",
//"911681576639439",
//"G_gqkipIq3Bwkpj57X7lhmq_r3Q");

//Cloudinary cloudinary = new Cloudinary(account);
//var result = cloudinary.Search().Expression("dsad").Execute();
//var uploadParams = new ImageUploadParams()
//{
//    File = new FileDescription(@"C:\Users\Georges\Desktop\solmusic-master\solmusic-master\music-files\2.mp3"),
//    UseFilename = true,
//    UniqueFilename = false,
//    Overwrite = true,
//    Folder = "GorgesMusic"
//};


//var uploadParams = new VideoUploadParams
//{
//    File = new FileDescription(@"C:\Users\Georges\Desktop\solmusic-master\solmusic-master\music-files\2.mp3"),
//    UseFilename = true,
//    UniqueFilename = false,
//    Overwrite = true,
//    Folder = "GorgesMusic"
//};



//var uploadResult = cloudinary.Upload(uploadParams);
//Console.WriteLine(uploadResult.JsonObj);

//}


//List<CsvLine> lines = new List<CsvLine>();

//var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
//{
//    HasHeaderRecord = false,
//    MissingFieldFound = null

//};

//try
//{

//    using (var fs = new StreamReader(@"C:\Users\Georges\Desktop\19332_Spotify_Songs.csv"))
//    {
//        // I just need this one line to load the records from the file in my List<CsvLine>
//        using (var csv = new CsvReader(fs, configuration))
//        {
//            lines = csv.GetRecords<CsvLine>().ToList();
//        }
//    }
//}

//catch (Exception e)
//{
//    Console.WriteLine(e);
//}


//record CsvLine([Index(8)] string Song, [Index(10)] string AudioLink, [Index(19)] string ImageLink);



//SendGrid

//var client = new SendGridClient("*********");
//var from = new EmailAddress("aspnettest1912@gmail.com", "Example User");
//var subject = "Sending with SendGrid is Fun";
//var to = new EmailAddress("fepaw99251@kaimdr.com", "Example User");
//var plainTextContent = "and easy to do anywhere, even with C#";
//var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
//var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//var response = await client.SendEmailAsync(msg);

//Console.WriteLine(((int)response.StatusCode));
//// A success status code means SendGrid received the email request and will process it.
//// Errors can still occur when SendGrid tries to send the email. 
//// If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
//Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");


