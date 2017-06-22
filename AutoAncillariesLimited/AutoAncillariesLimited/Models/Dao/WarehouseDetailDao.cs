using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.Dao
{
  public class WarehouseDetailDao
  {
    private readonly AALEntities entities = new AALEntities
    {
      Configuration =
      {
        ProxyCreationEnabled = false
      }
    };

    public bool ImportWarehouseDetail(int id, ImportBillDetail detail)
    {
      try
      {
        var warehouseDetail = new WarehouseDetail
        {
          ProductId = detail.ProductId,
          WarehouseId = id,
          Quantity = detail.Quantity
        };
        entities.WarehouseDetails.Add(warehouseDetail);
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