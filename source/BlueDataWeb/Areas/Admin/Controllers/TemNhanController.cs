using AutoMapper;
using BlueDataWeb.Areas.Admin.Models;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Models.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminNews")]
    public class TemNhanController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        private string _keyName = NewsType.E_TemNhan.ToString();

        public ActionResult Index(long idCate = 0)
        {

            var cate = db.NewCategories.Where(x => x.KeyName == _keyName && x.AppID == this.AppID).FirstOrDefault();

            if (idCate > 0)
            {

                cate = db.NewCategories.Where(x => x.NewCategoriesID == idCate).FirstOrDefault();
            }

            string cateIDstr = string.Join(",", NewCateHelper.GetListChild(cate.NewCategoriesID, true).Select(x => x.NewCategoriesID).ToList());

            var lst = db.spNewsPaging(99999, 1, cateIDstr, "", this.AppID, _keyName).ToList();
            return View(lst);
        }

        public ActionResult Create()
        {
            var cate = db.NewCategories.Where(x => x.KeyName == _keyName && x.AppID == this.AppID).First();

            var lstCate = NewCateHelper.GetListChild(cate.NewCategoriesID, false);
            List<long> ListCateChild = lstCate.Select(x => x.NewCategoriesID).ToList();
            ViewBag.NewCategoriesID = new SelectList(db.NewCategories.Where(x => ListCateChild.Contains(x.NewCategoriesID)), "NewCategoriesID", "Name");

            var data = new NewsModels(_keyName, this.AppID, "");
            return View(data);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(NewsModels model)
        {
            if (ModelState.IsValid)
            {
                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        model.ImagePath = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image

                model.CreatedDate = DateTime.Now;
                News entity = Mapper.Map<NewsModels, News>(model);

                db.News.Add(entity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).ToList();
            model.BinCommbox(_keyName, this.AppID, model.NewCategoriesID.ToString());
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            NewsModels model = new NewsModels();
            try
            {
                News news = db.News.Find(id);
                model = Mapper.Map<News, NewsModels>(news);
                model.BinCommbox(_keyName, this.AppID, news.NewCategoriesID.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(NewsModels model)
        {
            if (ModelState.IsValid)
            {
                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        model.ImagePath = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image

                if (model.OrderBY == null)
                {
                    model.OrderBY = 0;
                }

                News entity = Mapper.Map<NewsModels, News>(model);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).ToList();
            model.BinCommbox(_keyName, this.AppID, model.NewCategoriesID.ToString());
            return View(model);
        }

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

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}