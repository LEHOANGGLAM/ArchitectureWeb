using BlueDataWeb.Areas.Admin.Models;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

// Insert new
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class ArchitectureController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        //
        // GET: /TinTuc/

        public ArchitectureController()
        {
        }

        public ActionResult ArchitectureMenu()
        {
            var lstCate = db.ArchitectureCategories.AsNoTracking().Where(x => x.ParentCategoryID == 0).ToList();
            return PartialView("_ArchitectureMenu", lstCate);
        }

        public ActionResult List(int id)
        {
            ArchitectureNewsView dataView = new ArchitectureNewsView();

            //get list category con
            List<ArchitectureCategory> cateChild = db.ArchitectureCategories.AsNoTracking().Where(x => x.ParentCategoryID == id && x.IsDeleted !=true).ToList();
            dataView.cateChild = cateChild;

            var cate = db.ArchitectureCategories.AsNoTracking().Where(x => x.ID == id).First();

            if (cate.ParentCategoryID > 0)
            {
                dataView.cateParent = db.ArchitectureCategories.AsNoTracking().Where(x => x.ID == cate.ParentCategoryID).First();
                dataView.cateChild = db.ArchitectureCategories.AsNoTracking().Where(x => x.ParentCategoryID == dataView.cateParent.ID && x.IsDeleted != true).ToList();
            }
            else
            {
                dataView.cateParent = null;
                dataView.cateChild = cateChild;
            }

            var lstcateAll = new List<long>();
            lstcateAll.Add(cate.ID);
            lstcateAll.AddRange(cateChild.Select(x => x.ID).ToList());

            var lstnews = db.ArchitectureNews.Include(x => x.ArchitectureCategory).Where(x => lstcateAll.Contains(x.CategoriesID.Value) && x.AppID == this.AppID).OrderByDescending(x => x.ID).ToList();

            dataView.data = lstnews;
            dataView.cate = cate;
          
            return View(dataView);
        }

        public ActionResult ListViewByKeyName(string keyname, string view, int page = 0)
        {
            ArchitectureCategory cate = db.ArchitectureCategories.Where(m => m.KeyName == keyname && m.IsDeleted != true).FirstOrDefault();

            List<ArchitectureCategory> cateChild = db.ArchitectureCategories.AsNoTracking().Where(x => x.ParentCategoryID == cate.ID && x.IsDeleted != true).ToList();

            ViewBag.cate = cate;
            ViewBag.cateChild = cateChild;
            var lstcateAll = new List<long>();
            lstcateAll.Add(cate.ID);
            lstcateAll.AddRange(cateChild.Select(x => x.ID).ToList());

            var lstnews = db.ArchitectureNews.Include(x => x.ArchitectureCategory).Where(x => lstcateAll.Contains(x.CategoriesID.Value) && x.AppID == this.AppID && x.IsDelete != true).OrderByDescending(x => x.ID).ToList();

            if (page > 0)
            {
                return PartialView(view, lstnews.Take(page));

            }
            else
            {
                return PartialView(view, lstnews );
            }
        }

        public ActionResult Detail(long id)
        {
            //get list category con

            var model = db.ArchitectureNews.Include(x => x.ArchitectureCategory).Where(x => x.ID == id && x.AppID.Value == this.AppID).FirstOrDefault();
            return View(model);
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