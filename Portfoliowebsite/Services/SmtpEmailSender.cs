using System.Net;
using System.Net.Mail;
using DotNetEnv;
using Mailtrap;

namespace Portfoliowebsite.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        public async Task SendAsync(string Name, string Email, string Subject, string Message)
        {  
            try
            {
                Env.Load(Path.Combine(Directory.GetCurrentDirectory(), "../.env"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("error loading enviroment variables", ex);
                return;
            }

            var smtpEmail = Environment.GetEnvironmentVariable("EMAIL");
            var smtpPassword = Environment.GetEnvironmentVariable("PASSWORD");

            if (string.IsNullOrEmpty(smtpEmail) || string.IsNullOrEmpty(smtpPassword))
            {
                Console.WriteLine("EMAIL or PASSWORD environment variables are missing from .env file");
                return;
            }

            var smtp = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                EnableSsl = false,
                Credentials = new NetworkCredential(smtpEmail, smtpPassword)
            };

            var mail = new MailMessage();
            mail.From = new MailAddress("noreply@example.com", "Website");
            mail.To.Add("s1212292@student.windesheim.nl");

            mail.Subject = $"Contact: {Subject}";
            mail.Body = $"Naam: {Name}\nEmail: {Email}\nBericht:\n{Message}";

            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"there was an error sending the email", ex);
                return;
            }
            finally
            {
                // disposing the SmtpClient and MailMessage to free up resouces
                smtp.Dispose();
                mail.Dispose();
            }
        }
    }
}
