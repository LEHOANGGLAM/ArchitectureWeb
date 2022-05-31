using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Models;
using BlueDataWeb.Models.Enum;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();

        //
        // GET: /Admin/Contact/

        public ActionResult Index()
        {
            var data = db.Contacts.AsNoTracking().Where(x => x.AppID == this.AppID).OrderByDescending(m => m.ContactId).ToList();
            return View(data);
        }

        //
        // GET: /Admin/Contact/Details/5

        public ActionResult Details(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        //
        

        public ActionResult EditDichVu(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.Status = new SelectList((new DataModel()).getTrangThaiLienHe(), "ID", "Value", contact.Status);
            ViewBag.DichVu = new SelectList((new DataModel()).getGoiDichVu(), "ID", "Value", contact.Status);


            return View(contact);
        }

        //
        // POST: /Admin/Contact/Edit/5

        [HttpPost]
        public ActionResult EditDichVu(Contact contact)
        {
            if (ModelState.IsValid)
            {

 
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.Status = new SelectList((new DataModel()).getTrangThaiLienHe(), "ID", "Value", contact.Status);
            ViewBag.DichVu = new SelectList((new DataModel()).getGoiDichVu(), "ID", "Value", contact.Status);

            return View(contact);
        }

   
        
        //
        // GET: /Admin/Contact/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        //
        // POST: /Admin/Contact/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
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