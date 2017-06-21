namespace AAL_Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BillDetails = new HashSet<BillDetail>();
            ExportBillDetails = new HashSet<ExportBillDetail>();
            ImportBillDetails = new HashSet<ImportBillDetail>();
            WarehouseDetails = new HashSet<WarehouseDetail>();
        }

        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public int Inventory { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [Column(TypeName = "money")]
        public decimal? PromotionPrice { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(250)]
        public string Photo { get; set; }

        [StringLength(10)]
        public string SubTitle { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public int? CategoryId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillDetail> BillDetails { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExportBillDetail> ExportBillDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImportBillDetail> ImportBillDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarehouseDetail> WarehouseDetails { get; set; }
    }
}
