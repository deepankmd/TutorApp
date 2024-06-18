using System.Net;
using System.Net.Mail;

namespace TutorAppAPI.Services
{

    public class NotificationService
    {
        public static Task SendEmailAsync(string email, string subject, string message)
        {
            string fromemail = "tutormastert@gmail.com";
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromemail, "Tutor@1234")
            };

            return client.SendMailAsync(
                new MailMessage(from: fromemail, to: email, subject, message));
        }

        //public void SendEmail(string toEmail, string subject, string body)
        //{
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("Your App", EmailFromAddress));
        //    message.To.Add(new MailboxAddress(toEmail, toEmail));
        //    message.Subject = subject;
        //    message.Body = new TextPart("plain")
        //    {
        //        Text = body
        //    };

        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect(SmtpServer, SmtpPort, false);
        //        client.Authenticate(SmtpUser, SmtpPass);
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //}

        //public void SendSms(string toPhoneNumber, string message)
        //{
        //    TwilioClient.Init(AccountSid, AuthToken);

        //    var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber))
        //    {
        //        From = new PhoneNumber(TwilioPhoneNumber),
        //        Body = message
        //    };

        //    MessageResource.Create(messageOptions);
        //}

        public void AddNotification(string message, List<string> notifications)
        {
            notifications.Add(message);
        }
    }
}
