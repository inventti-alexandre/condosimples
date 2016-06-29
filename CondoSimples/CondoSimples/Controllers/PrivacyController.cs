using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CondoSimples.Controllers
{
    public class PrivacyController : Controller
    {
        // GET: Privacy
        public ActionResult Index()
        {
            return View();
        }

        // GET: Privacy/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Privacy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Privacy/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Privacy/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Privacy/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Privacy/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Privacy/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
