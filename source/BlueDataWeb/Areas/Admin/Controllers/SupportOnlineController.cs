using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminSupport")]
    public class SupportOnlineController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();

        //
        // GET: /Admin/SupportOnline/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.SupportOnlines.OrderBy(s=>s.SupportOnlineId).ToList());
        }

        //
        // GET: /Admin/SupportOnline/Details/5

        [Authorize]
        public ActionResult Details(int id = 0)
        {
            SupportOnline supportonline = db.SupportOnlines.Find(id);
            if (supportonline == null)
            {
                return HttpNotFound();
            }
            return View(supportonline);
        }

        //
        // GET: /Admin/SupportOnline/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/SupportOnline/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(SupportOnline supportonline)
        {
            if (ModelState.IsValid)
            {
                db.SupportOnlines.Add(supportonline);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supportonline);
        }

        //
        // GET: /Admin/SupportOnline/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            SupportOnline supportonline = db.SupportOnlines.Find(id);
            if (supportonline == null)
            {
                return HttpNotFound();
            }
            return View(supportonline);
        }

        //
        // POST: /Admin/SupportOnline/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(SupportOnline supportonline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supportonline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supportonline);
        }

        //
        // GET: /Admin/SupportOnline/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            SupportOnline supportonline = db.SupportOnlines.Find(id);
            if (supportonline == null)
            {
                return HttpNotFound();
            }
            return View(supportonline);
        }

        //
        // POST: /Admin/SupportOnline/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            SupportOnline supportonline = db.SupportOnlines.Find(id);
            db.SupportOnlines.Remove(supportonline);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}