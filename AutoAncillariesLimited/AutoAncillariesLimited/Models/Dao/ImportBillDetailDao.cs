using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.Dao
{
  public class ImportBillDetailDao
  {
    private readonly AALEntities entities = new AALEntities
    {
      Configuration =
      {
        ProxyCreationEnabled = false
      }
    };

    public bool ImportBillDetailInsert(ImportBill importBill, Product product, int quantity, decimal price)
    {
      try
      {
        var detail = new ImportBillDetail
        {
          ImportBill = importBill,
          Product = product,
          Quantity = quantity,
          Price = price
        };
        entities.ImportBillDetails.Add(detail);
        entities.SaveChanges();
      }
      catch (Exception)
      {
        // ignored
      }
      return false;
    }
  }
}