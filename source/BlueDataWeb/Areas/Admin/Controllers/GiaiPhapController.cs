using AutoMapper;
using BlueDataWeb.Helpers;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminNews")]
    public class GiaiPhapController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();

        //
        // GET: /Admin/News/

        public ActionResult Index()
        {
            var cate = db.NewCategories.Where(x => x.KeyName == "E_GiaiPhap" && x.AppID == this.AppID).FirstOrDefault();
            var data = new List<News>();
            if(cate !=  null)
            {
                data = db.News.Where(x => x.NewCategoriesID.Value == cate.NewCategoriesID
                  && (x.IsDelete == null || x.IsDelete.Value == false)).OrderBy(m => m.OrderBY).ToList();
            }
            
            return View(data);
        }

        public ActionResult Create()
        {
            var cate = db.NewCategories.Where(x => x.KeyName == "E_GiaiPhap" && x.AppID == this.AppID).FirstOrDefault();


            List<NewCategory> ListCateChild = new List<NewCategory>();

            ListCateChild.Add(cate);

            ViewBag.NewCategoriesID = new SelectList(ListCateChild, "NewCategoriesID", "Name");
            NewsModels newView = new NewsModels();
            newView.AppID = this.AppID;
            newView.IsDelete = false;
            return View(newView);
        }

        //
        // POST: /Admin/News/Create

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
            News news = db.News.Find(id);

            var cate = db.NewCategories.Where(x => x.KeyName == "E_GiaiPhap" && x.AppID == this.AppID).FirstOrDefault();

            List<NewCategory> ListCateChild = new List<NewCategory>();

            ListCateChild.Add(cate);

            ViewBag.NewCategoriesID = new SelectList(ListCateChild, "NewCategoriesID", "Name");

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
                return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}