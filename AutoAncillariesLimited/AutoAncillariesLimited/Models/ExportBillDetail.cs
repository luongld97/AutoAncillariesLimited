//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoAncillariesLimited.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExportBillDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ExportBillId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual ExportBill ExportBill { get; set; }
        public virtual Product Product { get; set; }
    }
}
