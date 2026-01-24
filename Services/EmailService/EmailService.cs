
using MailKit.Net.Smtp;
using MimeKit;

namespace GoWork.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string to, string subject, string body, string userName = "Clinet")
        {
            // Retrieve the mail server (SMTP host) from the configuration.
            string? MailServer = _configuration["EmailSettings:SmtpServer"];

            // Retrieve the sender email address from the configuration.
            string? SenderEmail = _configuration["EmailSettings:SenderEmail"];

            // Retrieve the sender email password from the configuration.
            string? Password = _configuration["EmailSettings:Password"];

            // Retrieve the sender's display name from the configuration.
            string? SenderName = _configuration["EmailSettings:SenderName"];

            // Retrieve the sender's UserName from the configuration.
            string? Username = _configuration["EmailSettings:Username"];

            // Retrieve the SMTP port number from the configuration and convert it to an integer.
            int Port = Convert.ToInt32(_configuration["EmailSettings:SmtpPort"]);

            var message = new MimeMessage();

            var from = new MailboxAddress(SenderName, SenderEmail);
            message.From.Add(from);

            var To = new MailboxAddress(userName, to);
            message.To.Add(To);

            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            var smtp = new SmtpClient();
            await smtp.ConnectAsync(MailServer, Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(Username, Password);
            await smtp.SendAsync(message);
            smtp.Disconnect(true);
        }
    }
}
