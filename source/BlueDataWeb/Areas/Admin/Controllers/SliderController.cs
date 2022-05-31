using BlueDataWeb.Areas.Admin.Models.Service;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Models.Enum;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminSlider")]
    public class SliderController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        private ImageService imgservice = new ImageService();

        public ActionResult Index()
        {
            var lstType = new DataModel().GetTypeSlider().Select(p => p.ID).ToList();
            var data = db.Images.AsNoTracking().Where(m => lstType.Contains(m.Type) && m.AppID == this.AppID).ToList();
            return View(data);
        }

        public ActionResult Create(int id = 0)
        {
            ViewBag.Type = new SelectList(new DataModel().GetTypeSlider(), "ID", "Value");

            Image data = new Image();
            data.AppID = this.AppID;

            if (id > 0)
            {
                data = db.Images.Find(id);
            }
            return View(data);
        }

        //
        // POST: /Admin/Image/Create

        //[HttpPost]
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Image model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    #region xử lý upload image

                    if (Request.Files["img"] != null)
                    {
                        string fullname = Request.Files["img"].FileName;
                        if (fullname != "")
                        {
                            model.ImgName = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "Sliders");
                        }
                    }

                    #endregion xử lý upload image

                    if (model.ImageId == 0)
                    {
                        db.Images.Add(model);
                    }
                    else
                    {
                        db.Entry(model).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    TempData["Status"] = $"Cập nhật thành công";

                    return Redirect("/admin/Slider/Create?id=" + model.ImageId);
                }
                catch (Exception ex)
                {
                    TempData["Status"] = $"Cập nhật lỗi {ex.StackTrace}";
                }
            }

            return View(model);
        }

        public ActionResult Delete(int id = 0)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

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