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
    [Authorize(Roles = "Sindico,Empregado")]
    public class OutSourcerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OutSourcer
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(db.OutSourcerModels.Include(c => c.Condo).Where(c => c.Condo.ID == user.Condo_ID).ToList());
        }

        // GET: OutSourcer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutSourcerModel outSourcerModel = db.OutSourcerModels.Include(i => i.Address).First(x => x.ID == id);
            if (outSourcerModel == null)
            {
                return HttpNotFound();
            }
            return View(outSourcerModel);
        }

        // GET: OutSourcer/Create
        public ActionResult Create()
        {
            OutSourcerModel model = new OutSourcerModel();
            model.Address = new AddressModel();

            return View(model);
        }

        // POST: OutSourcer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CompanyName,Tel,CNPJ,Site,Contact")] OutSourcerModel outSourcerModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AddressModel address = new AddressModel();
                    address.CEP = Request.Form["CEP"];
                    address.Street = Request.Form["Street"];
                    address.Number = Request.Form["Number"];
                    address.City = Request.Form["City"];
                    address.State = Request.Form["State"];

                    db.AddressModels.Add(address);
                    db.SaveChanges();

                    outSourcerModel.Address = address;

                    var user = db.Users.Find(User.Identity.GetUserId());
                    CondoModel condo = db.CondoModels.Find(user.Condo_ID);

                    outSourcerModel.Condo = condo;

                    db.OutSourcerModels.Add(outSourcerModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }catch
                {
                    ModelState.AddModelError("ErrorOutSourcer", "Ocorreu um erro ao salvar o prestador.");
                    return View(outSourcerModel);
                }
            }

            return View(outSourcerModel);
        }

        // GET: OutSourcer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutSourcerModel outSourcerModel = db.OutSourcerModels.Include(a => a.Address).Include(c => c.Condo).First(x => x.ID == id);
            if (outSourcerModel == null)
            {
                return HttpNotFound();
            }
            return View(outSourcerModel);
        }

        // POST: OutSourcer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CompanyName,Tel,CNPJ,Site,Contact")] OutSourcerModel outSourcerModel)
        {
            if (ModelState.IsValid)
            {
                var outsource = db.OutSourcerModels.Include(a => a.Address)
                                                     .Include(c => c.Condo)
                                                       .First(x => x.ID == outSourcerModel.ID);
                

                AddressModel address = outsource.Address;
                address.CEP = Request.Form["CEP"];
                address.Street = Request.Form["Street"];
                address.Number = Request.Form["Number"];
                address.City = Request.Form["City"];
                address.State = Request.Form["State"];

                outsource.CompanyName = Request.Form["CompanyName"];
                outsource.Tel = Request.Form["Tel"];
                outsource.CNPJ = Request.Form["CNPJ"];
                outsource.Site = Request.Form["Site"];
                outsource.Contact = Request.Form["Contact"];

                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();

                db.Entry(outsource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(outSourcerModel);
        }

        // GET: OutSourcer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutSourcerModel outSourcerModel = db.OutSourcerModels.Include(a => a.Address).First(x => x.ID == id);
            if (outSourcerModel == null)
            {
                return HttpNotFound();
            }
            return View(outSourcerModel);
        }

        // POST: OutSourcer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OutSourcerModel outSourcerModel = db.OutSourcerModels.Include(a => a.Address).First(x => x.ID == id);

            db.AddressModels.Remove(outSourcerModel.Address);
            db.SaveChanges();

            db.OutSourcerModels.Remove(outSourcerModel);
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
