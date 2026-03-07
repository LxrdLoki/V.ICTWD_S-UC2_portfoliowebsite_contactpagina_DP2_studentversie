using Microsoft.AspNetCore.Mvc;
using Portfoliowebsite.Models;
using Portfoliowebsite.Services;

namespace Portfoliowebsite.Controllers
{
    public class ContactController : Controller
    {

        private readonly IEmailSender _email;
        public ContactController(IEmailSender email) => _email = email;

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormModel form, string website)
        {
            // honeypot check 
            if (!string.IsNullOrEmpty(website))
            {
                return View(form);
            }
            // validation checks for empty fields, maxlength and a simple email format check
            // a lot of if statments but it works >:)
            if(string.IsNullOrWhiteSpace(form.Name))
            {
                ModelState.AddModelError("Name", "Naam is verplicht");
                return View(form);
            }
            if(string.IsNullOrWhiteSpace(form.Email))
            {
                ModelState.AddModelError("Email", "E-mail is verplicht");
                return View(form);
            }
            if(string.IsNullOrWhiteSpace(form.Subject))
            {
                ModelState.AddModelError("Subject", "Onderwerp is verplicht");
                return View(form);
            }
            if(string.IsNullOrWhiteSpace(form.Message))
            {
                ModelState.AddModelError("Message", "Bericht is verplicht");
                return View(form);
            }


            //maxlength check to prevent abuse (very big ifstatmement but it works >:) )
            if(form.Name.Length > 100)
            {
                ModelState.AddModelError("Name", "Naam mag niet langer zijn dan 100 tekens");
                return View(form);
            }
            if(form.Email.Length > 100)
            {
                ModelState.AddModelError("Email", "E-mail mag niet langer zijn dan 100 tekens");
                return View(form);
            }
            if(form.Subject.Length > 100)
            {
                ModelState.AddModelError("Subject", "Onderwerp mag niet langer zijn dan 100 tekens");
                return View(form);
            }
            if(form.Message.Length > 1000)
            {
                ModelState.AddModelError("Message", "Bericht mag niet langer zijn dan 1000 tekens");
                return View(form);
            }

            //a simple check for email format (too lazy for regex sorry D:)
            if(!form.Email.Contains("@") || !form.Email.Contains("."))
            {
                ModelState.AddModelError("Email", "Ongeldige email");
                return View(form);
            }
            await _email.SendAsync(form.Name, form.Email, form.Subject, form.Message);
            

            TempData["ThanksName"] = form.Name;
            TempData["ThanksEmail"] = form.Email;
            TempData["ThanksMessage"] = form.Message;

            return RedirectToAction(nameof(Thanks));
        }

        public IActionResult Thanks()
        {
            return View();
        }
    }
}
