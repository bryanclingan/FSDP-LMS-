using LMS.UI.MVC.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace LMS.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            string message = $"You have received an email from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br/>{cvm.Message}";

            MailMessage mm = new MailMessage("webadmin@bryandoescode.com", "bryanclingan@outlook.com", cvm.Subject, message);

            mm.IsBodyHtml = true;

            mm.Priority = MailPriority.High;

            mm.ReplyToList.Add(cvm.Email);

            SmtpClient client = new SmtpClient("mail.bryandoescode.com");

            client.Credentials = new NetworkCredential("webadmin@bryandoescode.com", "P@ssw0rd");

            client.Port = 587;
            //client.UseDefaultCredentials = false;
            //client.EnableSsl = false;

            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry your request cannot be sent at this time. Please try again later. <br/>ErrorMessage:<br/> {ex.StackTrace}";
                return View(cvm);
            }
            return RedirectToAction("Index");

        }
    }
}

