using AutoMapper;
using BlueDataWeb.Areas.Admin.Models;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Models.Enum;
using MVC_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminNews")]
    public class UngDungController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        private string _keyName = NewsType.E_UngDung.ToString();
        //
        // GET: /Admin/News/


        private NewsRepository repo = new NewsRepository();




        public ActionResult Index(long idCate = 0)
        {


            var cate = db.NewCategories.Where(x => x.KeyName == _keyName && x.AppID == this.AppID).FirstOrDefault();

            if (idCate > 0)
            {

                cate = db.NewCategories.Where(x => x.NewCategoriesID == idCate).FirstOrDefault();
            }

            string cateIDstr = string.Join(",", NewCateHelper.GetListChild(cate.NewCategoriesID, true).Select(x => x.NewCategoriesID).ToList());

            var lst = db.spNewsPaging(99999, 1, cateIDstr, "", this.AppID, _keyName).ToList();

            ViewBag.lstCate = NewCateHelper.GetListChild(cate.NewCategoriesID, false);

            return View(lst);
        }

        //

        public ActionResult Details(int id = 0)
        {
            return View(repo.GetById(id));
        }

        //
        // GET: /Admin/News/Create

        public ActionResult Create()
        {
            var cate = db.NewCategories.Where(x => x.KeyName == this._keyName && x.AppID == this.AppID).First();

            var lstCate = NewCateHelper.GetListChild(cate.NewCategoriesID, false);
            List<long> ListCateChild = NewCateHelper.GetListChild(cate.NewCategoriesID, true).Select(x => x.NewCategoriesID).ToList();
            ViewBag.NewCategoriesID = new SelectList(db.NewCategories.Where(x => ListCateChild.Contains(x.NewCategoriesID)), "NewCategoriesID", "Name");

            var data = new NewsModels();
            data.AppID = this.AppID;
            data.KeyName = _keyName.ToString();

            return View(data);
        }

        //
        // POST: /Admin/News/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(NewsModels model)
        {
            if (ModelState.IsValid)
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
                repo.Insert(entity);

                return RedirectToAction("Index", new { @idCate = model.NewCategoriesID });
            }

            return View(model);
        }

        //
        // GET: /Admin/News/Edit/5

        public ActionResult Edit(int id = 0)
        {
            News news = repo.GetById(id);

            var cate = db.NewCategories.Where(x => x.KeyName == this._keyName && x.AppID == this.AppID).First();
            var lstCate = NewCateHelper.GetListChild(cate.NewCategoriesID, false);
            List<long> ListCateChild = NewCateHelper.GetListChild(cate.NewCategoriesID, true).Select(x => x.NewCategoriesID).ToList();

            ViewBag.NewCategoriesID = new SelectList(db.NewCategories.Where(x => ListCateChild.Contains(x.NewCategoriesID)), "NewCategoriesID", "Name", news.NewCategoriesID);

            NewsModels newView = Mapper.Map<News, NewsModels>(news);

            if (newView == null)
            {
                return HttpNotFound();
            }
            return View(newView);
        }

        //
        // POST: /Admin/News/Edit/5

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        news.ImagePath = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image

                if (news.OrderBY == null)
                {
                    news.OrderBY = 0;
                }

                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { @idCate = news.NewCategoriesID });
            }
            return View(news);
        }

        //
        // GET: /Admin/News/Delete/5

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