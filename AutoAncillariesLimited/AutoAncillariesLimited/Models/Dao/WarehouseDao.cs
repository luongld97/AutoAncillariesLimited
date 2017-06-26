using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.Dao
{
  public class WarehouseDao
  {
    public Warehouse Warehouse(AALEntities entities, int id)
    {
      return entities.Warehouses.Find(id);
    }

    public List<Product> ProductsInWarehouse(AALEntities entities, int warehouseId)
    {
      var wdDao = new WarehouseDetailDao();
      var details = wdDao.WarehouseDetails(entities, warehouseId);
      var products = details.Select(detail => detail.Product).ToList();
      return products;
    }
  }
}