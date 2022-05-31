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
    [Authorize(Roles = "Admin, AdminSendMail")]
    public class SubEmailController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        EmailService emailservice = new EmailService();

        //
        // GET: /Admin/SubEmail/

        public ActionResult Index()
        {
            var lstuser = db.Users.ToList();
            ViewBag.lstuser = lstuser;
            return View(db.SubEmails.ToList());
        }

        //
        // GET: /Admin/SubEmail/Details/5

        public ActionResult Details(int id = 0)
        {
            SubEmail subemail = db.SubEmails.Find(id);
            if (subemail == null)
            {
                return HttpNotFound();
            }
            return View(subemail);
        }

        //
        // GET: /Admin/SubEmail/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/SubEmail/Create

        [HttpPost]
        public ActionResult Create(SubEmail subemail)
        {
            if (ModelState.IsValid)
            {
                db.SubEmails.Add(subemail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subemail);
        }

        //
        // GET: /Admin/SubEmail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SubEmail subemail = db.SubEmails.Find(id);
            if (subemail == null)
            {
                return HttpNotFound();
            }
            return View(subemail);
        }

        //
        // POST: /Admin/SubEmail/Edit/5

        [HttpPost]
        public ActionResult Edit(SubEmail subemail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subemail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subemail);
        }

        //
        // GET: /Admin/SubEmail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SubEmail subemail = db.SubEmails.Find(id);
            if (subemail == null)
            {
                return HttpNotFound();
            }
            return View(subemail);
        }
        public ActionResult DeleteConfirmedUser(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult SendMail(SubEmail subemail)
        {
            
            if (Session["UsernameAd"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string emailto;
           
            SendEmail sendemail = db.SendEmails.Where(m => m.Name == "Newsletter").FirstOrDefault();
            string subject = sendemail.Subject;
            //string body = sendemail.Body;
            if (sendemail != null)
            {
                var lstSubemail = db.SubEmails.ToList();
                var lstuser = db.Users.ToList();
                foreach (var sub in lstSubemail)
                {
                    var chk = Request.Form["checkSub" + sub.ID];
                    if (chk == "on")
                    {
                        SubEmail subtest = db.SubEmails.Where(m => m.Email == sub.Email).FirstOrDefault();
                        string body = sendemail.Body + "http://dealtop1.com/SubscribeEmail/Unsubscribe" + subtest.CodeActive;
                        emailto = sub.Email;
                        //bool sendmail = emailservice.SendMail_By_Gmail(emailto, subject, body);
                        emailservice.SendEmail(sendemail, emailto);
                        if (ModelState.IsValid)
                        {
                            subtest.Status = true;
                            db.Entry(subtest).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                    }
                }
                foreach (var user in lstuser)
                {
                    var chk = Request.Form["checkUser" + user.Id];
                    if (chk == "on")
                    {
                        User usertest = db.Users.Where(m => m.Email == user.Email).FirstOrDefault();
                        string body = sendemail.Body + "http://dealtop1.com/SubscribeEmail/Unsubscribe" + usertest.CodeActive;
                        emailto = user.Email;
                        //bool sendmail = emailservice.SendMail_By_Gmail(emailto, subject, body);
                        emailservice.SendEmail(sendemail, emailto);
                        if (ModelState.IsValid)
                        {
                            usertest.Status = true;
                            db.Entry(usertest).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                    }
                }
            }

            return RedirectToAction("Index", "SubEmail");
        }
        //
        // POST: /Admin/SubEmail/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SubEmail subemail = db.SubEmails.Find(id);
            db.SubEmails.Remove(subemail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteConfirmedUser")]
        public ActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            user.IsActive = false;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Reset()
        {
            var lstsub = db.SubEmails.ToList();
            var lstuser = db.Users.ToList();

            foreach (var sub in lstsub)
            {
                sub.Status = false;
                if (ModelState.IsValid)
                {

                    db.Entry(sub).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            foreach (var user in lstuser)
            {
                user.Status = false;
                if (ModelState.IsValid)
                {

                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "SubEmail");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}