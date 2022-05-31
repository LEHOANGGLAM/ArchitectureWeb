using BlueDataWeb.Areas.Admin.Models.Service;
using BlueDataWeb.Models.Entites;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminCategory")]
    public class NewsCategoryController : BaseController
    {
        private ImageService imgservice = new ImageService();

        private BlueDataWebEntities db = new BlueDataWebEntities();

        public ActionResult Index()
        {
            var category = db.NewCategories.OrderBy(a => a.DisplayOrder).Where(c => c.AppID == this.AppID && (c.IsDeleted == false || c.IsDeleted == null));
            return View(category);
        }

        

        public ActionResult Create(int id = 0)
        {
            //ViewBag.ParentCategoryID = new SelectList(db.Categories.Where(c => c.ParentCategoryID == 0), "CategoryId", "CategoryName");
            List<SelectListItem> list = new List<SelectListItem>();
            List<NewCategory> category = db.NewCategories.Where(c => c.AppID == this.AppID && (c.IsDeleted == false || c.IsDeleted == null)).OrderBy(m => m.DisplayOrder).ToList();
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
                        Value = c.NewCategoriesID.ToString()
                    });
                    foreach (var cc in category)
                    {
                        if (cc.ParentCategoryID == c.NewCategoriesID)
                        {
                            list.Add(new SelectListItem
                            {
                                Text = "- -" + cc.Name,
                                Value = cc.NewCategoriesID.ToString()
                            });
                        }
                    }
                }
            }
            ViewBag.ParentCategoryID = list;
            NewCategory data = new NewCategory();
            data.AppID = this.AppID;
            if (id > 0)
            {
                data = db.NewCategories.Find(id);
            }

            return View(data);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(NewCategory model)
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

                    if (model.NewCategoriesID  == 0)
                    {
                        db.NewCategories.Add(model);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    TempData["Status"] = $"Cập nhật thành công";
                    return Redirect("/admin/NewsCategory/Create?id=" + model.NewCategoriesID);
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
            NewCategory category = db.NewCategories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {
                var categoryChild = db.NewCategories.Where(x => x.ParentCategoryID == category.NewCategoriesID).FirstOrDefault();
                if (categoryChild != null)
                {
                    TempData["Status"] = $"Danh mục đang sử dụng - là danh mục cha của danh mục khác";
                    return Redirect("/admin/NewsCategory/NoDel");
                }

                var baiviet = db.ArchitectureNews.Where(x => x.CategoriesID == category.NewCategoriesID && x.IsDelete != true).FirstOrDefault();
                if (categoryChild != null)
                {
                    TempData["Status"] = $"Danh mục đang sử dụng - là danh mục của bài viết";

                    return Redirect("/admin/NewsCategory/NoDel");
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
             NewCategory category = db.NewCategories.Find(id);
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