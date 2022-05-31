using BlueDataWeb.Areas.Admin.Models.Service;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminCategory")]
    public class ImagePageController : Controller
    {
        private ImageService imgservice = new ImageService();

        private BlueDataWebEntities db = new BlueDataWebEntities();

  

        //
        // GET: /Admin/Category/

        public ActionResult Index()
        {
            var images = db.ImagePages;
            return View(images.ToList());
        }

        //
        // GET: /Admin/Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Category/Create

        [HttpPost]
        public ActionResult Create(ImagePage imagePage)
        {
            if (ModelState.IsValid)
            {
                

                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        imagePage.Image = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image



                db.ImagePages.Add(imagePage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(imagePage);
        }

        //
        // GET: /Admin/Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ImagePage imagePage = db.ImagePages.Find(id);

            return View(imagePage);
        }

        //
        // POST: /Admin/Category/Edit/5

        [HttpPost]
        public ActionResult Edit(ImagePage imagePage)
        {
            if (ModelState.IsValid)
            {
                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        imagePage.Image = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image


                db.Entry(imagePage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imagePage);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}