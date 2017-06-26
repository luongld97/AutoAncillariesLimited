using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.Dao
{
  public class ImportBillDao
  {
    private readonly AALEntities entities = new AALEntities();
    public Supplier Supplier(int id)
    {
      return entities.Suppliers.Find(id);
    }

    public Employee Employee(int id)
    {
      return entities.Employees.Find(id);
    }
  }
}