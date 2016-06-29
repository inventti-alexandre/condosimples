using CondoSimples.Mail;
using CondoSimples.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.Post = db.BoardModels;
            ViewBag.Borrow = db.BorrowModels;


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
                MailHandler.SendMailToUs(message, email, subject);

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}