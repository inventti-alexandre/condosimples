using CondoSimples.Mail;
using CondoSimples.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace CondoSimples.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            var condo = db.CondoModels.FirstOrDefault(x => x.ID == user.Condo_ID);

            ViewBag.Condo = condo.Name;
            ViewBag.Post = db.BoardModels.Include(i => i.User).Where(x => x.User.Condo_ID == condo.ID).ToList();
            ViewBag.Borrow = db.BorrowModels.Include(u => u.UserRequest).Include(x => x.UserLending).Where(y => y.DateComplete == null && y.UserRequest.Id != user.Id && y.DateReturn > DateTime.Now && y.UserRequest.Condo_ID == user.Condo_ID && y.UserLending == null).ToList();
            ViewBag.Schedule = db.ScheduleModels.Include(i => i.Place).Include(i => i.User).Include(i => i.User.User).Where(x => x.User.User.Id == user.Id).ToList();


            DateTime notificationLimit = DateTime.Now.AddMonths(-1);
            ViewBag.Notification = db.NotificationModels.Include(c => c.Condo).Where(x => x.Condo.ID == condo.ID && x.DateRegister > notificationLimit).ToList();

            if (User.IsInRole("Sindico"))
            {
                ViewBag.UrlCondo = string.Concat(Request.Url.Host, "/User/Create?condo=", condo.ID);
            }

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(string email, string message, string subject)
        {
            if (ModelState.IsValid)
            {
                string body = string.Format("De: {0}<br/>Assunto: {1}<br/>Mensagem: {2}", email, subject, message);

                MailHandler.SendMail(body, "rdgs.rafael@gmail.com", "Contato");                

                return RedirectToAction("About");
            }

            return View();
        }
    }
}