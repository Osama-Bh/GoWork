namespace GoWork.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, string userName = "Clinet");
    }
}
