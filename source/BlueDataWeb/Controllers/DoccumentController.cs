using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Paging;
using BlueDataWeb.Models.CustomsModel;



namespace BlueDataWeb.Controllers
{
    public class DoccumentController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        //
        // GET: /Doccument/

        public ActionResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            ViewBag.pagesize = (page - 1) * GlobalVariables.DefaultPageSize; // xử lý thứ tự danh sách
            return View("Index", db.Documents.OrderByDescending(m => m.DateCreate).ToPagedList(currentPageIndex, GlobalVariables.DefaultPageSize));
        }

    }
}
