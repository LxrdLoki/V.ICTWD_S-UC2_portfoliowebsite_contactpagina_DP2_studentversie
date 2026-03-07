using System.Diagnostics;
using System.Text.Json;
using DotNetEnv;
using Portfoliowebsite.Models;

namespace Portfoliowebsite.Services
{
    public class VerifyRecaptchaService
    {
        public async Task<bool> VerifyRecaptcha(string token)
        {
            try
            {
                Env.Load(Path.Combine(Directory.GetCurrentDirectory(), "../.env"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("error loading enviroment variables", ex);
                return false;
            }
            var secret = Environment.GetEnvironmentVariable("private_captcha_key");

            using var client = new HttpClient();

            var response = await client.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={token}",
                null
            );

            var json = await response.Content.ReadAsStringAsync();

            if(!json.Contains("success"))
            {
                Console.WriteLine("invalid response from recaptcha api");
                return false;
            }

            var result = JsonSerializer.Deserialize<RecaptchaResponse>(json);

            return result!.success;
        }
    }
}