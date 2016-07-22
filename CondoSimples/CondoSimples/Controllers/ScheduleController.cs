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
    public class ScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Schedule
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            return View(db.ScheduleModels.Include(i => i.Place)
                                            .Include(i => i.User)
                                            .Include(i => i.User.Unit)
                                            .Include(i => i.User.Unit.Tower)
                                            .Include(i => i.User.Unit.Tower.Condo)
                                            .Where(x => x.User.Unit.Tower.Condo.ID == user.Condo_ID).ToList());
        }

        // GET: Schedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleModel scheduleModel = db.ScheduleModels.Find(id);
            if (scheduleModel == null)
            {
                return HttpNotFound();
            }
            return View(scheduleModel);
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            ViewBag.Places = new SelectList(db.CommonPlaceModels.Include(c => c.Condo).Where(x => x.Condo.ID == user.Condo_ID).ToList(), "ID", "Name");

            return View();
        }

        // POST: Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateSchedule")] ScheduleModel scheduleModel, int Places)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                ViewBag.Places = new SelectList(db.CommonPlaceModels.Include(c => c.Condo).Where(x => x.Condo.ID == user.Condo_ID).ToList(), "ID", "Name", Places);

                ScheduleModel schedule = db.ScheduleModels.Include(i => i.Place).FirstOrDefault(x => x.Place.ID == Places && x.DateSchedule == scheduleModel.DateSchedule);
                if (schedule == null)
                {
                    scheduleModel.User = db.UserModels.Include(u => u.User).FirstOrDefault(x => x.User.Id == user.Id);
                    scheduleModel.Place = db.CommonPlaceModels.FirstOrDefault(x => x.ID == Places);

                    db.ScheduleModels.Add(scheduleModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Já existe uma reserva para esse lugar nesta data. Escolha outra.";
                }
            }

            return View(scheduleModel);
        }

        // GET: Schedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleModel scheduleModel = db.ScheduleModels.Find(id);
            if (scheduleModel == null)
            {
                return HttpNotFound();
            }

            var user = db.Users.Find(User.Identity.GetUserId());
            ViewBag.Places = new SelectList(db.CommonPlaceModels.Include(c => c.Condo).Where(x => x.Condo.ID == user.Condo_ID).ToList(), "ID", "Name");

            return View(scheduleModel);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateSchedule")] ScheduleModel scheduleModel, int Places)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                ViewBag.Places = new SelectList(db.CommonPlaceModels.Include(c => c.Condo).Where(x => x.Condo.ID == user.Condo_ID).ToList(), "ID", "Name", Places);

                ScheduleModel schedule = db.ScheduleModels.Include(i => i.Place).FirstOrDefault(x => x.Place.ID == Places && x.DateSchedule == scheduleModel.DateSchedule);
                if (schedule == null)
                {
                    scheduleModel.Place = db.CommonPlaceModels.FirstOrDefault(x => x.ID == Places);

                    db.Entry(scheduleModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Já existe uma reserva para esse lugar nesta data. Escolha outra.";
                }
            }
            return View(scheduleModel);
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleModel scheduleModel = db.ScheduleModels.Find(id);
            if (scheduleModel == null)
            {
                return HttpNotFound();
            }
            return View(scheduleModel);
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScheduleModel scheduleModel = db.ScheduleModels.Find(id);
            db.ScheduleModels.Remove(scheduleModel);
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
