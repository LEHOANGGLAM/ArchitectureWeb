using BlueDataWeb.Models.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class TemNhanController : BaseController
    {
        //
        // GET: /SanPham/
        private BlueDataWebEntities db = new BlueDataWebEntities();
        public ActionResult Index()
        {

 

            NewCategory newcategory = db.NewCategories.Where(m => m.KeyName == "E_TemNhan" && m.AppID == this.AppID).FirstOrDefault();
            ViewBag.newcategory = newcategory;
            var lstnew = db.News.Where(m => m.AppID == this.AppID && m.KeyName == "E_TemNhan" && (m.IsDelete == null || m.IsDelete.Value == false)).OrderByDescending(m => m.NewsID).ToList();


            return View(lstnew);
        }

 

        public ActionResult Details(int id = 0)
        {
            News news = db.News.AsNoTracking().Where(x => x.AppID == this.AppID && x.NewsID == id).FirstOrDefault();
            NewCategory newcategory = db.NewCategories.AsNoTracking().Where(m => m.KeyName == "E_TemNhan" && m.AppID == this.AppID).FirstOrDefault();

            var lstAllSanPham = db.News.AsNoTracking().Where(x => x.NewCategoriesID == newcategory.NewCategoriesID && x.AppID == this.AppID &&
                (x.IsDelete == null || x.IsDelete.Value == false)).OrderBy(x => x.NewsID).ToList();
            ViewBag.lstAllSanPham = lstAllSanPham;
          
            return View(news);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
