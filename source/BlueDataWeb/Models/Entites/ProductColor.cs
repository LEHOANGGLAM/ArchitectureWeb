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
    
    public partial class ProductColor
    {
        public ProductColor()
        {
            this.Colors = new HashSet<Color>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    
        public virtual ICollection<Color> Colors { get; set; }
    }
}
