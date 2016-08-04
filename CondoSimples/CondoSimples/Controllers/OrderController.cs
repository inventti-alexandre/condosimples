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
    [Authorize]
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Order
        [Authorize(Roles = "Empregado")]
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(db.OrderModels.Include(u => u.UserRecipient)
                                        .Include(u => u.UserRecipient.User)
                                        .Include(u => u.UserEmployee)
                                        .Include(u => u.UserRecipient.Unit)
                                        .Where(x => x.UserRecipient.User.Condo_ID == user.Condo_ID && x.DateReceived.Month == DateTime.Now.Month).ToList());
        }

        // GET: Order/IndexByUser
        public ActionResult IndexByUser()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(db.OrderModels.Include(u => u.UserRecipient)
                                        .Include(u => u.UserRecipient.User)
                                        .Include(u => u.UserEmployee)
                                        .Where(x => x.UserRecipient.User.Id == user.Id).ToList());
        }

        // GET: Order/Create
        [Authorize(Roles = "Empregado")]
        public ActionResult Create()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            ViewBag.Unit = new SelectList((from un in db.UnitModels.Include(t => t.Tower)
                                           join us in db.UserModels on un.ID equals us.Unit.ID
                                           where un.Tower.Condo.ID == user.Condo_ID
                                           select new
                                           {
                                               ID = un.ID,
                                               Name = un.Name
                                           }).ToList(), "ID", "Name");

            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empregado")]
        public ActionResult Create([Bind(Include = "ID,Description,Unit")] OrderModel orderModel)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            ViewBag.Unit = new SelectList((from un in db.UnitModels.Include(t => t.Tower)
                                           join us in db.UserModels on un.ID equals us.Unit.ID
                                           where un.Tower.Condo.ID == user.Condo_ID
                                           select new
                                           {
                                               ID = un.ID,
                                               Name = un.Name
                                           }).ToList(), "ID", "Name", Request.Form["Unit"]);            

            int idUnit = Convert.ToInt32(Request.Form["Unit"]);

            if (ModelState.IsValid)
            {
                try
                {
                    orderModel.UserRecipient = db.UserModels.Include(u => u.Unit).Include(u => u.User).First(u => u.Unit.ID == idUnit);
                }
                catch
                {
                    ViewBag.Error = "Não existe usuário na unidade selecionada";
                    return View(orderModel);
                }

                orderModel.UserEmployee = db.EmployeeModels.Include(u => u.User).First(e => e.User.Id == user.Id);                
                orderModel.DateReceived = DateTime.Now;

                db.OrderModels.Add(orderModel);
                db.SaveChanges();

                Mail.MailHandler.SendMail(orderModel.Description, orderModel.UserRecipient.Email, "Nova encomenda");

                string errorSMS = string.Empty;
                SMS.SMSHandler.SendSMS(orderModel.UserRecipient.Cel, "CondoSimples - Você recebeu uma encomenda", ref errorSMS);

                return RedirectToAction("Index");
            }

            return View(orderModel);
        }

        // GET: Order/Delete/5
        [Authorize(Roles = "Empregado")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderModel orderModel = db.OrderModels.Find(id);
            if (orderModel == null)
            {
                return HttpNotFound();
            }
            return View(orderModel);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empregado")]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderModel orderModel = db.OrderModels.Include(u => u.UserRecipient).First(x => x.ID == id);

            Mail.MailHandler.SendMail(orderModel.Description, orderModel.UserRecipient.Email, "Encomenda excluída");

            db.OrderModels.Remove(orderModel);
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
