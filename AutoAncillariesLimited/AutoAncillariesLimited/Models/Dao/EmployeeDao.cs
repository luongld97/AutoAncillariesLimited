using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.Dao
{
  public class EmployeeDao
  {
    public Employee Employee(AALEntities entities, int id)
    {
      return entities.Employees.Find(id);
    }
  }
}