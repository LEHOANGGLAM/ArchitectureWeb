using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Models.Service;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminSetting")]
    public class SendEmailController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        EmailService emailservice = new EmailService();


        //
        // GET: /Admin/SendEmail/

        public ActionResult Index()
        {
            return View(db.SendEmails.ToList());
        }

        //
        // GET: /Admin/SendEmail/Details/5

        public ActionResult Details(int id = 0)
        {
            SendEmail sendemail = db.SendEmails.Find(id);
            if (sendemail == null)
            {
                return HttpNotFound();
            }
            return View(sendemail);
        }

        //
        // GET: /Admin/SendEmail/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/SendEmail/Create

        [HttpPost]
        public ActionResult Create(SendEmail sendemail)
        {
            if (ModelState.IsValid)
            {
                db.SendEmails.Add(sendemail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sendemail);
        }

        //
        // GET: /Admin/SendEmail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SendEmail sendemail = db.SendEmails.Find(id);
            if (sendemail == null)
            {
                return HttpNotFound();
            }
            return View(sendemail);
        }

        //
        // POST: /Admin/SendEmail/Edit/5
        [ValidateInput(false)]//Fix bug FCK
        [HttpPost]
        public ActionResult Edit(SendEmail sendemail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sendemail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sendemail);
        }

        //
        // GET: /Admin/SendEmail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SendEmail sendemail = db.SendEmails.Find(id);
            if (sendemail == null)
            {
                return HttpNotFound();
            }
            return View(sendemail);
        }

        //
        // POST: /Admin/SendEmail/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {           
            SendEmail sendemail = db.SendEmails.Find(id);
            db.SendEmails.Remove(sendemail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult SendMail()
        {
            string emailto;
            SendEmail sendemail = db.SendEmails.Where(m => m.Name == "Newsletter").FirstOrDefault();
            if (sendemail != null)
            {
                var lstSubemail = db.SubEmails.Take(10).ToList();
                foreach (var sub in lstSubemail)
                {
                    emailto = sub.Email;
                    emailservice.SendEmail(sendemail, emailto);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}