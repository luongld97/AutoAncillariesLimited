using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.Dao
{
  public class ProductDao
  {
    public Product Product(AALEntities entities, int id)
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

    public bool IsExist(Product product, IEnumerable<Product> products)
    {
      var result = products.SingleOrDefault(pro => pro.Name.Equals(product.Name) && !pro.Id.Equals(product.Id));
      return result != null;

    }
  }
}