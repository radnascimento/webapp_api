using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Api.Helpers
{
    public class EmailHelper
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587; // Use 465 for SSL
        private readonly string _senderEmail;
        private readonly string _senderPassword;

        public EmailHelper()
        {

        }

        public EmailHelper(string senderEmail, string senderPassword)
        {
            _senderEmail = senderEmail;
            _senderPassword = senderPassword;
        }

        public async Task<string> LoadEmailTemplateAsync(string templateName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", templateName);
            return await File.ReadAllTextAsync(filePath);
        }

        public async Task<bool> SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Your Name", _senderEmail));
                email.To.Add(new MailboxAddress("", recipientEmail));
                email.Subject = subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = body };
                email.Body = bodyBuilder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_senderEmail, _senderPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}
