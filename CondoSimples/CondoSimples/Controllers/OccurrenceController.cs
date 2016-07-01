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
    public class OccurrenceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Occurrence
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            return View(db.OccurrenceModels.Include(u => u.User).Include(c => c.Condo).Where(x => x.Condo.ID == user.Condo_ID).ToList());
        }

        // GET: Occurrence
        public ActionResult IndexPending()
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            return View(db.OccurrenceModels.Include(u => u.User).Include(c => c.Condo).Where(x => x.Condo.ID == user.Condo_ID && x.Solved == false).ToList());
        }

        // GET: Occurrence
        public ActionResult IndexSolved()
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            return View(db.OccurrenceModels.Include(u => u.User).Include(c => c.Condo).Where(x => x.Condo.ID == user.Condo_ID && x.Solved == true).ToList());
        }

        // GET: Occurrence/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OccurrenceModel occurrenceModel = db.OccurrenceModels.Find(id);
            if (occurrenceModel == null)
            {
                return HttpNotFound();
            }
            return View(occurrenceModel);
        }

        // GET: Occurrence/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Occurrence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description")] OccurrenceModel occurrenceModel)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());

                occurrenceModel.Condo = db.CondoModels.First(x => x.ID == user.Condo_ID);
                occurrenceModel.DateOccurrence = DateTime.Now;
                occurrenceModel.Solved = false;
                occurrenceModel.User = db.UserModels.First(x => x.User.Id == user.Id);

                db.OccurrenceModels.Add(occurrenceModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(occurrenceModel);
        }

        // GET: Occurrence/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OccurrenceModel occurrenceModel = db.OccurrenceModels.Find(id);
            if (occurrenceModel == null)
            {
                return HttpNotFound();
            }
            return View(occurrenceModel);
        }

        // POST: Occurrence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,Solved")] OccurrenceModel occurrenceModel)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());

                occurrenceModel.Condo = db.CondoModels.First(x => x.ID == user.Condo_ID);
                occurrenceModel.DateOccurrence = DateTime.Now;

                if (!User.IsInRole("Sindico"))
                    occurrenceModel.Solved = false;

                db.Entry(occurrenceModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(occurrenceModel);
        }

        // GET: Occurrence/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OccurrenceModel occurrenceModel = db.OccurrenceModels.Find(id);
            if (occurrenceModel == null)
            {
                return HttpNotFound();
            }
            return View(occurrenceModel);
        }

        // POST: Occurrence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OccurrenceModel occurrenceModel = db.OccurrenceModels.Find(id);
            db.OccurrenceModels.Remove(occurrenceModel);
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
