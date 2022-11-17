//using SendGrid.Helpers.Mail;
//using SendGrid;
//using ConsoleApp3;

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

using SpotifyAPI.Web;
//Cloudinary


using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.VisualBasic;
using Microsoft.Extensions.Configuration;

using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System.IO;
using System.Text;

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
List<CsvLine> lines = new List<CsvLine>();

var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    HasHeaderRecord = false,
    MissingFieldFound = null
    
};

try
{
    
    using (var fs = new StreamReader(@"C:\Users\Georges\Desktop\19332_Spotify_Songs.csv"))
    {
        // I just need this one line to load the records from the file in my List<CsvLine>
        using (var csv = new CsvReader(fs, configuration))
        {
            lines = csv.GetRecords<CsvLine>().ToList();
        }
    }
}

catch (Exception e)
{
    Console.WriteLine(e);
}


 record CsvLine([Index(8)] string Song, [Index(10)] string AudioLink, [Index(19)] string ImageLink);

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


