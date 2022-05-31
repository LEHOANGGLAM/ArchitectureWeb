using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;

namespace BlueDataWeb.Areas.Admin.Models
{
    public class Bill
    {
        public string ShipperName { get; set; }
        public string ShipperTel { get; set; }
        public string ShipperCompany { get; set; }
        public string ShipperAddress { get; set; }
        public string RecieverName { get; set; }
        public string RecieverTel { get; set; }

        public string RecieverCompany { get; set; }

        public string RecieverAddress { get; set; }
        public string RecieverContry { get; set; }
        public string RecieverPostCode { get; set; }

        public string Service { get; set; }

        public string DescriptionNo { get; set; }
        public string DescriptionActualWeight { get; set; }

        public string DescriptionDimWeight { get; set; }

        public string DescriptionValue { get; set; }

        public string DescriptionContents { get; set; }

        public string DescriptionExport { get; set; }
        public string Country { get; set; }

        public List<SelectListItem> Countrylst { get; set; } // List bộ phận (phòng) 

        public Bill()
        {
            BlueDataWebEntities db = new BlueDataWebEntities();
            Countrylst = db.Countries.Select(x => new SelectListItem { Text = x.CountryName, Value = x.CountryName }).ToList();
            Countrylst.Insert(0, new SelectListItem { Text = "-- select country --", Value = "" });
        }

    }
}