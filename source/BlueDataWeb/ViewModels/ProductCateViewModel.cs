using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlueDataWeb.Models.Entites;

namespace BlueDataWeb.ViewModels
{
    public class ProductCateViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categorys { get; set; }
        public List<Image> Images { get; set; }
        public int CountProduct { get; set; }
    }
}