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
    public class CommonPlaceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CommonPlace
        public ActionResult Index()
        {
            return View(db.CommonPlaceModels.ToList());
        }

        // GET: CommonPlace/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommonPlaceModel commonPlaceModel = db.CommonPlaceModels.Find(id);
            if (commonPlaceModel == null)
            {
                return HttpNotFound();
            }
            return View(commonPlaceModel);
        }

        // GET: CommonPlace/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommonPlace/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,Capacity,IncludeItens,Active,Terms")] CommonPlaceModel commonPlaceModel)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                var condo = db.CondoModels.FirstOrDefault(x => x.ID == user.Condo_ID);

                commonPlaceModel.Condo = condo;

                db.CommonPlaceModels.Add(commonPlaceModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commonPlaceModel);
        }

        // GET: CommonPlace/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommonPlaceModel commonPlaceModel = db.CommonPlaceModels.Find(id);
            if (commonPlaceModel == null)
            {
                return HttpNotFound();
            }
            return View(commonPlaceModel);
        }

        // POST: CommonPlace/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Capacity,IncludeItens,Active,Terms")] CommonPlaceModel commonPlaceModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commonPlaceModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commonPlaceModel);
        }

        // GET: CommonPlace/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommonPlaceModel commonPlaceModel = db.CommonPlaceModels.Find(id);
            if (commonPlaceModel == null)
            {
                return HttpNotFound();
            }
            return View(commonPlaceModel);
        }

        // POST: CommonPlace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommonPlaceModel commonPlaceModel = db.CommonPlaceModels.Find(id);
            db.CommonPlaceModels.Remove(commonPlaceModel);
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
