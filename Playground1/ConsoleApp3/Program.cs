//using SendGrid.Helpers.Mail;
//using SendGrid;
//using ConsoleApp3;
var p1 = new Person("Georgi", "Popov");
Console.WriteLine(p1.FirstName + " " + p1.LastName);
//public record Person(string FirstName, string LastName)
//{
//    public int Age { get; set; }
//    void Write()
//    {

//    }
//}

class Person
{
    private string _fName;
    private string _lName;

    public Person()
    {

    }

    public Person(string fName, string lName)
        => (FirstName, LastName) = (fName, lName);

    public string FirstName
    {
        get => this._fName;
        set => this._fName = !string.IsNullOrWhiteSpace(value) 
            ? value : throw new ArgumentNullException();
    }

    public string LastName
    {
        get => this._lName;
        init => this._lName = value ?? throw new ArgumentNullException();
    }
}



//var db = new AnimalDbContext();

//db.Animals.Add(new Animal
//{
//    Name = "Martin"
//});
// Create the service using the client credentials.
//using ConsoleApp3.MongoDbTest;
//using MongoDB.Driver;


//var mongoClient = new MongoClient(CustometDatabaseSettings.ConnectionString);
//var mongoDatabase = mongoClient.GetDatabase(CustometDatabaseSettings.DatabaseName);

//var customersCollection = mongoDatabase.GetCollection<Customer>(CustometDatabaseSettings.BooksCollectionName);

//customersCollection.InsertOne(new Customer { Name = "Georgi" });

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


