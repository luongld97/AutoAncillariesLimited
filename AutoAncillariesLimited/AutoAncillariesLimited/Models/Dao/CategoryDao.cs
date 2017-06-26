using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.Dao
{
  public class CategoryDao
  {
    public Category Category(AALEntities entities, int id)
    {
      return entities.Categories.SingleOrDefault(cat => cat.Id.Equals(id));
    }
  }
}