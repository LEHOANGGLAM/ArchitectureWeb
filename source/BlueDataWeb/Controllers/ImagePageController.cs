using BlueDataWeb.Models.Entites;
using BlueDataWeb.Paging;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class ImagePageController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();



        public ActionResult GetByKeyName(string keyName)
        {

            ImagePage data = db.ImagePages.Where(x => x.KeyName.ToUpper() == keyName.ToUpper()).FirstOrDefault();
            TempData["Description"] = data.MetaDescription;
            TempData["Keywords"] = data.MetaKeywords;
            return PartialView("_BannerPage", data);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}