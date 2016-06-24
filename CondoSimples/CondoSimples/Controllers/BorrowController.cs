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
    public class BorrowController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Borrow
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(db.BorrowModels.Include(u => u.UserRequest).Include(x => x.UserLending).Where(y => y.DateComplete == null && y.UserRequest.Id != user.Id && y.DateReturn > DateTime.Now && y.UserRequest.Condo_ID == user.Condo_ID && y.UserLending == null).ToList());
        }

        // GET: Borrow/IndexByUser
        public ActionResult IndexByUser()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(db.BorrowModels.Include(u => u.UserRequest).Include(x => x.UserLending).Where(y => y.DateComplete == null && y.UserRequest.Id == user.Id && y.DateReturn > DateTime.Now).ToList());
        }

        // GET: Borrow/IndexByUser
        public ActionResult Lended()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(db.BorrowModels.Include(u => u.UserRequest).Include(x => x.UserLending).Where(y => y.UserLending.Id == user.Id).ToList());
        }

        // GET: Borrow/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowModel borrowModel = db.BorrowModels.Find(id);
            if (borrowModel == null)
            {
                return HttpNotFound();
            }
            return View(borrowModel);
        }

        // GET: Borrow/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Borrow/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,Quantity,DateRequire,DateReturn,DateComplete,IdUserRequest,IdUserLending")] BorrowModel borrowModel)
        {
            if (ModelState.IsValid)
            {
                borrowModel.DateRequire = DateTime.Now;
                borrowModel.UserRequest = db.Users.Find(User.Identity.GetUserId());

                db.BorrowModels.Add(borrowModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(borrowModel);
        }

        // GET: Borrow/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowModel borrowModel = db.BorrowModels.Find(id);
            if (borrowModel == null)
            {
                return HttpNotFound();
            }
            return View(borrowModel);
        }

        // POST: Borrow/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,Quantity,DateRequire,DateReturn,DateComplete,IdUserRequest,IdUserLending")] BorrowModel borrowModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrowModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(borrowModel);
        }

        // GET: Borrow/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowModel borrowModel = db.BorrowModels.Find(id);
            if (borrowModel == null)
            {
                return HttpNotFound();
            }
            return View(borrowModel);
        }

        // POST: Borrow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BorrowModel borrowModel = db.BorrowModels.Find(id);
            db.BorrowModels.Remove(borrowModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Borrow/Create
        public ActionResult Lend(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowModel borrowModel = db.BorrowModels.Find(id);
            if (borrowModel == null)
            {
                return HttpNotFound();
            }

            borrowModel.UserLending = db.Users.Find(User.Identity.GetUserId());

            db.Entry(borrowModel).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Lended");
        }

        // GET: Borrow/Return
        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowModel borrowModel = db.BorrowModels.Find(id);
            if (borrowModel == null)
            {
                return HttpNotFound();
            }

            borrowModel.DateComplete = DateTime.Now;

            db.Entry(borrowModel).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Lended");
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
