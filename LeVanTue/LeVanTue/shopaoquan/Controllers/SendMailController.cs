using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using shopaoquan.Models;

namespace shopaoquan.Controllers
{
    public class SendMailController : Controller
    {
        // GET: SendMail
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(modelMail email)
        {
            MailMessage mm = new MailMessage("tuela4445@gmail.com", email.To);
            mm.Subject = email.Subject;
            mm.Body = email.body;
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("tuela4445@gmail.com", "ebmuqkwnkmwbflgg");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            ViewBag.Message = "Ban da gui mail thanh cong";
            return View();
        }
    }
}