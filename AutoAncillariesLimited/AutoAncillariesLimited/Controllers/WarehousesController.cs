using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;
using AutoAncillariesLimited.Models.Dao;
using AutoAncillariesLimited.Models.View_Models;
using Newtonsoft.Json;

namespace AutoAncillariesLimited.Controllers
{
  public class WarehousesController : Controller
  {
    // GET: Warehouses
    private readonly AALEntities entities = new AALEntities();

    public ActionResult WarehouseManagement()
    {
      ViewBag.Warehouses = entities.Warehouses.ToList();
      return View();
    }
    public ActionResult Warehouses() => Json(entities.Warehouses.ToList(), JsonRequestBehavior.AllowGet);

    public ActionResult ProductsInWarehouse(int id)
    {
      var wDao = new WarehouseDao();
      var warehouse = wDao.Warehouse(entities, id);
      var wds = warehouse.WarehouseDetails;
      var products = wds.Select(wd => new Product
      {
        Id = wd.Product.Id,
        Name = wd.Product.Name,
        Price = wd.Product.Price,
        Description = wd.Product.Description,
        Category = wd.Product.Category,
        Inventory = wd.Quantity.Value
      });
      var list = JsonConvert.SerializeObject(products,
        Formatting.None,
        new JsonSerializerSettings
        {
          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
      return Content(list, "application/json");
    }
  }
}