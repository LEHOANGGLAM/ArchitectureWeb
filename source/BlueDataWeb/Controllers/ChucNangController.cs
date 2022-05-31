using BlueDataWeb.Models.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class ChucNangController : BaseController
    {
        //
        // GET: /SanPham/
        private BlueDataWebEntities db = new BlueDataWebEntities();
        public ActionResult Index()
        {

            NewCategory newcategory = db.NewCategories.AsNoTracking().Where(m => m.KeyName == "E_ChucNang" && m.AppID == this.AppID).FirstOrDefault();

            var lstnew = new List<News>();
            if(newcategory!= null)
            {
                ViewBag.newcategory = newcategory;
                  lstnew = db.News.AsNoTracking().Where(m => m.NewCategoriesID == newcategory.NewCategoriesID && m.AppID == this.AppID
                    && (m.IsDelete == null || m.IsDelete.Value == false)).OrderBy(m => m.OrderBY).ToList();
            }
            
            return View("SanPham_Index", lstnew);
        }

 

        public ActionResult Details(int id = 0)
        {
            News news = db.News.AsNoTracking().Where(x => x.AppID == this.AppID && x.NewsID == id).FirstOrDefault();
            NewCategory newcategory = db.NewCategories.AsNoTracking().Where(m => m.KeyName == "E_ChucNang" && m.AppID == this.AppID).FirstOrDefault();

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
