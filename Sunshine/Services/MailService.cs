using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System;

namespace Sunshine.Services
{
    public class MailService
    {
        public async Task SendEmailConfirmationEmail(string email, string confirmationEmailLink)
        {
            string from = Environment.GetEnvironmentVariable("From");
            string smtpServer = Environment.GetEnvironmentVariable("SmtpServer");
            int port = int.Parse(Environment.GetEnvironmentVariable("Port"));
            string username = Environment.GetEnvironmentVariable("Username");
            string password = Environment.GetEnvironmentVariable("Password");

            MimeMessage emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Сонечко", Environment.GetEnvironmentVariable("From")));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Підтвердження email";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"Для підтвердження email перейдіть за посиланням {confirmationEmailLink}"
            };

            using (SmtpClient client = new SmtpClient())
            {
                await client.ConnectAsync(Environment.GetEnvironmentVariable("SmtpServer"), int.Parse(Environment.GetEnvironmentVariable("Port")));
                await client.AuthenticateAsync(Environment.GetEnvironmentVariable("Username"), Environment.GetEnvironmentVariable("Password"));
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
