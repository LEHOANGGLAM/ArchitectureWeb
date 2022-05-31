using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Utility;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using BlueDataWeb.Areas.Admin.Models.Service;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminCategory")]
    public class CategoryController : Controller
    {
        ImageService imgservice = new ImageService();

        private BlueDataWebEntities db = new BlueDataWebEntities();
     

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
            PagerInfo pi = PagerInfo.Get("Category", 50);
            pi.RowCount = db.Categories.OrderByDescending(p => p.CategoryName).Where(c => c.IsDeleted == false || c.IsDeleted == null).Count();
            pi.Navigate(pageNo);
            int StartRow = pi.PageNo * pi.PageSize;
            var category = db.Categories.OrderByDescending(a => a.CategoryName).Where(c => c.IsDeleted == false || c.IsDeleted == null).Skip(StartRow).Take(pi.PageSize);
            return View(category);
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
            PagerInfo pi = PagerInfo.Get("Category", 50);
            pi.RowCount = db.Categories.OrderByDescending(p => p.CategoryName).Where(c => c.IsDeleted == false || c.IsDeleted==null).Count();
            pi.Navigate(pageNo);
            int StartRow = pi.PageNo * pi.PageSize;
            var category = db.Categories.OrderByDescending(a => a.CategoryName).Where(c => c.IsDeleted == false || c.IsDeleted == null).Skip(StartRow).Take(pi.PageSize);
            return PartialView("IndexAjax",category);
        }
        //
        // GET: /Admin/Category/Details/5

        public ActionResult Details(int id = 0)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Admin/Category/Create

        public ActionResult Create()
        {
            //ViewBag.ParentCategoryID = new SelectList(db.Categories.Where(c => c.ParentCategoryID == 0), "CategoryId", "CategoryName");
            List<SelectListItem> list = new List<SelectListItem>();
            List<Category> category = db.Categories.Where(m=>m.IsDeleted==false || m.IsDeleted ==null).OrderByDescending(m=>m.CategoryName).ToList();
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
                        Text = c.CategoryName,
                        Value = c.CategoryID.ToString()
                    });
                    foreach (var cc in category)
                    {
                        if (cc.ParentCategoryID == c.CategoryID)
                        {
                            list.Add(new SelectListItem
                            {
                                Text = "- -" + cc.CategoryName,
                                Value = cc.CategoryID.ToString()
                            });
                        }
                    }
                }
            }
            ViewBag.ParentCategoryID = list;
            return View();
        }

        //
        // POST: /Admin/Category/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {


                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        category.Image = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image


                 

                category.IsDeleted = false;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        //
        // GET: /Admin/Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ParentCategoryID = new SelectList(db.Categories.Where(c => c.ParentCategoryID == 0), "CategoryId", "CategoryName", category.ParentCategoryID);
            List<SelectListItem> list = new List<SelectListItem>();
            List<Category> lstcategory = db.Categories.Where(m => m.IsDeleted == false || m.IsDeleted == null).OrderByDescending(m=>m.CategoryName).ToList();
            list.Add(new SelectListItem
            {
                Text = "-- Chọn danh mục cấp trên --",
                Value = "0"
            });
            foreach (var c in lstcategory)
            {
                if (c.ParentCategoryID == 0)
                {
                    if (c.CategoryID == category.ParentCategoryID)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = c.CategoryName,
                            Value = c.CategoryID.ToString(),
                            Selected = true
                        });
                    }
                    else
                    {

                        list.Add(new SelectListItem
                        {
                            Text = c.CategoryName,
                            Value = c.CategoryID.ToString()
                        });
                    }
                    foreach (var cc in lstcategory)
                    {
                        if (cc.ParentCategoryID == c.CategoryID)
                        {
                            if (cc.CategoryID == category.ParentCategoryID)
                            {
                                list.Add(new SelectListItem
                                {
                                    Text = "- -" + cc.CategoryName,
                                    Value = cc.CategoryID.ToString(),
                                    Selected = true
                                });
                            }
                            else
                            {
                                list.Add(new SelectListItem
                                {
                                    Text = "- -" + cc.CategoryName,
                                    Value = cc.CategoryID.ToString()
                                });
                            }
                        }
                    }
                }
            }
            ViewBag.ParentCategoryID = list;
            return View(category);
        }

        //
        // POST: /Admin/Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
              
                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        category.Image = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image

                 

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Admin/Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentCategoryID = new SelectList(db.Categories.Where(c => c.ParentCategoryID == 0), "CategoryId", "CategoryName", category.ParentCategoryID);
            return View(category);
        }

        //
        // POST: /Admin/Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
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