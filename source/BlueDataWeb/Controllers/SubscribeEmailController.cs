using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;
using System.Data.Entity;
using System.Data;

namespace BlueDataWeb.Controllers
{
    public class SubscribeEmailController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        //
        // GET: /SubscribeEmail/

        public ActionResult Index()
        {
            return View();
        }
        public static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();

            //
            // GET: /User/Edit/5
        }
        public ActionResult Subscribe()
        {
            return View();
        }
        [HttpPost]
         public ActionResult Subscribe(SubEmail sub)
         {
             SubEmail subtest = db.SubEmails.Where(m => m.Email.Contains(sub.Email.Trim())).FirstOrDefault();
             if (sub.Email == null || subtest != null )
             {
                 if (sub.Email == "")
                     TempData["message"] = "Vui lòng nhập Email";
                 else
                     TempData["message"] = "Email đã đăng ký";

                 return RedirectToAction("Index","Home");
             }
             sub.IsActive = true;
             sub.Status = false;
             sub.CodeActive = "?"+ md5(sub.Email);
             
             if (ModelState.IsValid)
             {
                 db.SubEmails.Add(sub);
                 db.SaveChanges();
                 TempData["message"] = "Đăng ký thành công!";
                 return RedirectToAction("Index", "Home");
             }
             return View();
         }

        public ActionResult Unsubscribe()
        {
            string Query = Request.Url.Query;
            SubEmail subtest = db.SubEmails.Where(m => m.CodeActive == Query).FirstOrDefault();
            User usertest = db.Users.Where(m => m.CodeActive == Query).FirstOrDefault();
            if (subtest != null)
            {
                if (ModelState.IsValid)
                {
                    subtest.IsActive = false;
                    db.Entry(subtest).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            
            if (usertest != null)
            {
                if (ModelState.IsValid)
                {
                    usertest.IsActive = false;
                    db.Entry(usertest).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            return View();

        }

        //[HttpPost]
        //public ActionResult Unsubscribe(SubEmail sub)
        //{
           

        //    return View();

        //}

    }
}
