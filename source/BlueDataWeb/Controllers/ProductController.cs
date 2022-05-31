using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.ViewModels;
 
using BlueDataWeb.Models.CustomsModel;

namespace BlueDataWeb.Controllers
{
    public class ProductController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        private UpdateDate updatedate = new UpdateDate();

        public ActionResult Search(string txtsearch)
        {
            var lstpro = db.Products.Where(m=>m.IsSelected==false || m.IsSelected==null).ToList();
            List<Product> LstProduct = new List<Product>();

            if (txtsearch != null)
            {
                string a = txtsearch;
                string s = convertToUnSign3(a).ToUpper();

                string[] arrayStr = s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                ViewBag.s = a;
                for (int i = 0; i < arrayStr.Count(); i++)
                {
                    lstpro = lstpro.FindAll(
                delegate(Product math)
                {
                    if (convertToUnSign3(math.ProductName).ToUpper().Contains(arrayStr[i]))
                        return true;
                    else
                        return false;
                }
                     );
                }

                ViewBag.CountProduct = lstpro.Count();
                ViewBag.CategoryName = "Kết quả tìm kiếm";

                return View("~/Views/Category/Categorydetail.cshtml", lstpro);
                
            }
            else
            {
                return View("~/Views/Category/Categorydetail.cshtml", null );
            }


        }
        //
        // GET: /Product/
        //chuyển sang tiếng việt không dấu
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public ActionResult Index(int page = 0)
        {
            int countSkip = 0;
            if (page != 0)
            {
                countSkip = 9 * (page - 1);
            }

            var lstProduct = db.Products.Where(m => m.CategoryID != null && (m.IsSelected == null || m.IsSelected == false) && m.Published == true).ToList();
            int CountProduct = lstProduct.Count();
            lstProduct = lstProduct.OrderByDescending(m => m.DateCreated).Skip(countSkip).Take(9).ToList();

            var lstimg = db.Images.Where(m => m.IsShow == true && m.Type == "E_Slider").ToList();
            var lstCategory = db.Categories.Where(m => m.IsDeleted == false || m.IsDeleted == null).ToList();
            var viewmodel = new ProductCateViewModel()
            {
                Products = lstProduct,
                Categorys = lstCategory,
                Images = lstimg,
                CountProduct = CountProduct
            };

            ViewBag.lstprovince = db.Provices.ToList();
            return PartialView(viewmodel);
        }

        public ActionResult IndexBackup()
        {
            var _getValueAllCatePro = db.Products.Include(p => p.Category).Where(m => m.CategoryID != null && (m.IsSelected == false || m.IsSelected == null));
            ViewBag.lstDealnew = _getValueAllCatePro.OrderByDescending(m => m.DateCreated).Take(3).ToList();
            ViewBag.lstimg = db.Images.ToList();
            //
            var lstcategory = db.Categories;
            var lstcategorylevel1 = lstcategory.Where(m => m.ParentCategoryID == 0).ToList();
            //if (lstcategorylevel1 != null)
            //{
            //    List<int> lstSliderProductID = new List<int>();
            //    foreach (Category cateLevel1ID in lstcategorylevel1)
            //    {
            //        List<Category> lsttemp = new List<Category>();
            //        //lam gi? lấy cái list category con cate leve1
            //        List<Category> lstcategorylevel2 = lstcategory.Where(m => m.ParentCategoryID == cateLevel1ID.CategoryID).ToList();

            //        lsttemp.Add(cateLevel1ID);
            //        //comment nha em
            //        // List<Int32> lstcategoryleve3 = new List<Int32>();
            //        foreach (Category cateLeve2ID in lstcategorylevel2)
            //        {
            //            lsttemp.Add(cateLeve2ID);
            //            List<Category> lstcategorylevel3 = lstcategory.Where(m => m.ParentCategoryID == cateLeve2ID.CategoryID).ToList();
            //            lsttemp.AddRange(lstcategorylevel3);
            //        }

            //        int productID = _getValueAllCatePro.AsEnumerable().Where(m => lsttemp.Any(x => x.CategoryID == m.CategoryID)).OrderByDescending(m => m.DateCreated).Select(m => m.ProductID).FirstOrDefault();
            //        lstSliderProductID.Add(productID);

            //    }
            //    // lam gi? lấy các sản phẩm của category con
            //    _getValueAllCatePro = _getValueAllCatePro.Where(m => lstSliderProductID.Contains((int)m.ProductID));

            //}

            return PartialView(_getValueAllCatePro.Where(m => m.IsSlider == true).ToList());
        }

