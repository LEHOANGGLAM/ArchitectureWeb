using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpressBD.Models;
using ExpressBD.Utility;
using ExpressBD.Areas.Admin.Models.Service;

namespace ExpressBD.Areas.Admin.Controllers
{
    public class DocumentController : Controller
    {
        private ExpressBDEntities db = new ExpressBDEntities();
        ImageService imgservice = new ImageService();
        //
        // GET: /Admin/Document/

        public ActionResult Index()
        {
            return View(db.Documents.ToList());
        }

        //
        // GET: /Admin/Document/Details/5

        public ActionResult Details(long id = 0)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // GET: /Admin/Document/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Document/Create

        [HttpPost]
      
        public ActionResult Create(Document document)
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
                        file0.SaveAs(Server.MapPath("~/Content/Document/" + rename));
                        document.DocFileName = rename;
                        document.DateCreate = DateTime.Now;
                    }
                }
                #endregion

                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(document);
        }

        //
        // GET: /Admin/Document/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // POST: /Admin/Document/Edit/5

        [HttpPost]
       
        public ActionResult Edit(Document document)
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
                        file0.SaveAs(Server.MapPath("~/Content/Document/" + rename));
                        document.DocFileName = rename;
                    }
                }
                #endregion

                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        //
        // GET: /Admin/Document/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // POST: /Admin/Document/Delete/5

        [HttpPost, ActionName("Delete")]
      
        public ActionResult DeleteConfirmed(long id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
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