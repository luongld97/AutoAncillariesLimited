using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models
{
  public class ImportBillDetailMetaData
  {
    [Required]
    public int Id { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int ImportBillId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal Price { get; set; }
  }
  [MetadataType(typeof(ImportBillMetaData))]
  public partial class ImportBillDetail
  {
    
  }
}