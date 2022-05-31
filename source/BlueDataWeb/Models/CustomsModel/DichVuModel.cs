using BlueDataWeb.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueDataWeb.Models.CustomsModel
{
    public class DichVuModel
    {
        public NewCategory New { get; set; }
        public List<News> ListNews { get; set; }
    }


    public class NewCateModel
    {
        public NewCategory NewCate { get; set; }
        public List<News> ListNews { get; set; }
    }
}