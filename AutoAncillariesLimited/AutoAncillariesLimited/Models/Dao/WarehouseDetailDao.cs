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

        public WarehouseDetail MaxQuantityDetails(AALEntities entities, int warehouseId, int productId)
        {
            var list = entities.WarehouseDetails.ToList()
                .SingleOrDefault(n => n.WarehouseId == warehouseId && n.ProductId == productId);
            return list;
        }
    }
}