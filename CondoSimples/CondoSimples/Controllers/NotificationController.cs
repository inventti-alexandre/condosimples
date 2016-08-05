using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CondoSimples.Models;
using Microsoft.AspNet.Identity;

namespace CondoSimples.Controllers
{
    [Authorize(Roles = "Sindico")]
    public class NotificationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Notification
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(db.NotificationModels.Include(c => c.Condo).Where(x => x.Condo.ID == user.Condo_ID).ToList());
        }

        // GET: Notification/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificationModel notificationModel = db.NotificationModels.Find(id);
            if (notificationModel == null)
            {
                return HttpNotFound();
            }
            return View(notificationModel);
        }

        // GET: Notification/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notification/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Message")] NotificationModel notificationModel)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());

                notificationModel.DateRegister = DateTime.Now;
                notificationModel.Condo = db.CondoModels.FirstOrDefault(x => x.ID == user.Condo_ID);
                notificationModel.User = db.UserModels.Include(u => u.User).FirstOrDefault(x => x.User.Id == user.Id);

                db.NotificationModels.Add(notificationModel);
                db.SaveChanges();

                var usersToSendMail = db.Users.Where(x => x.Condo_ID == notificationModel.Condo.ID).ToList();
                var roleCondomino = db.Roles.Include(i => i.Users).First(x => x.Name == "Condomino");

                foreach (ApplicationUser userToMail in usersToSendMail)
                {
                    bool sendSMS = false;

                    Mail.MailHandler.SendMail(notificationModel.Message, userToMail.Email, "Nova notificação");

                    foreach(var role in userToMail.Roles)
                    {
                        if (role.RoleId == roleCondomino.Id)
                            sendSMS = true;
                    }

                    if (sendSMS)
                    {
                        string strErro = string.Empty;

                        var userSms = db.UserModels.Include(i => i.User).First(x => x.User.Id == userToMail.Id);
                        SMS.SMSHandler.SendSMS(userSms.Cel.Replace("-","").Replace(" ","").Replace("(","").Replace(")",""),
                                                notificationModel.Message, 
                                                ref strErro);
                    }
                }

                return RedirectToAction("Index");
            }

            return View(notificationModel);
        }

        // GET: Notification/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificationModel notificationModel = db.NotificationModels.Find(id);
            if (notificationModel == null)
            {
                return HttpNotFound();
            }
            return View(notificationModel);
        }

        // POST: Notification/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Message,DateRegister")] NotificationModel notificationModel)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());

                notificationModel.DateRegister = DateTime.Now;
                notificationModel.Condo = db.CondoModels.FirstOrDefault(x => x.ID == user.Condo_ID);
                notificationModel.User = db.UserModels.Include(u => u.User).FirstOrDefault(x => x.User.Id == user.Id);

                db.Entry(notificationModel).State = EntityState.Modified;
                db.SaveChanges();

                var usersToSendMail = db.Users.Where(x => x.Condo_ID == notificationModel.Condo.ID).ToList();

                foreach (ApplicationUser userToMail in usersToSendMail)
                {
                    Mail.MailHandler.SendMail(notificationModel.Message, userToMail.Email, "Notificação alterada");
                }

                return RedirectToAction("Index");
            }
            return View(notificationModel);
        }

        // GET: Notification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificationModel notificationModel = db.NotificationModels.Find(id);
            if (notificationModel == null)
            {
                return HttpNotFound();
            }
            return View(notificationModel);
        }

        // POST: Notification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NotificationModel notificationModel = db.NotificationModels.Find(id);
            db.NotificationModels.Remove(notificationModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
