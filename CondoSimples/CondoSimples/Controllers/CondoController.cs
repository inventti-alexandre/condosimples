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
    public class CondoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Condo
        public ActionResult Index()
        {
            return View(db.CondoModels.ToList());
        }

        // GET: Condo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CondoModel condoModel = db.CondoModels.Find(id);
            if (condoModel == null)
            {
                return HttpNotFound();
            }
            return View(condoModel);
        }

        // GET: Condo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Condo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] CondoModel condoModel)
        {
            if (ModelState.IsValid)
            {
                db.CondoModels.Add(condoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(condoModel);
        }

        // GET: Condo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CondoModel condoModel = db.CondoModels.Find(id);
            if (condoModel == null)
            {
                return HttpNotFound();
            }
            return View(condoModel);
        }

        // POST: Condo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] CondoModel condoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(condoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(condoModel);
        }

        // GET: Condo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CondoModel condoModel = db.CondoModels.Find(id);
            if (condoModel == null)
            {
                return HttpNotFound();
            }
            return View(condoModel);
        }

        // POST: Condo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CondoModel condoModel = db.CondoModels.Find(id);
            db.CondoModels.Remove(condoModel);
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
