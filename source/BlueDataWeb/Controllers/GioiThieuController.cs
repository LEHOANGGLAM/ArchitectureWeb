using BlueDataWeb.Models.Entites;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class GioiThieuController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        //
        // GET: /About/

        public ActionResult Index()
        {
            var lstBaiVietGioiThieu = db.Abouts.AsNoTracking().Where(x=>x.AppID == this.AppID).OrderBy(x=>x.Order_by).ToList();
            return View(lstBaiVietGioiThieu);
        }
        public ActionResult Top()
        {
            var lstBaiVietGioiThieu = db.Abouts.AsNoTracking().Where(x => x.AppID == this.AppID).OrderBy(x => x.Order_by).ToList();
            return PartialView("_GioiThieuTop", lstBaiVietGioiThieu.First());
        }
        public ActionResult Detail(int id)
        {

            var lstBaiVietGioiThieu = db.Abouts.AsNoTracking().Where(x => x.AppID == this.AppID).ToList();

            var about = db.Abouts.AsNoTracking().Where(x => x.AppID == this.AppID).
                Where(m => m.ID == id ).FirstOrDefault();





            ViewBag.lstBaiVietGioiThieu = lstBaiVietGioiThieu;

            return View(about);
        }
        public ActionResult GioiThieu_Index()
        {
            return View();
        }
    }
}