namespace AAL_Entities.Models
{
  using System;
  using System.Data.Entity;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;

  public partial class AALModel : DbContext
  {
    public AALModel()
        : base("name=AALEntities")
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }
    public virtual DbSet<BillDetail> BillDetails { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<ExportBill> ExportBills { get; set; }
    public virtual DbSet<ExportBillDetail> ExportBillDetails { get; set; }
    public virtual DbSet<ImportBill> ImportBills { get; set; }
    public virtual DbSet<ImportBillDetail> ImportBillDetails { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<Warehouse> Warehouses { get; set; }
    public virtual DbSet<WarehouseDetail> WarehouseDetails { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Customer>()
          .Property(e => e.Email)
          .IsUnicode(false);

      modelBuilder.Entity<Customer>()
          .Property(e => e.Phone)
          .IsUnicode(false);

      modelBuilder.Entity<Employee>()
          .Property(e => e.Username)
          .IsUnicode(false);

      modelBuilder.Entity<Employee>()
          .Property(e => e.Password)
          .IsUnicode(false);

      modelBuilder.Entity<Employee>()
          .Property(e => e.Email)
          .IsUnicode(false);

      modelBuilder.Entity<Employee>()
          .Property(e => e.Phone)
          .IsUnicode(false);

      modelBuilder.Entity<ExportBill>()
          .Property(e => e.CreateDate)
          .IsFixedLength();

      modelBuilder.Entity<ExportBill>()
          .Property(e => e.Promotion)
          .HasPrecision(3, 2);

      modelBuilder.Entity<ExportBillDetail>()
          .Property(e => e.Price)
          .HasPrecision(19, 4);

      modelBuilder.Entity<ImportBill>()
          .Property(e => e.Promotion)
          .HasPrecision(3, 2);

      modelBuilder.Entity<ImportBillDetail>()
          .Property(e => e.Price)
          .HasPrecision(19, 4);

      modelBuilder.Entity<Product>()
          .Property(e => e.Price)
          .HasPrecision(19, 4);

      modelBuilder.Entity<Product>()
          .Property(e => e.PromotionPrice)
          .HasPrecision(19, 4);

      modelBuilder.Entity<Product>()
          .Property(e => e.Photo)
          .IsUnicode(false);

      modelBuilder.Entity<Product>()
          .Property(e => e.SubTitle)
          .IsFixedLength();

      modelBuilder.Entity<Product>()
          .Property(e => e.MetaTitle)
          .IsUnicode(false);

      modelBuilder.Entity<Supplier>()
          .Property(e => e.Email)
          .IsUnicode(false);

      modelBuilder.Entity<Supplier>()
          .Property(e => e.Phone)
          .IsUnicode(false);
    }
  }
}
