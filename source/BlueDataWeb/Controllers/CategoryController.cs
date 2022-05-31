using BlueDataWeb.Models.Entites;
using BlueDataWeb.Utility;
using System.Collections.Generic;

// Insert new
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class CategoryController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();

        //
        // GET: /Category/

        public ActionResult Categorydetail(string title, int page = 0)
        {
            int countSkip = 0;
            if (page != 0)
            {
                countSkip = 8 * (page - 1);
            }

            var _getValueAllCatePro = db.Products.Include(p => p.Category).Where(m => m.CategoryID != null && (m.IsSelected == false || m.IsSelected == null) && m.Published == true);
            ViewBag.lstimg = db.Images.ToList();
            //
            var lstcategory = db.Categories.Where(m => m.IsDeleted == false || m.IsDeleted == null);
            Category categorylevel1 = null;
            foreach (var item in lstcategory)
            {
                string name = HelperSEO.ToSeoUrl(item.CategoryName);
                if (name == title)
                {
                    categorylevel1 = lstcategory.Where(m => m.CategoryID == item.CategoryID).FirstOrDefault();
                    break;
                }
            }

            if (categorylevel1 != null)
            {
                if (categorylevel1.ParentCategoryID == 0)
                {
                    List<Category> lsttemp = new List<Category>();
                    List<Category> lstcategorylevel2 = lstcategory.Where(m => m.ParentCategoryID == categorylevel1.CategoryID).ToList();

                    lsttemp.Add(categorylevel1);
                    foreach (Category cateLeve2ID in lstcategorylevel2)
                    {
                        lsttemp.Add(cateLeve2ID);
                        List<Category> lstcategorylevel3 = lstcategory.Where(m => m.ParentCategoryID == cateLeve2ID.CategoryID).ToList();
                        lsttemp.AddRange(lstcategorylevel3);
                    }

                    var lstproductID = _getValueAllCatePro.AsEnumerable().Where(m => lsttemp.Any(x => x.CategoryID == m.CategoryID)).OrderByDescending(m => m.DateCreated).Select(m => m.ProductID).ToList();

                    _getValueAllCatePro = _getValueAllCatePro.Where(m => lstproductID.Contains((int)m.ProductID));
                }
                else
                {
                    var lstCategorylv2 = lstcategory.Where(m => m.ParentCategoryID == categorylevel1.CategoryID);
                    //nêu cate levl2
                    if (lstCategorylv2.Count() > 0)
                    {
                        _getValueAllCatePro = _getValueAllCatePro.Where(m => lstCategorylv2.Any(x => x.CategoryID == m.CategoryID) || m.CategoryID == categorylevel1.CategoryID);
                    }
                    else
                    {
                        //cate level3
                        _getValueAllCatePro = _getValueAllCatePro.Where(m => m.CategoryID == categorylevel1.CategoryID);
                    }
                    ViewBag.CategoryName = lstCategorylv2;
                }

                //ViewBag.CategoryID = categorylevel1.CategoryID;
                ViewBag.Title = categorylevel1.MetaTitle;
                ViewBag.Keywords = categorylevel1.MetaKeywords;
                ViewBag.CategoryName = categorylevel1.CategoryName;
                ViewBag.Description = HttpUtility.HtmlDecode(categorylevel1.MetaDescription);
            }

            ViewBag.lstprovince = db.Provices.ToList();
            ViewBag.CountProduct = _getValueAllCatePro.Count();
            return View(_getValueAllCatePro.OrderByDescending(m => m.DateCreated).Skip(countSkip).Take(8).ToList());
        }

        public ActionResult Breadcrumb(int? id)
        {
            var lstcategory = db.Categories.Where(m => m.IsDeleted == false || m.IsDeleted == null);
            Category category = lstcategory.Where(m => m.CategoryID == id && m.ParentCategoryID == 0).FirstOrDefault();
            if (category != null)
                return PartialView(lstcategory.Where(m => m.CategoryID == id).ToList());
            else
            {
                List<Category> lsttemp = new List<Category>();
                Category cate1 = lstcategory.Where(m => m.CategoryID == id).FirstOrDefault();
                Category cate2 = lstcategory.Where(m => m.CategoryID == cate1.ParentCategoryID).FirstOrDefault();
                if (cate2 != null && cate2.ParentCategoryID == 0)
                {
                    lsttemp.Add(cate2);
                    lsttemp.Add(cate1);
                    return PartialView(lsttemp);
                }
                else
                {
                    Category cate3 = lstcategory.Where(m => m.CategoryID == cate2.ParentCategoryID).FirstOrDefault();
                    lsttemp.Add(cate3);
                    lsttemp.Add(cate2);
                    lsttemp.Add(cate1);
                    return PartialView(lsttemp);
                }
            }
        }

        public ActionResult ViewCategory()
        {
            var lstcategory = db.Categories.Where(m => m.IsDeleted == null || m.IsDeleted == false).OrderBy(m => m.DisplayOrder).ToList();
            return PartialView("_Menutop", lstcategory);
        }

        public ActionResult ViewSider()
        {
            var lstSlider = db.Images.Where(m => m.Type == "E_Slider" && m.IsShow == true).ToList();
            return PartialView("_Banner", lstSlider);
        }
        public ActionResult ViewSiderDichVu()
        {
            var lstSlider = db.Images.Where(m => m.Type == "E_Slider_DichVu" && m.IsShow == true).ToList();
            return PartialView("_SliderDichVu", lstSlider);
        }

        public ActionResult ViewPartners()
        {
            var lstSlider = db.Images.Where(m => m.Type == "E_Partners" && m.IsShow == true).ToList();
            return PartialView("_A_HinhAnh", lstSlider);
        }

        public ActionResult RelativeProduct(int CategoryID)
        {
            var lstProduct = db.Products.Where(m => m.CategoryID == CategoryID && (m.IsSelected == false || m.IsSelected == null)).Take(8).ToList();
            return PartialView(lstProduct);
        }

        public ActionResult NewsProduct()
        {
            var lstProduct = db.Products.Where(m => (m.IsSelected == false || m.IsSelected == null))
                .OrderByDescending(m => m.DateCreated).Take(3).ToList();
            return PartialView(lstProduct);
        }
    }
}