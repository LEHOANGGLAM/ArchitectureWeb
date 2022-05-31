using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Utility;
using BlueDataWeb.Areas.Admin.Models.Service;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminTransport")]
    public class TransportController : Controller
    {
        ImageService imgservice = new ImageService();
        private BlueDataWebEntities db = new BlueDataWebEntities();
        public string type = "E_Transport";
        //
        // GET: /Admin/AboutCategory/

        public ActionResult Index()
        {
            int pageNo = 0;
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("PageNo:"))
                {
                    pageNo = int.Parse(key.Substring(7));
                    break;
                }
            }
            PagerInfo pi = PagerInfo.Get("AboutCategory", 50);
            pi.RowCount = db.AboutCategories.OrderByDescending(p => p.AboutID).Count();
            pi.Navigate(pageNo);
            int StartRow = pi.PageNo * pi.PageSize;
            var abcategory = db.AboutCategories.OrderByDescending(a => a.AboutID).Where(m => m.Type == type).Skip(StartRow).Take(pi.PageSize);
            return View(abcategory);
        }
        [HttpPost, ActionName("Index")]
        public ActionResult Index1()
        {
            int pageNo = 0;
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("PageNo:"))
                {
                    pageNo = int.Parse(key.Substring(7));
                    break;
                }
            }
            PagerInfo pi = PagerInfo.Get("AboutCategory", 50);
            pi.RowCount = db.AboutCategories.OrderByDescending(p => p.AboutID).Count();
            pi.Navigate(pageNo);
            int StartRow = pi.PageNo * pi.PageSize;
            var abcategory = db.AboutCategories.OrderByDescending(a => a.AboutID).Where(m => m.Type == type).Skip(StartRow).Take(pi.PageSize);
            return PartialView("IndexAjax", abcategory);
        }

        //
        // GET: /Admin/AboutCategory/Details/5

        public ActionResult Details(int id = 0)
        {
            AboutCategory aboutcategory = db.AboutCategories.Find(id);
            if (aboutcategory == null)
            {
                return HttpNotFound();
            }
            return View(aboutcategory);
        }

        //
        // GET: /Admin/AboutCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/AboutCategory/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(AboutCategory aboutcategory)
        {
            if (ModelState.IsValid)
            {
                #region xử lý upload image

                String NamePro = null;
                if (Request.Files["LargeImage"] != null)
                {
                    HttpPostedFileBase file0 = Request.Files["LargeImage"];
                    string fullname = Request.Files["LargeImage"].FileName;
                    if (fullname != "")
                    {
                        NamePro = fullname.Substring(1 + fullname.LastIndexOf("\\"));
                        string rename = HelperSEO.ToSeoImage(DateTime.Now.Ticks + NamePro.Substring(NamePro.LastIndexOf(".")));
                        imgservice.Resize(Server.MapPath("~/Images/Service/"), rename, 615, 315, file0.InputStream);
                        aboutcategory.LargeImage = rename;
                    }
                }

                String NamePro1 = null;
                if (Request.Files["SmallImage"] != null)
                {
                    HttpPostedFileBase file0 = Request.Files["SmallImage"];
                    string fullname = Request.Files["SmallImage"].FileName;
                    if (fullname != "")
                    {
                        NamePro1 = fullname.Substring(1 + fullname.LastIndexOf("\\"));
                        string rename = HelperSEO.ToSeoImage(DateTime.Now.Ticks + NamePro1.Substring(NamePro.LastIndexOf(".")));
                        imgservice.Resize(Server.MapPath("~/Images/Service/"), rename, 200, 140, file0.InputStream);
                        aboutcategory.SmallImage = rename;
                    }
                }

                #endregion
                aboutcategory.Type = type;
                db.AboutCategories.Add(aboutcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Parentcate = new SelectList(db.AboutCategories.Where(m => m.Parentcate == null), "AboutID", "Name", aboutcategory.Parentcate);
            return View(aboutcategory);
        }

        //
        // GET: /Admin/AboutCategory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AboutCategory aboutcategory = db.AboutCategories.Find(id);
            if (aboutcategory == null)
            {
                return HttpNotFound();
            }

            ViewBag.Parentcate = new SelectList(db.AboutCategories.Where(m => m.Parentcate == null), "AboutID", "Name", aboutcategory.Parentcate);

            return View(aboutcategory);
        }

        //
        // POST: /Admin/AboutCategory/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(AboutCategory aboutcategory)
        {
            if (ModelState.IsValid)
            {

                 


                #region xử lý upload image

                String NamePro = null;
                if (Request.Files["img"] != null)
                {
                    HttpPostedFileBase file0 = Request.Files["img"];
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        NamePro = fullname.Substring(1 + fullname.LastIndexOf("\\"));
                        string rename = HelperSEO.ToSeoImage(DateTime.Now.Ticks + NamePro.Substring(NamePro.LastIndexOf(".")));
                        imgservice.Resize(Server.MapPath("~/Images/Service/"), rename, 615, 315, file0.InputStream);
                        aboutcategory.LargeImage = rename;
                    }
                }

                String NamePro1 = null;
                if (Request.Files["SmallImage"] != null)
                {
                    HttpPostedFileBase file0 = Request.Files["SmallImage"];
                    string fullname = Request.Files["SmallImage"].FileName;
                    if (fullname != "")
                    {
                        NamePro1 = fullname.Substring(1 + fullname.LastIndexOf("\\"));
                        string rename = HelperSEO.ToSeoImage(DateTime.Now.Ticks + NamePro1.Substring(NamePro1.LastIndexOf(".")));
                        imgservice.Resize(Server.MapPath("~/Images/Service/"), rename, 200, 140, file0.InputStream);
                        aboutcategory.SmallImage = rename;
                    }
                }

                #endregion
                db.Entry(aboutcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.Parentcate = new SelectList(db.AboutCategories.Where(m => m.Parentcate == 0), "AboutID", "Name", aboutcategory.Parentcate);

            return View(aboutcategory);
        }

        //
        // GET: /Admin/AboutCategory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AboutCategory aboutcategory = db.AboutCategories.Find(id);
            if (aboutcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.Parentcate = new SelectList(db.AboutCategories, "AboutID", "Name", aboutcategory.Parentcate);
            return View(aboutcategory);
        }

        //
        // POST: /Admin/AboutCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutCategory aboutcategory = db.AboutCategories.Find(id);
            db.AboutCategories.Remove(aboutcategory);
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