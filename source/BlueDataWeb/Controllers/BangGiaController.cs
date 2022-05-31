using BlueDataWeb.Models.Entites;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using WebMVC.Entities;

namespace BlueDataWeb.Controllers
{
    public class BangGiaController : BaseController
    {
        public ActionResult Index()
        {
            var srv = new BaoGiaService();
            List<BaoGia> dataBaoGia = new List<BaoGia>();
            dataBaoGia = srv.BaoGia_GetAll(this.AppID, 1);

            return View("BangGia_Index", dataBaoGia);
        }

        public ActionResult Details(long id)
        {
            var srv = new BaoGiaService();
            List<BaoGia> dataBaoGia = new List<BaoGia>();
            dataBaoGia = srv.BaoGia_GetAll(this.AppID, 1);
            ViewBag.AllBaoGia = dataBaoGia;

            var _baogia = new BaoGia();
            var _lstGiaTri = new List<BaoGia_GiaTri>();
            _baogia = srv.BaoGia_Get_ByID(id);
            _lstGiaTri = srv.BaoGia_GiaTri_GetAll(this.AppID, id, 1);
            ViewBag.lstGiaTri = _lstGiaTri;

            return View(_baogia);
        }


        public ActionResult TopBaoGiaHome()
        {
            var srv = new BaoGiaService();

            var _lstAllGiaTri = new List<List<BaoGia_GiaTri>>();
            List<BaoGia> dataBaoGia = new List<BaoGia>();
            dataBaoGia = srv.BaoGia_GetAll(this.AppID, 1);
             
            if(dataBaoGia != null && dataBaoGia.Count > 0)
            {
                foreach (var item in dataBaoGia)
                {
                    var _lstGiaTri = new List<BaoGia_GiaTri>();
                    
                    _lstGiaTri = srv.BaoGia_GiaTri_GetAll(this.AppID, item.BaoGiaID, 1);
                    _lstAllGiaTri.Add(_lstGiaTri);
                }
            }

            ViewBag.AllGiaTri = _lstAllGiaTri;

            return View("_TopBaoGiaHome", dataBaoGia);
        }



        public FileResult DownloadFile(string fileName)
        {
            var FileVirtualPath = "/Images/News/" + fileName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }


    }
}