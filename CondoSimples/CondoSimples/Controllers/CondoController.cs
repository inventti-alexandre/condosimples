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
    public class CondoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Condo
        [Authorize]
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(db.CondoModels.Where(x => x.ID == user.Condo_ID).ToList());
        }

        // GET: Condo/Details/5
        [Authorize]
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
        public ActionResult Create([Bind(Include = "ID,Name,ParkingSlots,Address")] CondoModel condoModel)
        {
            if (ModelState.IsValid)
            {
                db.CondoModels.Add(condoModel);
                db.SaveChanges();

                int nTowers = Convert.ToInt32(Request.Form["towers"]);
                int nUnits = Convert.ToInt32(Request.Form["units"]);

                for (int t = 0; t < nTowers; t++)
                {
                    TowerModel tower = new TowerModel();
                    tower.Name = String.Format("Torre {0}", t + 1);
                    tower.Condo_ID = condoModel.ID;
                    tower.Condo = condoModel;

                    db.TowerModels.Add(tower);
                    db.SaveChanges();

                    for (int u = 0; u < nUnits; u++)
                    {
                        UnitModel unit = new UnitModel();
                        unit.Name = String.Format("{0} - Unidade {1}", tower.Name, u + 1);
                        unit.Tower_ID = tower.ID;
                        unit.Tower = tower;

                        db.UnitModels.Add(unit);
                        db.SaveChanges();
                    }
                }

                TempData["adm"] = "S";
                return RedirectToAction("Create", "User", new { condo = condoModel.ID });
            }

            return View(condoModel);
        }

        // GET: Condo/Edit/5
        [Authorize]
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
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Name,ParkingSlots,Address")] CondoModel condoModel)
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
        [Authorize]
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
        [Authorize]
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
