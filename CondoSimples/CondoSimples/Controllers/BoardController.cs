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
    public class BoardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Board
        [Authorize]
        public ActionResult Index()
        {
            IEnumerable<BoardModel> boards = db.BoardModels.Where(x => x.DateExpires >= DateTime.Now).AsEnumerable();
            return View(boards);
        }

        // GET: Board/IndexByUser
        [Authorize]
        public ActionResult IndexByUser()
        {
            string userId = User.Identity.GetUserId();
            IEnumerable<BoardModel> boards = db.BoardModels.Where(x => x.User.Id == userId).AsEnumerable();

            return View(boards);
        }

        // GET: Board/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardModel boardModel = db.BoardModels.Find(id);
            if (boardModel == null)
            {
                return HttpNotFound();
            }
            return View(boardModel);
        }

        // GET: Board/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Board/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Post")] BoardModel boardModel)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(x => x.Id == userId);

                boardModel.User = user;
                boardModel.DatePost = DateTime.Now;
                boardModel.DateExpires = DateTime.Now.AddMonths(1);

                db.BoardModels.Add(boardModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boardModel);
        }

        // GET: Board/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardModel boardModel = db.BoardModels.Find(id);
            if (boardModel == null)
            {
                return HttpNotFound();
            }
            return View(boardModel);
        }

        // POST: Board/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Post,DatePost,DateExpires")] BoardModel boardModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boardModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boardModel);
        }

        // GET: Board/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardModel boardModel = db.BoardModels.Find(id);
            if (boardModel == null)
            {
                return HttpNotFound();
            }
            return View(boardModel);
        }

        // POST: Board/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoardModel boardModel = db.BoardModels.Find(id);
            db.BoardModels.Remove(boardModel);
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
