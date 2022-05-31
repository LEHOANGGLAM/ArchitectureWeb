using BlueDataWeb.Areas.Admin.Models.Service;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Utility;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminService")]
    public class AboutCategoryController : Controller
    {
        private ImageService imgservice = new ImageService();
        private BlueDataWebEntities db = new BlueDataWebEntities();
        public string type = "E_Transport";

        //
        // GET: /Admin/AboutCategory/
        public void DropdownCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var category = db.Categories.Where(m => m.IsDeleted == false || m.IsDeleted == null);
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
                                Text = "---" + cc.CategoryName,
                                Value = cc.CategoryID.ToString()
                            });
                            foreach (var c3 in category)
                            {
                                if (c3.ParentCategoryID == cc.CategoryID)
                                {
                                    list.Add(new SelectListItem
                                    {
                                        Text = "------" + c3.CategoryName,
                                        Value = c3.CategoryID.ToString()
                                    });
                                }
                            }
                        }
                    }
                }
            }
            ViewBag.TinhTrang = list;
        }

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
            var abcategory = db.AboutCategories.OrderByDescending(a => a.AboutID).Skip(StartRow).Take(pi.PageSize);
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
            var abcategory = db.AboutCategories.OrderByDescending(a => a.AboutID).Skip(StartRow).Take(pi.PageSize);
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
            DropdownCategory();
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

                if (Request.Files["LargeImage"] != null)
                {
                    string fullname = Request.Files["LargeImage"].FileName;
                    if (fullname != "")
                    {
                        aboutcategory.LargeImage = UploadImageHelper.Upload(Request.Files["LargeImage"], Server.MapPath("~/Images/"), "News");
                    }
                }

                if (Request.Files["SmallImage"] != null)
                {
                    string fullname = Request.Files["SmallImage"].FileName;
                    if (fullname != "")
                    {
                        aboutcategory.SmallImage = UploadImageHelper.Upload(Request.Files["SmallImage"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image

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
            List<SelectListItem> list = new List<SelectListItem>();
            var category = db.Categories.Where(m => m.IsDeleted == false || m.IsDeleted == null);
            foreach (var c in category)
            {
                bool selected = false;
                if (c.ParentCategoryID == 0)
                {
                    if (c.CategoryID == aboutcategory.TinhTrang)
                    {
                        selected = true;
                    }
                    list.Add(new SelectListItem
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryID.ToString(),
                        Selected = selected
                    });

                    foreach (var cc in category)
                    {
                        if (cc.ParentCategoryID == c.CategoryID)
                        {
                            if (cc.CategoryID == aboutcategory.TinhTrang)
                            {
                                selected = true;
                            }
                            list.Add(new SelectListItem
                            {
                                Text = "---" + cc.CategoryName,
                                Value = cc.CategoryID.ToString(),
                                Selected = selected
                            });
                            selected = false;
                            foreach (var c3 in category)
                            {
                                if (c3.ParentCategoryID == cc.CategoryID)
                                {
                                    if (c3.CategoryID == aboutcategory.TinhTrang)
                                    {
                                        selected = true;
                                    }
                                    list.Add(new SelectListItem
                                    {
                                        Text = "------" + c3.CategoryName,
                                        Value = c3.CategoryID.ToString(),
                                        Selected = selected
                                    });
                                    selected = false;
                                }
                            }
                        }
                    }
                }
            }

            ViewBag.TinhTrang = list;
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

                if (Request.Files["LargeImage"] != null)
                {
                    string fullname = Request.Files["LargeImage"].FileName;
                    if (fullname != "")
                    {
                        aboutcategory.LargeImage = UploadImageHelper.Upload(Request.Files["LargeImage"], Server.MapPath("~/Images/"), "News");
                    }
                }

                if (Request.Files["SmallImage"] != null)
                {
                    string fullname = Request.Files["SmallImage"].FileName;
                    if (fullname != "")
                    {
                        aboutcategory.SmallImage = UploadImageHelper.Upload(Request.Files["SmallImage"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image

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