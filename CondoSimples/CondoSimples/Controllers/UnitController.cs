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
    public class UnitController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Unit
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            var unitModels = db.UnitModels.Include(i => i.Tower).Include(i => i.Tower.Condo).Where(x => x.Tower.Condo.ID == user.Condo_ID);
            return View(unitModels.ToList());
        }

        // GET: Unit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitModel unitModel = db.UnitModels.Include(i => i.Tower).FirstOrDefault(x => x.ID == id);
            if (unitModel == null)
            {
                return HttpNotFound();
            }
            return View(unitModel);
        }

        // GET: Unit/Create
        public ActionResult Create()
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            ViewBag.Tower_ID = new SelectList(db.TowerModels.Include(i => i.Condo).Where(x => x.Condo.ID == user.Condo_ID) , "ID", "Name");
            return View();
        }

        // POST: Unit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Tower_ID")] UnitModel unitModel)
        {
            if (ModelState.IsValid)
            {
                db.UnitModels.Add(unitModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var user = db.Users.Find(User.Identity.GetUserId());
            ViewBag.Tower_ID = new SelectList(db.TowerModels.Include(i => i.Condo).Where(x => x.Condo.ID == user.Condo_ID), "ID", "Name", unitModel.Tower_ID);
            return View(unitModel);
        }

        // GET: Unit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitModel unitModel = db.UnitModels.Include(i => i.Tower).FirstOrDefault(x => x.ID == id);
            if (unitModel == null)
            {
                return HttpNotFound();
            }
            var user = db.Users.Find(User.Identity.GetUserId());
            ViewBag.Tower_ID = new SelectList(db.TowerModels.Include(i => i.Condo).Where(x => x.Condo.ID == user.Condo_ID), "ID", "Name", unitModel.Tower_ID);
            return View(unitModel);
        }

        // POST: Unit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Tower_ID")] UnitModel unitModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unitModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var user = db.Users.Find(User.Identity.GetUserId());
            ViewBag.Tower_ID = new SelectList(db.TowerModels.Include(i => i.Condo).Where(x => x.Condo.ID == user.Condo_ID), "ID", "Name", unitModel.Tower_ID);
            return View(unitModel);
        }

        // GET: Unit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitModel unitModel = db.UnitModels.Find(id);
            if (unitModel == null)
            {
                return HttpNotFound();
            }
            return View(unitModel);
        }

        // POST: Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UnitModel unitModel = db.UnitModels.Find(id);
            db.UnitModels.Remove(unitModel);
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
