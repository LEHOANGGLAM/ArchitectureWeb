using BlueDataWeb.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class HomeController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();

        public ActionResult Index()
        {
            var LstGioiThieu = db.Abouts.AsNoTracking().Where(x => x.AppID == this.AppID).Take(this.GioiThieuPageHome).ToList();

            List<NewCategory> lstNewCategory = this.lstNewCategory;

            NewCategory newcategory_cn = lstNewCategory.Where(m => m.KeyName == "E_ChucNang" && m.AppID == this.AppID ).FirstOrDefault();

            if (newcategory_cn != null)
            {
                var LstSanPham = db.News.AsNoTracking().Where(x => x.AppID == this.AppID && x.NewCategoriesID == newcategory_cn.NewCategoriesID
                 && (x.IsDelete == null || x.IsDelete.Value == false)
                ).Take(this.SPHome).ToList();
                ViewBag.LstSanPham = LstSanPham;
            }

            NewCategory newcategory_gp = lstNewCategory.Where(m => m.KeyName == "E_GiaiPhap" && m.AppID == this.AppID).FirstOrDefault();

            if (newcategory_gp != null)
            {
                var LstGiaiPhap = db.News.AsNoTracking().Where(x => x.AppID == this.AppID && x.NewCategoriesID == newcategory_gp.NewCategoriesID
                 && (x.IsDelete == null || x.IsDelete.Value == false)).Take(this.SPHome).ToList();
                ViewBag.LstGiaiPhap = LstGiaiPhap;
            }
 

            NewCategory duAncategory = lstNewCategory.Where(m => m.KeyName == "E_TrienKhai" && m.AppID == this.AppID).FirstOrDefault();
            if (duAncategory != null)
            {
                var LstDuAn = db.News.AsNoTracking().Where(x => x.AppID == this.AppID && x.NewCategoriesID == duAncategory.NewCategoriesID && x.isCaseStudy == true
                 && (x.IsDelete == null || x.IsDelete.Value == false)


                ).Take(this.DuanHome).ToList();
                ViewBag.LstDuAn = LstDuAn;
            }

            NewCategory _ungDungcategory = lstNewCategory.Where(m => m.KeyName == "E_UngDung" && m.AppID == this.AppID).FirstOrDefault();
            if (duAncategory != null)
            {
                var LstUngDung = db.News.AsNoTracking().Where(x => x.AppID == this.AppID && x.NewCategoriesID == _ungDungcategory.NewCategoriesID
                 && (x.IsDelete == null || x.IsDelete.Value == false)

                ).Take(this.UnngDungHome).ToList();
                ViewBag.LstUngDung = LstUngDung;
            }


            var lstDoiTac = db.Images.AsNoTracking().Where(x => x.AppID == this.AppID && x.Type == "E_Partners").ToList();
      

            ViewBag.lstNewCategory = lstNewCategory;
            ViewBag.LstGioiThieu = LstGioiThieu;
            ViewBag.lstDoiTac = lstDoiTac;
            return View();
        }

        public ActionResult SanPham()
        {
            NewCategory newcategory = db.NewCategories.Where(m => m.KeyName == "E_ChucNang").FirstOrDefault();
            var lstnew = db.News.Where(m => m.NewCategoriesID == newcategory.NewCategoriesID && (m.IsDelete == null || m.IsDelete.Value == false)).OrderByDescending(m => m.CreatedDate).ToList();
            return View(lstnew);
        }

        public ActionResult DichVu()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TinTuc()
        {
            return View();
        }

        public ActionResult DuAn()
        {
            NewCategory newcategory = db.NewCategories.Where(m => m.KeyName == "E_TrienKhai").FirstOrDefault();
            var lstnew = db.News.Where(m => m.NewCategoriesID == newcategory.NewCategoriesID && (m.IsDelete == null || m.IsDelete.Value == false)).OrderByDescending(m => m.CreatedDate).ToList();
            return View(lstnew);
        }

        public ActionResult ThanhTich()
        {
            var lstAnh = db.Images.Where(m => m.Type == "E_ThanhTich").ToList();
            return View(lstAnh);
        }

        public ActionResult DoiTac()
        {
            var lstAnh = db.Images.Where(m => m.Type == "E_Partners").ToList();
            return View(lstAnh);
        }

        public ActionResult DoiTacHome()
        {
            var lstSlider = db.Images.Where(m => m.Type == "E_Partners" && m.IsShow == true).ToList();
            return PartialView("_A_DoiTac", lstSlider);
        }

        public ActionResult HinhAnh()
        {
            var lstAnh = db.Images.Where(m => m.Type == "E_AnhHoatDong").ToList();
            return View(lstAnh);
        }

        public ActionResult HinhAnhHome()
        {
            var lstAnh = db.Images.Where(m => m.Type == "E_AnhHoatDong" && m.IsShow == true).ToList();
            return PartialView("_A_HinhAnh", lstAnh);
        }

        public ActionResult Search(string keyword)
        {
            var lstnew = new List<News>();
            if (!String.IsNullOrEmpty(keyword))
            {
                lstnew = db.News.Where(s => s.Title.Contains(keyword)
                                       || s.ShortDescription.Contains(keyword) && (s.IsDelete == null || s.IsDelete.Value == false)).ToList();
            }

            return View(lstnew);
        }

        public ActionResult Home_Index()
        {
            return View();
        }
        public ActionResult GioiThieu()
        {
            return View();
        }
        public ActionResult ThietKeKienTruc()
        {
            return View();
        }
        public ActionResult DichVuXayDung()
        {
            return View();
        }
        public ActionResult DichVuSuaChuaNha()
        {
            return View();
        }
        public ActionResult BangBaoGia()
        {
            return View();
        }
        public ActionResult PhongThuy()
        {
            return View();
        }
        public ActionResult MauNhaDep()
        {
            return View();
        }
        public ActionResult DuAnDaHoanThanh()
        {
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }
    }
}
