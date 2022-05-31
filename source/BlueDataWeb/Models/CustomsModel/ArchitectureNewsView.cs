using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BlueDataWeb.Models.CustomsModel
{
    public class ArchitectureNewsView
    {
        public List<BlueDataWeb.Models.Entites.ArchitectureNew> data { get; set; }
        public  BlueDataWeb.Models.Entites.ArchitectureCategory cate { get; set; }
        public List<BlueDataWeb.Models.Entites.ArchitectureCategory> cateChild { get; set; }
        public BlueDataWeb.Models.Entites.ArchitectureCategory  cateParent { get; set; }
       

        public ArchitectureNewsView()
        {
            data = new List<BlueDataWeb.Models.Entites.ArchitectureNew>();
        }
    }
}