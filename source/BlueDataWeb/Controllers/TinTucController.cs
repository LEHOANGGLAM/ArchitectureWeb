using BlueDataWeb.Areas.Admin.Models;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class TinTucController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        //
        // GET: /TinTuc/

        public TinTucController()
        {
            var cate = db.NewCategories.Where(x => x.KeyName == "E_TinTuc" && x.AppID == this.AppID).FirstOrDefault();
            if (cate != null)

            {
                ViewBag.TinTucController_Menu = NewCateHelper.GetListChild(cate.NewCategoriesID, true);
            }
        }

        public ActionResult NewHome(string keyname, string view, int page = 5)
        {
            NewCategory cate = db.NewCategories.Where(m => m.KeyName == keyname).FirstOrDefault();
            return PartialView(view, db.News.Where(m => m.NewCategoriesID == cate.NewCategoriesID && (m.IsDelete == null || m.IsDelete.Value == false)).OrderBy(x => x.OrderBY).Take(page));
        }

        public ActionResult Details(int id = 0)
        {
            News news = db.News.Find(id);
            var listNew = db.News.Where(x => x.NewsID != id && x.NewCategoriesID == news.NewCategoriesID && (x.IsDelete == null || x.IsDelete.Value == false)).OrderByDescending(x => x.NewsID).Take(8).ToList();

            ViewBag.TopNewRelate = listNew;

            List<NewCategory> lstcate = db.NewCategories.Where(m => m.IsShow == true).ToList();
            ViewBag.lstCate = lstcate;

            // tăng số lượt xem của tin tức
            news.Views = (news.Views == null ? 0 : news.Views) + 1; 

            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = news.Title;
            return View(news);
        }

        public ActionResult TinTucSub(int id)
        {
            int cateid = id;
            int BlockSize = ConfigurationManager.AppSettings["BlockSize_Blog"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["BlockSize_Blog"].ToString()) : 10;
            string lstCate = "";
            if (cateid != 0)
            {
                var cate = db.NewCategories.Where(x =>  x.AppID == this.AppID && x.NewCategoriesID == cateid).FirstOrDefault();
                if (cate != null)
                {
                    lstCate = string.Join(",", NewCateHelper.GetListChild(cate.NewCategoriesID, true).Select(x => x.NewCategoriesID).ToList());
                }

                ViewBag.CateName = cate.Name;
                ViewBag.CateDES = cate.DescriptionContent;

                
            }
          


            ViewBag.lstCate = lstCate;

            NewCategory newcategory = db.NewCategories.Where(m => m.KeyName == "E_TinTuc" && m.AppID == this.AppID).FirstOrDefault();
            ViewBag.newcategory = newcategory;

            var lst = db.spNewsPaging(BlockSize, 1, lstCate, "", this.AppID, "").ToList();

            ViewBag.NoMoreData = lst.Count < BlockSize;
            return View( lst);
        }

        public ActionResult Index(int cateid = 0)
        {
            int BlockSize = ConfigurationManager.AppSettings["BlockSize_Blog"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["BlockSize_Blog"].ToString()) : 10;
            string lstCate = "";
            if (cateid != 0)
            {
                var cate = db.NewCategories.Where(x => x.KeyName == "E_TinTuc" && x.AppID == this.AppID && x.NewCategoriesID == cateid).FirstOrDefault();
                if (cate != null)
                {
                    lstCate = string.Join(",", NewCateHelper.GetListChild(cate.NewCategoriesID, true).Select(x => x.NewCategoriesID).ToList());
                }
            }
            else
            {

            }


            ViewBag.lstCate = lstCate;

            NewCategory newcategory = db.NewCategories.Where(m => m.KeyName == "E_TinTuc" && m.AppID == this.AppID).FirstOrDefault();
            ViewBag.newcategory = newcategory;

            var lst = db.spNewsPaging(BlockSize, 1, lstCate, "", this.AppID, "").ToList();

            ViewBag.NoMoreData = lst.Count < BlockSize;
            return View("TinTuc_Index", lst);
        }

        [HttpPost]
        public ActionResult InfinateScroll(int BlockNumber, string CategoryId)
        {
            ////////////////// THis line of code only for demo. Needs to be removed ///////////////
            System.Threading.Thread.Sleep(500);
            //////////////////////////////////////////////////////////////////////////////////////////
            int BlockSize = ConfigurationManager.AppSettings["BlockSize_Blog"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["BlockSize_Blog"].ToString()) : 10;
            var lst = db.spNewsPaging(BlockSize, BlockNumber, CategoryId, "", this.AppID, "").ToList();


            JsonModel jsonModel = new JsonModel();
            jsonModel.NoMoreData = 0;

            if (lst.Count < BlockSize)
            {
                jsonModel.NoMoreData = 1;
            }

            jsonModel.HTMLString = RenderPartialViewToString("TinTucList", lst);

            return Json(jsonModel);
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult NewHomeSideBar(string view, int page = 10)
        {
            var cate = db.NewCategories.Where(x => x.KeyName == "E_TinTuc").First();
            var lstCate = NewCateHelper.GetListChild(cate.NewCategoriesID);
            List<long> ListCateChild = lstCate.Select(x => x.NewCategoriesID).ToList();

            var listNew = db.News.Where(x => ListCateChild.Contains(x.NewCategoriesID.Value) && x.IsNoiBat == true && (x.IsDelete == null || x.IsDelete.Value == false)).OrderBy(x => x.OrderBY).Take(page).ToList();

            return PartialView(view, listNew);
        }
    }
}