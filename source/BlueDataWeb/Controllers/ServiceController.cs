using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;

namespace BlueDataWeb.Controllers
{
    public class ServiceController : Controller
    {
        BlueDataWebEntities db = new BlueDataWebEntities();
        //
        // GET: /Service/

        public ActionResult Index()
        {
            var lstService = db.AboutCategories.Where(m=>m.Type==null).ToList();
            return View(lstService);
        }

        public ActionResult SiteShow()
        {
            var lstService = db.AboutCategories.Where(m => m.Type == null).OrderBy(m => m.AboutID).Take(4).ToList();
            return PartialView(lstService);
        }

        public ActionResult Details(int id)
        {
            var service = db.AboutCategories.Find(id);
            ViewBag.lstservice = db.AboutCategories.Where(m => m.Type == null).OrderBy(m => m.AboutID).ToList();
            ViewBag.Title = service.Name;
            return View(service);
        }

    }
}
