using SPATemplateConversion.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SPATemplateConversion.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ContactAjax(ContactViewModel cvm)
        {
            string body = $"You have received an email from <strong>{cvm.Name}</strong>. The email address given was <strong>{cvm.Email}</strong>.<br/><strong>The following message was sent:</strong> {cvm.Message}";

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("email@domain.ext");
            mm.To.Add(new MailAddress("email@email.com"));
            mm.Subject = cvm.Subject;
            mm.Body = body;

            mm.IsBodyHtml = true;
            mm.ReplyToList.Add(cvm.Email);

            SmtpClient smtp = new SmtpClient("mail.domain.ext");
            NetworkCredential cred = new NetworkCredential("email@domain.ext", "password");

            smtp.Credentials = cred;
            smtp.Send(mm);
            return Json(cvm); 
        }


        public PartialViewResult ContactConfirmation(string name, string email)
        {
            ViewBag.Name = name;
            ViewBag.Email = email;
            return PartialView("ContactConfirmation"); 
        }
    }
}