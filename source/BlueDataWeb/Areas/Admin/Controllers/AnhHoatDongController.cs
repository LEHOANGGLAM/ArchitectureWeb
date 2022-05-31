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
    public class AnhHoatDongController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        private int width = 495;
        private int height = 369;

        private int width_s = 150;
        private int height_s = 100;
        // GET: /Admin/Image/

        public ActionResult Index()
        {
            var images = db.Images.Where(m => m.Type == "E_AnhHoatDong").OrderByDescending(x => x.ImageId);
            return View(images.ToList());
        }

        //
        // GET: /Admin/Image/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Image/Create

        [HttpPost]
        public ActionResult Create(Image image)
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
                        string img_name = HelperSEO.ToSeoImage(DateTime.Now.Ticks.ToString());

                        var image_upload = img_name;
                        ImageHelper.UploadFile(Server.MapPath("~/Images/AnhHoatDong/"), file0, width, height, ref image_upload);
                        image.ImgName = image_upload;

                        image_upload = img_name;

                        ImageHelper.UploadFile(Server.MapPath("~/Images/AnhHoatDong/Small/"), file0, width_s, height_s, ref image_upload);

                        string path = System.IO.Path.Combine(
                                        Server.MapPath("~/Images/AnhHoatDong/Full/"), image_upload);
                        file0.SaveAs(path);
                    }
                }

                #endregion xử lý upload image

                image.Type = "E_AnhHoatDong";
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
            Image image = db.Images.Find(id);
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

                String NamePro = null;
                if (Request.Files["img"] != null)
                {
                    HttpPostedFileBase file0 = Request.Files["img"];
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        NamePro = fullname.Substring(1 + fullname.LastIndexOf("\\"));
                        string img_name = HelperSEO.ToSeoImage(DateTime.Now.Ticks.ToString());

                        var image_upload = img_name;
                        ImageHelper.UploadFile(Server.MapPath("~/Images/AnhHoatDong/"), file0, width, height, ref image_upload);
                        image.ImgName = image_upload;

                        image_upload = img_name;

                        ImageHelper.UploadFile(Server.MapPath("~/Images/AnhHoatDong/Small/"), file0, width_s, height_s, ref image_upload);

                        string path = System.IO.Path.Combine(
                                        Server.MapPath("~/Images/AnhHoatDong/Full/"), image_upload);
                        file0.SaveAs(path);
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