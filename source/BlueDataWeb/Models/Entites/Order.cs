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
    
    public partial class Order
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int OrderId { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public Nullable<int> ProviceID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> WardID { get; set; }
        public string Phone { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string PaymentTransactionId { get; set; }
        public Nullable<int> IsOrderChecked { get; set; }
        public string Note { get; set; }
        public Nullable<double> DeleveryFee { get; set; }
        public Nullable<decimal> Ship { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual District District { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Provice Provice { get; set; }
        public virtual Ward Ward { get; set; }
    }
}
