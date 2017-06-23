using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;
using AutoAncillariesLimited.Models.Dao;
using AutoAncillariesLimited.Models.View_Models;
using Newtonsoft.Json;

namespace AutoAncillariesLimited.Controllers
{
  public class ImportBillController : Controller
  {
    private readonly AALEntities entities = new AALEntities
    {
      Configuration =
        {
          ProxyCreationEnabled = false
        }
    };
    // GET: ImportBill
    public ActionResult ImportBillManagement()
    {

      ViewBag.ImportBills = entities.ImportBills.ToList();
      return View();
    }

    public ActionResult ImportBillInsertForm()
    {
      ViewBag.Warehouses = entities.Warehouses.ToList();
      ViewBag.Suppliers = entities.Suppliers.ToList();
      ViewBag.Products = entities.Products.ToList();
      return PartialView("_ImportBillInsert");
    }

    public ActionResult ImportBillInsert(FormCollection formCollection)
    {
      try
      {
        var supplierId = int.Parse(formCollection["supplierId"]);
        var warehouseIds = formCollection["warehouseId"].Split(',');
        var pDao = new ProductDao();
        var whDao = new WarehouseDao();
        var sDao = new SupplierDao();
        var eDao = new EmployeeDao();
        var importBill = new ImportBill
        {
          CreateDate = DateTime.Now,
          Supplier = sDao.Supplier(entities, supplierId),
          Employee = eDao.Employee(entities, int.Parse(Session["employee"].ToString())),
          Status = true
        };
        if (ModelState.IsValid)
        {
          entities.ImportBills.Add(importBill);
          entities.SaveChanges();
        }
        for (var i = 1; i < warehouseIds.Length; i++)
        {
          var productIds = formCollection["productId_" + warehouseIds[i]].Split(',');
          var quantities = formCollection["quantity_" + warehouseIds[i]].Split(',');
          var prices = formCollection["price_" + warehouseIds[i]].Split(',');
          for (var j = 1; j < productIds.Length; j++)
          {
            var product = pDao.Product(entities, int.Parse(productIds[j]));
            var quantity = int.Parse(quantities[i]);
            var price = decimal.Parse(prices[j]);
            var warehouseDetail = new WarehouseDetail
            {
              Product = product,
              Warehouse = whDao.Warehouse(entities, int.Parse(warehouseIds[i])),
              Quantity = quantity
            };

            var importBillDetail = new ImportBillDetail
            {
              Product = product,
              Price = price,
              Quantity = quantity,
              ImportBill = importBill
            };

            if (!ModelState.IsValid) continue;
            product.Price = price;
            product.Inventory += quantity;
            entities.Products.Attach(product);
            entities.Entry(product).State = EntityState.Modified;
            entities.WarehouseDetails.Add(warehouseDetail);
            entities.ImportBillDetails.Add(importBillDetail);
            entities.SaveChanges();
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }


      return new EmptyResult();
    }

    public ActionResult Details(int id)
    {
      List<ImportBillDetail> details;
      try
      {
        var pDao = new ProductDao();
        var importBillDetails = entities.ImportBillDetails.ToList();
        details = importBillDetails.Where(ibd => ibd.ImportBillId.Equals(id)).ToList();
        foreach (var detail in details)
        {
          detail.Product = pDao.Product(entities, detail.ProductId.Value);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
      return Json(details, JsonRequestBehavior.AllowGet);
    }
  }
}