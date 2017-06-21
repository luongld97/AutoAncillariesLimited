namespace AAL_Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImportBill")]
    public partial class ImportBill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImportBill()
        {
            ImportBillDetails = new HashSet<ImportBillDetail>();
        }

        public int Id { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Status { get; set; }

        public decimal? Promotion { get; set; }

        public int? SupplierId { get; set; }

        public int? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImportBillDetail> ImportBillDetails { get; set; }
    }
}
