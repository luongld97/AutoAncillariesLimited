using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.View_Models
{
  public class ProductModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Inventory { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
  }
}