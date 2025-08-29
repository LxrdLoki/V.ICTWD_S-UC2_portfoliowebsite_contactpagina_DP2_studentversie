namespace Portfoliowebsite.Services
{
    public interface IEmailSender
    {
        Task SendAsync(string Name, string Email, string Subject, string Message);
    }
}
