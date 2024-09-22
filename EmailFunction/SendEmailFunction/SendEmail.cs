using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net.Mail;
using MimeKit;
using System.Net;
//using MailKit.Net.Smtp;
//using MimeKit;

namespace SendEmailFunction;

public static class SendEmail
{
    [FunctionName("SendEmail")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("Begin function.");
        var email = new MailMessage
        {
            From = new MailAddress("****", "****")
        };

        email.To.Add(new MailAddress("****"));

        email.Subject = "Test function";
        email.Body = "TEST MESSAGE";

        using var smtp = new SmtpClient("smtp.gmail.com", 587);

        smtp.Credentials = new NetworkCredential("****", "***   ");
        smtp.EnableSsl = true;

        await smtp.SendMailAsync(email);


        return new OkResult();
    }
}

