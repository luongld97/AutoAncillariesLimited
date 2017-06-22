using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.Dao
{
  public class ProductDao
  {
    private readonly AALEntities entities = new AALEntities
    {
      Configuration =
      {
        ProxyCreationEnabled = false
      }
    };
    public Product Product(int id)
    {
      return entities.Products.Find(id);
    }
  }
}