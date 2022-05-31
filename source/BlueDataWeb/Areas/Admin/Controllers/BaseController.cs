using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {

        public int AppID = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["AppID"]);
        public int Image_slider_w = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["Image_slider_w"]);
        public int Image_slider_h = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["Image_slider_h"]);
        public int Image_new_avata_w = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["Image_new_avata_w"]);
        public int Image_new_avata_h = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["Image_new_avata_h"]);
        public int Image_new_avata_w_bangia = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["Image_new_avata_w_bangia"]);
        public int Image_new_avata_h_bangia = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["Image_new_avata_h_bangia"]);

        public int Image_new_avata_w_small = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["Image_new_avata_w_small"]);
        public int Image_new_avata_h_small = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["Image_new_avata_h_small"]);
        
        public BaseController()
        {

        }
    }
}
