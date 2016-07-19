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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using CondoSimples.Membership;
using CondoSimples.Azure;

namespace CondoSimples.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        [Authorize]
        public ActionResult Index(string txt)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            
            if (txt != string.Empty && txt != null)
            {
                var userModels = db.UserModels.Include(u => u.Unit).Include(a => a.User).Where(x => x.User.Condo_ID == user.Condo_ID && (x.Name.Contains(txt) 
                                                                                                                                            || x.Pets.Contains(txt) 
                                                                                                                                            || x.Residents.Contains(txt)
                                                                                                                                            || x.Visitors.Contains(txt)
                                                                                                                                            || x.Email.Contains(txt)
                                                                                                                                            || x.Cel.Contains(txt)
                                                                                                                                            || x.CPF.Contains(txt)));
                return View(userModels.ToList());
            }else
            {
                var userModels = db.UserModels.Include(u => u.Unit).Include(a => a.User).Where(x => x.User.Condo_ID == user.Condo_ID);
                return View(userModels.ToList());
            } 
        }

        // GET: User/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.UserModels.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.Image = StorageHandler.GetImageUri("user_" + userModel.ID + ".jpg");

            return View(userModel);
        }

        // GET: User/Create
        [AllowAnonymous]
        public ActionResult Create(int? condo)
        {
            if (condo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CondoModel condoModel = db.CondoModels.Find(condo);
            if (condoModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.TowerId = new SelectList(db.TowerModels.Include(c => c.Condo).Where(x => x.Condo.ID == condo).ToList(), "Id", "Name");
            ViewBag.CondoId = condo;

            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CPF,Name,Birthdate,Cel,Phone,Email,EmailOthers,Residents,Pets,Cars,Visitors")] UserModel userModel, HttpPostedFileBase Image, int idCondo)
        {
            //int idCondo = 0;

            if (ModelState.IsValid)
            {
                MembershipHandler membership = new MembershipHandler();

                var user = new ApplicationUser { UserName = userModel.Email, Email = userModel.Email, Condo_ID = idCondo };
                membership.CreateUser(user, Request.Form["pass"]);
                
                if(TempData["adm"] != null)
                {
                    if (TempData["adm"].ToString() == "S")
                    {
                        membership.SetRoleSindico(user.Id);
                        TempData.Clear();
                    }
                }
                else
                {
                    membership.SetRoleCondomino(user.Id);
                }

                userModel.User = db.Users.FirstOrDefault(x => x.Email == userModel.Email);


                var towerId = Convert.ToInt32(Request.Form["TowerId"]);
                var tower = db.TowerModels.FirstOrDefault(x => x.ID == towerId);

                UnitModel unit = new UnitModel();
                unit.Name = String.Format(Request.Form["Unit"]);
                unit.Tower_ID = tower.ID;
                unit.Tower = tower;

                db.UnitModels.Add(unit);
                db.SaveChanges();

                userModel.Unit = unit;

                db.UserModels.Add(userModel);
                db.SaveChanges();

                if (Image != null)
                    StorageHandler.UploadImage(userModel.ID.ToString(), Image, "user_");

                membership.Login(user, HttpContext);
                return RedirectToAction("Index", "Home", "");
            }

            ViewBag.TowerId = new SelectList(db.TowerModels.Include(c => c.Condo).Where(x => x.Condo.ID == idCondo).ToList(), "Id", "Name");
            return View(userModel);
        }

        // GET: User/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.UserModels.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Unit_ID = new SelectList(db.UnitModels, "ID", "Name", userModel.Unit_ID);
            return View(userModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,CPF,Name,Birthdate,Cel,Phone,Email,EmailOthers,Residents,Pets,Cars,Visitors,Unit_ID")] UserModel userModel, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();

                if (Image != null)
                    StorageHandler.UploadImage(userModel.ID.ToString(), Image, "user_");

                return RedirectToAction("Index");
            }
            ViewBag.Unit_ID = new SelectList(db.UnitModels, "ID", "Name", userModel.Unit_ID);            

            return View(userModel);
        }

        // GET: User/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.UserModels.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            UserModel userModel = db.UserModels.Find(id);
            db.UserModels.Remove(userModel);
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
