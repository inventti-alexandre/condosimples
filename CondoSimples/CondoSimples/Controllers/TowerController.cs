using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CondoSimples.Models;

namespace CondoSimples.Controllers
{
    public class TowerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tower
        public ActionResult Index()
        {
            var towerModels = db.TowerModels.Include(t => t.Condo);
            return View(towerModels.ToList());
        }

        // GET: Tower/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TowerModel towerModel = db.TowerModels.Find(id);
            if (towerModel == null)
            {
                return HttpNotFound();
            }
            return View(towerModel);
        }

        // GET: Tower/Create
        public ActionResult Create()
        {
            ViewBag.Condo_ID = new SelectList(db.CondoModels, "ID", "Name");
            return View();
        }

        // POST: Tower/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Condo_ID")] TowerModel towerModel)
        {
            if (ModelState.IsValid)
            {
                db.TowerModels.Add(towerModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Condo_ID = new SelectList(db.CondoModels, "ID", "Name", towerModel.Condo_ID);
            return View(towerModel);
        }

        // GET: Tower/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TowerModel towerModel = db.TowerModels.Find(id);
            if (towerModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Condo_ID = new SelectList(db.CondoModels, "ID", "Name", towerModel.Condo_ID);
            return View(towerModel);
        }

        // POST: Tower/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Condo_ID")] TowerModel towerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(towerModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Condo_ID = new SelectList(db.CondoModels, "ID", "Name", towerModel.Condo_ID);
            return View(towerModel);
        }

        // GET: Tower/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TowerModel towerModel = db.TowerModels.Find(id);
            if (towerModel == null)
            {
                return HttpNotFound();
            }
            return View(towerModel);
        }

        // POST: Tower/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TowerModel towerModel = db.TowerModels.Find(id);
            db.TowerModels.Remove(towerModel);
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
