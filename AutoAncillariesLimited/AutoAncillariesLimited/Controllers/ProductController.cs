using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;

namespace AutoAncillariesLimited.Controllers
{
  public class ProductController : Controller
  {
    private readonly AALEntities entities = new AALEntities
    {
      Configuration =
      {
        ProxyCreationEnabled = false
      }
    };
    // GET: Product
    public ActionResult ProductManagement()
    {
      return View();
    }

    // Fill data from database to list products table

    public ActionResult Products() => Json(entities.Products.ToList(), JsonRequestBehavior.AllowGet);
  }
}