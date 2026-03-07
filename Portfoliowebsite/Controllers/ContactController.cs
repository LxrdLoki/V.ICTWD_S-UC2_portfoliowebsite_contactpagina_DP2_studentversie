using Microsoft.AspNetCore.Mvc;
using Portfoliowebsite.Services;

namespace Portfoliowebsite.Controllers
{
    public class ContactController : Controller
    {

        private readonly IEmailSender _email;
        public ContactController(IEmailSender email) => _email = email;

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(string Name, string Email, string Subject, string Message, string website)
        {
            // honeypot check
            if (!string.IsNullOrEmpty(website))
            {
                return View();
            }
            if(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Subject) || string.IsNullOrWhiteSpace(Message))
            {
                return View();
            }

            //maxlength check to prevent abuse
            if(Name.Length > 100 || Email.Length > 100 || Subject.Length > 100 || Message.Length > 1000)
            {
                ViewData["Name"] = Name;
                ViewData["Email"] = Email;
                ViewData["Subject"] = Subject;
                ViewData["Message"] = Message;

                return View();

            }

            //a simple check for email format
            if(!Email.Contains("@") || !Email.Contains("."))
            {
                ViewData["Name"] = Name;
                ViewData["Email"] = Email;
                ViewData["Subject"] = Subject;
                ViewData["Message"] = Message;

                return View();
            }
            await _email.SendAsync(Name, Email, Subject, Message);
            

            TempData["ThanksName"] = Name;
            TempData["ThanksEmail"] = Email;
            TempData["ThanksMessage"] = Message;

            return RedirectToAction(nameof(Thanks));
        }

        public IActionResult Thanks()
        {
            return View();
        }
    }
}
