using BlueDataWeb.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class DuAnHTController : Controller
    {
        //
        // GET: /DuAnDaHoanThanh/
        private BlueDataWebEntities db = new BlueDataWebEntities();

        public ActionResult DuAnDaHoanThanh_Index()
        {
            return View();
        }
      

    }
}
