﻿using System;
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
using CondoSimples.Azure;

namespace CondoSimples.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employee
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(db.EmployeeModels.Include(u => u.User).Where(x => x.User.Condo_ID == user.Condo_ID).ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeModel employeeModel = db.EmployeeModels.Find(id);
            if (employeeModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.Image = StorageHandler.GetImageUri("employee_" + employeeModel.ID + ".jpg");

            return View(employeeModel);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,Cel,Position,DutyDays,WorkShift")] EmployeeModel employeeModel, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                var userAdm = db.Users.Find(userId);

                MembershipHandler membership = new MembershipHandler();
                var user = new ApplicationUser { UserName = employeeModel.Email, Email = employeeModel.Email, Condo_ID = userAdm.Condo_ID };

                membership.CreateUser(user, Request.Form["pass"]);
                membership.SetRoleEmpregado(user.Id);

                employeeModel.User = db.Users.Find(user.Id);

                db.EmployeeModels.Add(employeeModel);
                db.SaveChanges();

                if (Image != null)
                    StorageHandler.UploadImage(employeeModel.ID.ToString(), Image, "employee_");

                return RedirectToAction("Index");
            }

            return View(employeeModel);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeModel employeeModel = db.EmployeeModels.Find(id);
            if (employeeModel == null)
            {
                return HttpNotFound();
            }
            return View(employeeModel);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,Cel,Position,DutyDays,WorkShift")] EmployeeModel employeeModel, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeModel).State = EntityState.Modified;
                db.SaveChanges();
                
                if(Image != null)
                    StorageHandler.UploadImage(employeeModel.ID.ToString(), Image, "employee_");

                return RedirectToAction("Index");
            }
            return View(employeeModel);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeModel employeeModel = db.EmployeeModels.Find(id);
            if (employeeModel == null)
            {
                return HttpNotFound();
            }
            return View(employeeModel);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeModel employeeModel = db.EmployeeModels.Find(id);
            db.EmployeeModels.Remove(employeeModel);
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
