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
    
    public partial class QuangCao
    {
        public int QuangCaoID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public Nullable<bool> IsShow { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Height { get; set; }
        public string Image { get; set; }
        public Nullable<int> PositionID { get; set; }
    }
}
