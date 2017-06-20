using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoAncillariesLimited.Models.View_Models
{
  public class ProductViewModel
  {
    public Product Product { get; set; }
    public List<SelectListItem> Categories { get; set; }
  }
}