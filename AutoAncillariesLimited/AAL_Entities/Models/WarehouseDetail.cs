namespace AAL_Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WarehouseDetail")]
    public partial class WarehouseDetail
    {
        public int Id { get; set; }

        public int? WarehouseId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
