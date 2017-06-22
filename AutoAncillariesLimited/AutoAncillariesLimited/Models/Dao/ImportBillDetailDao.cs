using System;
using System.Collections.Generic;
using System.Data.Entity;
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

    public bool ImportBillDetailInsert(ImportBill importBill, List<ImportBillDetail> details, int id)
    {
      try
      {
        var wdDao = new WarehouseDetailDao();
        var pDao = new ProductDao();
        foreach (var importBillDetail in details)
        {
          var product = pDao.Product(importBillDetail.ProductId.Value);
            product.Inventory += importBillDetail.Quantity.Value;
          entities.Products.Attach(product);
          entities.Entry(product).State = EntityState.Modified;
          entities.SaveChanges();
          importBillDetail.ImportBillId = importBill.Id;
          entities.ImportBillDetails.Add(importBillDetail);
          wdDao.ImportWarehouseDetail(id, importBillDetail);
        }
        entities.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        // ignored
      }
      return false;
    }
  }
}