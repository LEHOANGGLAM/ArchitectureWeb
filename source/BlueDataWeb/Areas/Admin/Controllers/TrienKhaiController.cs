using AutoMapper;
using BlueDataWeb.Helpers;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Models.Enum;
using BlueDataWeb.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminNews")]
    public class TrienKhaiController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();

        private string _keyName = NewsType.E_TrienKhai.ToString();


        public ActionResult Index(string idCate)
        {
             
            var cate = db.NewCategories.Where(x => x.KeyName == _keyName && x.AppID == this.AppID).First();
            var data = db.News.Where(x => x.NewCategoriesID.Value == cate.NewCategoriesID
                    && (x.IsDelete == null || x.IsDelete.Value == false)).OrderBy(m => m.OrderBY).ToList();
             
            return View(data);
        }

        

        public ActionResult Create()
        {
            var cate = db.NewCategories.Where(x => x.KeyName ==_keyName && x.AppID == this.AppID).First();
            List<NewCategory> ListCateChild = new List<NewCategory>();
            ListCateChild.Add(cate);
            ViewBag.NewCategoriesID = new SelectList(ListCateChild, "NewCategoriesID", "Name");

            var data = new NewsModels(_keyName, this.AppID, "");
            
      
            return View(data);

           
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(News news)
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


                news.CreatedDate = DateTime.Now;

                if (news.OrderBY == null)
                {
                    news.OrderBY = 0;
                }

                news.CreatedDate = DateTime.Now;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        //
        // GET: /Admin/News/Edit/5

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

        //
        // POST: /Admin/News/Edit/5

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

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}