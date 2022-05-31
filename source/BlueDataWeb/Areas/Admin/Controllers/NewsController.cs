using AutoMapper;
using BlueDataWeb.Areas.Admin.Models;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Models.Enum;
using MVC_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminNews")]
    public class NewsController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        private string _keyName = NewsType.E_TinTuc.ToString();
        //
        // GET: /Admin/News/

        private NewsRepository repo = new NewsRepository();

        public ActionResult Index(long idCate = 0)
        {
            //var cate = db.NewCategories.Where(x => x.KeyName == _keyName && x.AppID == this.AppID).FirstOrDefault();

            //if (idCate > 0)
            //{
            //    cate = db.NewCategories.Where(x => x.NewCategoriesID == idCate).FirstOrDefault();
            //}

            //string cateIDstr = string.Join(",", NewCateHelper.GetListChild(cate.NewCategoriesID, true).Select(x => x.NewCategoriesID).ToList());

            //var lst = db.spNewsPaging(99999, 1, cateIDstr, "", this.AppID, _keyName).ToList();

            //ViewBag.lstCate = NewCateHelper.GetListChild(cate.NewCategoriesID, false);

            var lst = db.News.Where(x => x.AppID == this.AppID && x.IsDelete != true).ToList();

            return View(lst);
        }

        //

        public ActionResult Details(int id = 0)
        {
            return View(repo.GetById(id));
        }

        //
        // GET: /Admin/News/Create

        public ActionResult Create(int id = 0)
        {
            var cate = db.NewCategories.Where(x => x.KeyName == this._keyName && x.AppID == this.AppID).First();

            var lstCate = NewCateHelper.GetListChild(cate.NewCategoriesID, false);
            List<long> ListCateChild = NewCateHelper.GetListChild(cate.NewCategoriesID, true).Select(x => x.NewCategoriesID).ToList();
            ViewBag.NewCategoriesID = new SelectList(db.NewCategories.Where(x => ListCateChild.Contains(x.NewCategoriesID)), "NewCategoriesID", "Name");

            var model = new NewsModels();
            model.AppID = this.AppID;
            model.KeyName = _keyName.ToString();

            if (id > 0)
            {
                var data = db.News.Find(id);

                model = Mapper.Map<News, NewsModels>(data);
            }
           

            return View(model);
        }

        //
        // POST: /Admin/News/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(NewsModels model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    #region xử lý upload image

                    if (Request.Files["img"] != null)
                    {
                        model.ImagePath = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }

                    #endregion xử lý upload image

                    model.CreatedDate = DateTime.Now;

                    if (model.OrderBY == null)
                    {
                        model.OrderBY = 0;
                    }

                    var entity = Mapper.Map<NewsModels, News>(model);
                    if (model.NewsID > 0)
                    {
                        repo.Update(entity);

                    }
                    else
                    {
                        repo.Insert(entity);

                    }

                    TempData["Status"] = $"Cập nhật thành công";
                    return Redirect("/admin/News/Create?id=" + model.NewsID);
                }
                catch (Exception ex )
                {

                    TempData["Status"] = $"Cập nhật lỗi {ex.StackTrace}";

                }
               
            }

            return View(model);
        }

        //
    
         

        public ActionResult Delete(int id = 0)
        {
            News news = db.News.Find(id);
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
            News news = db.News.Find(id);

            news.IsDelete = true;
            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", new { @idCate = news.NewCategoriesID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}