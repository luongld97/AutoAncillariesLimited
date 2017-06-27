using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.View_Models
{
  public class ImportBillModel
  {
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public string Employee { get; set; }
    public string Supplier { get; set; }
    public decimal TotalPrice { get; set; }
  }
}