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
    public ActionResult Warehouses()
      {
          var json = JsonConvert.SerializeObject(entities.Warehouses,
              Formatting.None,
              new JsonSerializerSettings
              {
                  ReferenceLoopHandling = ReferenceLoopHandling.Ignore
              });
            return Content(json, "application/json");
      }

      public ActionResult ProductsInWarehouse(int id)
    {
      List<Product> products;
      if (!id.Equals(-1))
      {
        products = new List<Product>();
        var wDao = new WarehouseDao();
        var warehouseDetails = wDao.Warehouse(entities, id).WarehouseDetails;
        foreach (var warehouseDetail in warehouseDetails)
        {
          var product = warehouseDetail.Product;
          if (products.SingleOrDefault(pro => pro.Id.Equals(product.Id)) == null)
            products.Add(product);
        }
      }
      else products = entities.Products.ToList();
      var productModels = products.Select(product => new ProductModel
      {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price.Value,
        Description = product.Description,
        CategoryId = product.CategoryId.Value,
        Inventory = product.Inventory.Value,
        CategoryName = product.Category.Name
      });
      return Json(productModels, JsonRequestBehavior.AllowGet);
    }
  }
}