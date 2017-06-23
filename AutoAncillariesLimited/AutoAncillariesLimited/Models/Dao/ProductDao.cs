using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.Dao
{
  public class ProductDao
  {
    public Product Product(AALEntities entities,int id)
    {
      return entities.Products.Find(id);
    }

    public List<Product> Products(AALEntities entities, string[] id)
    {
      var products = new List<Product>();
      for (var i = 1; i < id.Length; i++)
      {
        try
        {
          var product = entities.Products.Find(int.Parse(id[i]));
          if (product != null)
            products.Add(product);
        }
        catch
        {
          // ignored
        }
      }

      return products;
    }
  }
}