using BlueDataWeb.Models.Entites;
using BlueDataWeb.ServiceClass.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class BaseController : Controller
    {
        public int AppID = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["AppID"]);

        public int GioiThieuPageHome = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["GioiThieuPageHome"]);
        public int SPHome = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["SPHome"]);
        public int DuanHome = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["DuanHome"]);
        public int UnngDungHome = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["UnngDungHome"]);

        
        public int BlockNumber_LayZyLoad = int.Parse(System.Configuration.ConfigurationManager.AppSettings["BlockNumber"]);
        public int pagesize_TinCategory = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TinCategory"]);
        public List<NewCategory> lstNewCategory = new List<NewCategory>();

        public BaseController()
        {
            CommonService srv = new CommonService();
            
            var lstSetting = srv.GetSetting();
            ViewBag.lstSetting = lstSetting;

            var KienTrucCategory = srv.GetKienTrucCategory();
            ViewBag.KienTrucCategory = KienTrucCategory;
            
        }
    }
}