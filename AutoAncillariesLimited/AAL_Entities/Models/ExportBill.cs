namespace AAL_Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExportBill")]
    public partial class ExportBill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExportBill()
        {
            ExportBillDetails = new HashSet<ExportBillDetail>();
        }

        public int Id { get; set; }

        [StringLength(10)]
        public string CreateDate { get; set; }

        public bool? Status { get; set; }

        public decimal? Promotion { get; set; }

        public int? CustomerId { get; set; }

        public int? EmployeeId { get; set; }

        public int? WarehouseId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExportBillDetail> ExportBillDetails { get; set; }
    }
}
