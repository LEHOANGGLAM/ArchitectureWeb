using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueDataWeb.Models.CustomsModel
{
    public class GlobalVariables
    {
        public static string PathFolderName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PathFolderName"];
            }
        }

        public static int DefaultPageSize
        {
            get
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultPageSize"]);
            }
        }
        public static string PagegiaiphaoKD
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PagegiaiphaoKD"];
            }
        }

        public static int GiaiPhapID
        {
            get
            {
                return  Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["GiaiPhapID"]);
            }
        }
       
       
    }
}