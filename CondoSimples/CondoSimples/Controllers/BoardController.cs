using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CondoSimples.Models;
using CondoSimples.Membership;
using Microsoft.AspNet.Identity;
using CondoSimples.Mail;

namespace CondoSimples.Controllers
{
    public class BoardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Board
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(x => x.Id == userID);

            BoardModel activePost = db.BoardModels.FirstOrDefault(x => x.User.Id == userID && x.DateExpires > DateTime.Now);
            ViewBag.ActivePost = activePost != null ? "S" : "N";

            return View(db.BoardModels.Where(x => x.Published == true && x.DateExpires > DateTime.Now && x.User.Condo_ID == user.Condo_ID).ToList());
        }

        // GET: Board/IndexByUser
        public ActionResult IndexByUser()
        {
            string userId = User.Identity.GetUserId();
            return View(db.BoardModels.Where(x => x.User.Id == userId).ToList());
        }

        // GET: Board/IndexAdmin
        public ActionResult IndexAdmin()
        {
            string userID = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(x => x.Id == userID);
            return View(db.BoardModels.Where(x => x.Published == false && x.DateExpires > DateTime.Now && x.User.Condo_ID == user.Condo_ID).ToList());
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
        public ActionResult Create([Bind(Include = "Id,Post,DatePost,DateExpires,Published")] BoardModel boardModel)
        {
            if (ModelState.IsValid)
            {
                boardModel.DatePost = DateTime.Now;
                boardModel.DateExpires = DateTime.Now.AddMonths(1);

                if (!User.IsInRole(MembershipHandler.SINDICOROLE))
                    boardModel.Published = false;

                string userID = User.Identity.GetUserId();
                ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userID);
                boardModel.User = user;

                db.BoardModels.Add(boardModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boardModel);
        }

        // GET: Board/Publish/5
        public ActionResult Publish(int? id)
        {
            try
            {
                BoardModel boardModel = db.BoardModels.Find(id);
                boardModel.Published = true;
                db.Entry(boardModel).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("IndexAdmin");
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas ao publicar: " + ex.Message);
            }
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
        public ActionResult Edit([Bind(Include = "Id,Post,DatePost,DateExpires,Published")] BoardModel boardModel)
        {
            if (ModelState.IsValid)
            {
                boardModel.DatePost = DateTime.Now;
                boardModel.DateExpires = DateTime.Now.AddMonths(1);

                if (!User.IsInRole(MembershipHandler.SINDICOROLE))
                    boardModel.Published = false;

                string userID = User.Identity.GetUserId();
                ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userID);
                boardModel.User = user;

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

            MailHandler.SendMail("Sua publicação no mural de avisos foi considerada inadequada pela moderação do seu condomínio.",
                                    boardModel.User.Email,
                                    "CondoSimples - Publicação não aprovada");

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
