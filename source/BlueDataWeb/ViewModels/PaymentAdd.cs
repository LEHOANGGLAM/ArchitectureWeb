using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.ViewModels
{
    public class PaymentAdd
    {
        public IEnumerable<SelectListItem> SelectDis { get; set; }
        public double PriceSum { get; set; }
        public double TotalOrder { get; set; }
    }
}