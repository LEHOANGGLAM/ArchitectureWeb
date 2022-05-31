using AutoMapper;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using MVC_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminNews")]
    public class ArchitectureNewsController : BaseController
    {
        private ArchitectureNewsRepository repo = new ArchitectureNewsRepository();
        private ArchitectureCategoriesRepository repoCate = new ArchitectureCategoriesRepository();

        //private BlueDataWebEntities db = new BlueDataWebEntities();


        public List<SelectListItem> GetCateCBBox()
        {
            var cate = repoCate.GetAll().Where(x=>x.IsDeleted != true);
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "-- Chọn danh mục cấp trên --",
                Value = "0"
            });

            foreach (var c in cate)
            {
                if (c.ParentCategoryID == 0)
                {
                    list.Add(new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.ID.ToString()
                    });
                    foreach (var cc in cate)
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

            
            return list;
        }


        
        public ActionResult Index(long idCate = 0)
        {
            ViewBag.idCate = idCate;
            var listCate = repoCate.GetAll().Where(x => x.ParentCategoryID == 0 && x.IsDeleted != true).ToList();
            ViewBag.lstCate = listCate;
            if (idCate > 0)
            {

                var lstChild = repoCate.GetAll().Where(x => x.ParentCategoryID == idCate && x.IsDeleted != true).Select(x => x.ID).ToList();
                lstChild.Add(idCate);

                 var data = repo.GetAll().Where(x => x.IsDelete == false && this.AppID == x.AppID && lstChild.Contains(x.CategoriesID.Value)).OrderByDescending(x => x.ID).ToList();
                return View(data);

            }
            var lst = repo.GetAll().Where(x=>x.IsDelete == false).OrderByDescending(x=>x.ID).ToList();
            return View(lst);
        }

        // GET: /Admin/News/Create

        public ActionResult Create(int id = 0)
        {
            var menu =  GetCateCBBox();

            if (id > 0)
            {
                ArchitectureNew news = repo.GetById(id);
                ArchitectureNewsDto newView = Mapper.Map<ArchitectureNew, ArchitectureNewsDto>(news);

                if (newView == null)
                {
                    return HttpNotFound();
                }
                newView.Catelst = menu;
                return View(newView);
            }

            var data = new ArchitectureNewsDto();
            data.Catelst = menu;
            data.AppID = this.AppID;
            data.IsDelete = false;
            return View(data);
        }

        //
        // POST: /Admin/News/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ArchitectureNewsDto model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    #region xử lý upload image

                    if (Request.Files["img"] != null && !string.IsNullOrEmpty(Request.Files["img"].FileName))
                    {
                        model.ImagePath = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }

                    #endregion xử lý upload image

                    model.CreatedDate = DateTime.Now;

                    if (model.OrderBY == null)
                    {
                        model.OrderBY = 0;
                    }

                    var entity = Mapper.Map<ArchitectureNewsDto, ArchitectureNew>(model);

                    if (model.ID > 0)
                    {
                        repo.Update(entity);
                    }
                    else
                    {
                        repo.Insert(entity);
                    }

                    TempData["Status"] = $"Cập nhật thành công";
                    return Redirect("/admin/ArchitectureNews/Create?id=" + entity.ID);
                }
                catch (Exception ex)
                {

                    TempData["Status"] = $"Cập nhật lỗi {ex.StackTrace}";

                }

            }

            return View(model);
        }

        //
        // GET: /Admin/News/Edit/5

        public ActionResult Delete(int id = 0)
        {
            var news = repo.GetById(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /Admin/News/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var news = repo.GetById(id);

            news.IsDelete = true;
            repo.Update(news);

            return RedirectToAction("Index");
        }
    }
}