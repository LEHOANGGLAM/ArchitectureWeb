using BlueDataWeb.Models.Entites;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class TrienKhaiController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();

        public ActionResult Index()
        {
            NewCategory cate = db.NewCategories.AsNoTracking().Where(m => m.KeyName == "E_TrienKhai").FirstOrDefault();
            var data = db.News.AsNoTracking().Where(s => s.NewCategoriesID == cate.NewCategoriesID && s.AppID == this.AppID && (s.IsDelete == null || s.IsDelete.Value == false)).ToList();
            return View(data);
        }

        public ActionResult Details(int id = 0)
        {
            News news = db.News.AsNoTracking().Where(x => x.NewsID == id && x.AppID == this.AppID).FirstOrDefault();

            NewCategory cate = db.NewCategories.AsNoTracking().Where(m => m.KeyName == "E_TrienKhai").FirstOrDefault();
            var data = db.News.AsNoTracking().Where(s => s.NewCategoriesID == cate.NewCategoriesID && s.AppID == this.AppID && (s.IsDelete == null || s.IsDelete.Value == false)).ToList();
            ViewBag.AllData = data;
            return View(news);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}