        public ActionResult ProductCategory()
        {
            var _getValueAllCatePro = db.Products.Include(p => p.Category).Where(m => m.CategoryID != null && (m.IsSelected == null || m.IsSelected == false));
            ViewBag.lstimg = db.Images.ToList();
            ViewBag.lstCategory = db.Categories.ToList();

            List<Product> lstProductExpire = _getValueAllCatePro.Where(m => m.EndDate <= DateTime.Today).ToList();
            updatedate.Updateday(lstProductExpire, db);

            return PartialView(_getValueAllCatePro.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            ViewBag.lstcomment = db.Comments.Where(m => m.ProductID == id).Where(m => m.Status == true).Include(m => m.User).ToList();
            ViewBag.lstimg = db.Images.Where(m => (m.IsSlider == false || m.IsSlider == null) && m.DisplayOrder != null && m.ProductId == product.ProductID).OrderBy(m => m.DisplayOrder).ToList();
            if (product == null)
            {
                return HttpNotFound();
            }
            int count = db.Comments.Where(m => m.ProductID == product.ProductID).Count();

            ViewBag.lstcolor = db.Colors.Where(m => m.ProductId == product.ProductID).ToList();
            ViewBag.ProductID = id;
            return View(product);
        }

        public ActionResult TopDeal(int? id)
        {
            Product product = db.Products.Where(m => m.ProductID == id).FirstOrDefault();
            var lstproduct = db.Products.Where(m => (m.IsSelected == null || m.IsSelected == false)
                            && m.ProductID != id && m.CategoryID == product.CategoryID).OrderByDescending(m => m.DateCreated).Take(12).ToList();
            return PartialView(lstproduct);
        }

        public ActionResult DealNew()
        {
            ViewBag.lstimg = db.Images.ToList();
            var lstproduct = db.Products.Where(m => (m.IsSelected == null
                            || m.IsSelected == false)).Where(m => m.DateCreated >= DateTime.Today).OrderByDescending(m => m.DateCreated).ToList();
            return PartialView(lstproduct);
        }


        public ActionResult Breadcrumb(int? id)
        {
            Product product = db.Products.Where(m => m.ProductID == id).FirstOrDefault();
            if (product != null)
            {
                var lstcategory = db.Categories;
                Category category = lstcategory.Where(m => m.CategoryID == product.CategoryID && m.ParentCategoryID == 0).FirstOrDefault();

                if (category != null)
                {
                    ViewBag.ProductName = product.ProductName;
                    return PartialView(lstcategory.Where(m => m.CategoryID == product.CategoryID).ToList());
                }
                else
                {
                    List<Category> lsttemp = new List<Category>();
                    Category cate1 = lstcategory.Where(m => m.CategoryID == product.CategoryID).FirstOrDefault();
                    Category cate2 = lstcategory.Where(m => m.CategoryID == cate1.ParentCategoryID).FirstOrDefault();
                    if (cate2 != null && cate2.ParentCategoryID == 0)
                    {
                        lsttemp.Add(cate2);
                        lsttemp.Add(cate1);
                        ViewBag.ProductName = product.ProductName;
                        return PartialView(lsttemp);
                    }
                    else
                    {
                        Category cate3 = lstcategory.Where(m => m.CategoryID == cate2.ParentCategoryID).FirstOrDefault();
                        lsttemp.Add(cate3);
                        lsttemp.Add(cate2);
                        lsttemp.Add(cate1);
                        ViewBag.ProductName = product.ProductName;
                        return PartialView(lsttemp);
                    }
                }
            }

            return PartialView();
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        public ActionResult SelectProduct(int id)
        {
            var lstcolor = db.Colors.Where(m => m.ProductId == id).ToList();
            return PartialView(lstcolor);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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