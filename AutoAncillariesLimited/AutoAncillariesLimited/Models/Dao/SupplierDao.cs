using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.Dao
{
  public class SupplierDao
  {
    public Supplier Supplier(AALEntities entities, int id)
    {
      return entities.Suppliers.Find(id);
    }
  }
}