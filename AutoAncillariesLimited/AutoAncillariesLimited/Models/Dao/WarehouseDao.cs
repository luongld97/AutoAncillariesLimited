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
  }
}