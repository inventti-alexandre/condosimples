﻿using System;
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

namespace CondoSimples.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            var userModels = db.UserModels.Include(u => u.Unit);
            return View(userModels.ToList());
        }

        // GET: User/Details/5
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
            return View(userModel);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.Unit_ID = new SelectList(db.UnitModels, "ID", "Name");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CPF,Name,Birthdate,Cel,Email,Residents,Pets,Cars,Visitors,Unit_ID")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {

                MembershipHandler membership = new MembershipHandler();

                int idCondo = Convert.ToInt32(Request["condo"]);
                CondoModel condoddl = db.CondoModels.FirstOrDefault(x => x.ID == idCondo);

                var user = new ApplicationUser { UserName = userModel.Email, Email = userModel.Email, Condo_ID = idCondo };
                membership.CreateUser(user, Request.Form["pass"]);
                
                if(Request["adm"] == "S")
                {
                    membership.SetRoleSindico(user.Id);
                }
                else
                {
                    membership.SetRoleCondomino(user.Id);
                }

                userModel.User = db.Users.FirstOrDefault(x => x.Email == userModel.Email);

                db.UserModels.Add(userModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Unit_ID = new SelectList(db.UnitModels, "ID", "Name", userModel.Unit_ID);
            return View(userModel);
        }

        // GET: User/Edit/5
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
        public ActionResult Edit([Bind(Include = "ID,CPF,Name,Birthdate,Cel,Email,Residents,Pets,Cars,Visitors,Unit_ID")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Unit_ID = new SelectList(db.UnitModels, "ID", "Name", userModel.Unit_ID);
            return View(userModel);
        }

        // GET: User/Delete/5
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