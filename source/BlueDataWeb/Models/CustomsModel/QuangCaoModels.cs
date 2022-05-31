using BlueDataWeb.Models.Entites;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BlueDataWeb.Models.CustomsModel
{
    public class QuangCaoModels : QuangCao
    {
        public List<SelectListItem> ViTriQuangCaoList { get; set; }

        public QuangCaoModels()
        {
            BlueDataWebEntities db = new BlueDataWebEntities(); ;

            ViTriQuangCaoList = new List<SelectListItem>
                          {
                              new SelectListItem{ Text = "Top", Value = "0"},
                              new SelectListItem{ Text = "Center", Value = "1"}
                          };
        }
    }
}