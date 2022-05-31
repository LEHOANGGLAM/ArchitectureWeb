using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ExpressBD.Areas.Admin.Models;
using ExpressBD.Areas.Admin.Models.Service;
using ExpressBD.Models.Service;

using ExpressBD.Models;

namespace ExpressBD.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminExportBill")]
    public class ExportBillController : Controller
    {
        //
        // GET: /Admin/ExportBill/
        ExpressBDEntities db = new ExpressBDEntities();
        public ActionResult Index()
        {
            var lstExport = db.ExportBills.ToList();
            return View(lstExport);
        }

        public Bill billnew = new Bill();

        public ActionResult Create(int? id)
        {
            ViewBag.ShipperCountry = new SelectList(db.Countries, "ID", "CountryName");
            ViewBag.RecieverCountry = new SelectList(db.Countries, "ID", "CountryName");
            if (Request.HttpMethod == "GET")
            {
                var data = new ExportBill();
                if (id > 0)
                {
                    data = db.ExportBills.Where(m => m.ID == id).FirstOrDefault();
                    ViewBag.ShipperCountry = new SelectList(db.Countries, "ID", "CountryName", data.ShipperCountry);
                    ViewBag.RecieverCountry = new SelectList(db.Countries, "ID", "CountryName", data.RecieverContry);
                }

                return View(data);
            }

            var bill = new ExportBill();
            TryUpdateModel(bill);
            var lstCounTry = db.Countries;

            int CtrShip = Convert.ToInt32(bill.ShipperCountry);
            int CtrRe = Convert.ToInt32(bill.RecieverCountry);

            bill.RecieverCounTryTemp = lstCounTry.Where(m => m.ID == CtrShip).Select(m => m.CountryName).FirstOrDefault();
            bill.Countrytemp = lstCounTry.Where(m => m.ID == CtrRe).Select(m => m.CountryName).FirstOrDefault();

            if (bill.ID > 0)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.ExportBills.Add(bill);
                db.SaveChanges();
            }

            List<ExportBill> lstbill = new List<ExportBill>();
            lstbill.Add(bill);
            string strTemplateFileName = string.Empty;
            strTemplateFileName = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["linkExport"]);
            ConvertWord.ExportFile(lstbill, strTemplateFileName);
            return View(bill);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            ExportBill data = db.ExportBills.Where(m => m.ID == id).FirstOrDefault();
            db.ExportBills.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
