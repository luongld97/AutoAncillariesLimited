using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoAncillariesLimited.Models.View_Models
{
  public class ImportBillViewModel
  {
    public ImportBill ImportBill { get; set; }
    public List<ImportBillDetail> Details { get; set; }
    public int WarehouseId { get; set; }
  }
}