using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models
{
  public class ImportBillMetaData
  {
    [Required]
    public DateTime CreateDate { get; set; }
    [Required]
    public int SupplierId { get; set; }
    [Required]
    public int EmployeeId { get; set; }
    
  }
  [MetadataType(typeof(ImportBillMetaData))]
  public partial class ImportBill
  {
    
  }
}