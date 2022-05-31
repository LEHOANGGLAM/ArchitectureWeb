using BlueDataWeb.Helpers;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Utility;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminPartners")]
    public class PartnersController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        private int width = 320;
        private int height = 210;

        
    

        public ActionResult Index()
        {
            var images = db.Images.Where(m => m.Type == "E_Partners" & m.AppID == this.AppID).OrderByDescending(x => x.ImageId);
            return View(images.ToList());
        }

        //
        // GET: /Admin/Image/Create

        public ActionResult Create()
        {
            var data = new Image();
            data.AppID = this.AppID;
            return View(data);
        }

        //
        // POST: /Admin/Image/Create

        [HttpPost]
        public ActionResult Create(Image image)
        {
            if (ModelState.IsValid)
            {
                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        image.ImagePath = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image

                image.Type = "E_Partners";
                db.Images.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(image);
        }

        //
        // GET: /Admin/Image/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Image image = db.Images.Where(x=>x.ImageId == id && x.AppID == this.AppID).FirstOrDefault();
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        //
        // POST: /Admin/Image/Edit/5

        [HttpPost]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        image.ImagePath = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image

                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(image);
        }

        //
        // GET: /Admin/Image/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        //
        // POST: /Admin/Image/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
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