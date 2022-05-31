using BlueDataWeb.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class SliderController : BaseController
    {
        //
        // GET: /Sider/
        private BlueDataWebEntities db = new BlueDataWebEntities();

        public ActionResult ViewSlider(string typleSider)
        {
            List<Image> lstSlider = db.Images.AsNoTracking().Where(m => m.Type == typleSider && m.IsShow == true && m.AppID == this.AppID).OrderByDescending(x=>x.ImageId).ToList();
            return PartialView("_ViewSlider", lstSlider);
        }

        public ActionResult ViewSider_detail(string typleSider)
        {
            var lstSlider = db.Images.AsNoTracking().Where(m => m.Type == typleSider && m.IsShow == true && m.AppID == this.AppID).OrderByDescending(x => x.ImageId).ToList();
            return PartialView("_ViewSider_detail", lstSlider);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
