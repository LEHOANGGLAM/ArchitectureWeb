using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;

namespace BlueDataWeb.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public List<Color> Colors { get; set; }
        public decimal CartTotal { get; set; }
        public int Sumcart { get; set; }
    }
}