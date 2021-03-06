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
    
    public partial class ArchitectureNew
    {
        public long ID { get; set; }
        public Nullable<int> AppID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> Views { get; set; }
        public Nullable<long> CategoriesID { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsNew { get; set; }
        public Nullable<bool> IsNoiBat { get; set; }
        public Nullable<int> OrderBY { get; set; }
        public string KeyName { get; set; }
    
        public virtual ArchitectureCategory ArchitectureCategory { get; set; }
    }
}
