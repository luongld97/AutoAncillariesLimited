namespace AAL_Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExportBillDetail")]
    public partial class ExportBillDetail
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? ExportBillId { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public virtual ExportBill ExportBill { get; set; }

        public virtual Product Product { get; set; }
    }
}
