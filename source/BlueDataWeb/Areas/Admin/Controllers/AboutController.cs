using BlueDataWeb.Areas.Admin.Models.Service;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    public class AboutController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        private ImageService imgservice = new ImageService();

        public ActionResult Index()
        {
            List<About> lstAbout = db.Abouts.Where(x => x.AppID == this.AppID).OrderBy(x => x.Order_by).ToList();
            return View(lstAbout);
        }

        #region Create

        public ActionResult Create(int id = 0)
        {
            if (id > 0)
            {
                About About = db.Abouts.Where(m => m.ID == id && m.AppID == this.AppID).FirstOrDefault();

                AboutModels newView = new AboutModels();
                newView = newView.UpdateModelToView(About);

                if (About == null)
                {
                    return HttpNotFound();
                }
                return View(newView);
            }

            var data = new AboutModels();
            data.AppID = this.AppID;
            data.ID = 0;
            return View(data);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(About model)
        {
            if (ModelState.IsValid)
            {
                #region xử lý upload image

                try
                {
                    if (Request.Files["img"] != null)
                    {
                        string fullname = Request.Files["img"].FileName;
                        if (fullname != "")
                        {
                            model.ImagePath = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                        }
                    }

                    #endregion xử lý upload image

                    if (model.ID > 0)
                    {
                        db.Entry(model).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Abouts.Add(model);
                    }

                    db.SaveChanges();
                    TempData["Status"] = "Cập nhật thành công";

                    return Redirect("/admin/About/Create?id=" + model.ID);
                }
                catch (System.Exception ex)
                {
                    TempData["Status"] = $"Cập nhật lỗi {ex.StackTrace}";
                }
            }
            return View(model);
        }

        #endregion Create

        public ActionResult Delete(int id = 0)
        {
            var model = db.Abouts.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        //
        // POST: /Admin/News/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var entitid = db.Abouts.Find(id);

            db.Abouts.Remove(entitid);
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