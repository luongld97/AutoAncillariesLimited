using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.Dao
{
  public class WarehouseDetailDao
  {

    public IEnumerable<WarehouseDetail> WarehouseDetails(AALEntities entities, int warehouseId)
    {
      var list = entities.WarehouseDetails.ToList().Where(wd => wd.WarehouseId.Equals(warehouseId));
      return list;
    }
  }
}