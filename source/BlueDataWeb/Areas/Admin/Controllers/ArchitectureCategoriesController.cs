using BlueDataWeb.Areas.Admin.Models.Service;
using BlueDataWeb.Models.Entites;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminCategory")]
    public class ArchitectureCategoriesController : BaseController
    {
        private ImageService imgservice = new ImageService();

        private BlueDataWebEntities db = new BlueDataWebEntities();

        public ActionResult Index()
        {
            var category = db.ArchitectureCategories.OrderBy(a => a.DisplayOrder).Where(c => c.AppID == this.AppID && (c.IsDeleted != true));
            return View(category);
        }

 
        public ActionResult Create(int id = 0)
        {
            //ViewBag.ParentCategoryID = new SelectList(db.Categories.Where(c => c.ParentCategoryID == 0), "CategoryId", "CategoryName");
            List<SelectListItem> list = new List<SelectListItem>();
            List<ArchitectureCategory> category = db.ArchitectureCategories.Where(c => c.AppID == this.AppID && (c.IsDeleted == false || c.IsDeleted == null)).OrderBy(m => m.DisplayOrder).ToList();
            list.Add(new SelectListItem
            {
                Text = "-- Chọn danh mục cấp trên --",
                Value = "0"
            });

            foreach (var c in category)
            {
                if (c.ParentCategoryID == 0)
                {
                    list.Add(new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.ID.ToString()
                    });
                    foreach (var cc in category)
                    {
                        if (cc.ParentCategoryID == c.ID)
                        {
                            list.Add(new SelectListItem
                            {
                                Text = "- -" + cc.Name,
                                Value = cc.ID.ToString()
                            });
                        }
                    }
                }
            }
            ViewBag.ParentCategoryID = list;
            ArchitectureCategory data = new ArchitectureCategory();
            data.AppID = this.AppID;
            data.IsDeleted = false;
            if (id > 0)
            {
                data = db.ArchitectureCategories.Find(id);
            }

            return View(data);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ArchitectureCategory model)
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
                            model.Image = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                        }
                    }

                    #endregion xử lý upload image

                    model.IsDeleted = false;

                    if (model.ID == 0)
                    {
                        db.ArchitectureCategories.Add(model);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    TempData["Status"] = $"Cập nhật thành công";
                    return Redirect("/admin/ArchitectureCategories/Create?id=" + model.ID);
                }
                catch (System.Exception ex)
                {
                    TempData["Status"] = $"Cập nhật lỗi {ex.StackTrace}";
                }
            }
            else
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                var error = messages;
            }

            return View(model);
        }

        public ActionResult Delete(int id = 0)
        {
            ArchitectureCategory category = db.ArchitectureCategories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {
                var categoryChild = db.ArchitectureCategories.Where(x => x.ParentCategoryID == category.ID).FirstOrDefault();
                if (categoryChild != null)
                {
                    TempData["Status"] = $"Danh mục đang sử dụng - là danh mục cha của danh mục khác";
                    return Redirect("/admin/ArchitectureCategories/NoDel");
                }

                var baiviet = db.ArchitectureNews.Where(x => x.CategoriesID == category.ID && x.IsDelete != true).FirstOrDefault();
                if (categoryChild != null)
                {
                    TempData["Status"] = $"Danh mục đang sử dụng - là danh mục của bài viết";

                    return Redirect("/admin/ArchitectureCategories/NoDel");
                }
            }
            return View(category);
        }

        public ActionResult NoDel()
        {
            return View();
        }

        //
        // POST: /Admin/Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ArchitectureCategory category = db.ArchitectureCategories.Find(id);
            //db.Categories.Remove(category);
            category.IsDeleted = true;
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