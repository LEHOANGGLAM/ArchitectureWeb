//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueDataWeb.Models.Entites
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ship
    {
        public int ID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> ZoneID { get; set; }
        public string ProviceDistrictID { get; set; }
        public Nullable<double> Price { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
