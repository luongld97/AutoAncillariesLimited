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
    private readonly AALEntities entities = new AALEntities();
    // GET: ImportBill
    public ActionResult ImportBillManagement()
    {
      var importBills = entities.ImportBills;
      var importBillModels =
        (from importBill in importBills
         let details = importBill.ImportBillDetails
         let totalPrice = (decimal?)details.Sum(importBillDetail => importBillDetail.Price * importBillDetail.Quantity)
         select new ImportBillModel
         {
           Id = importBill.Id,
           CreateDate = importBill.CreateDate.Value,
           Employee = importBill.Employee.Name,
           Supplier = importBill.Supplier.Name,
           TotalPrice = totalPrice ?? 0
         }).ToList();

      ViewBag.ImportBills = importBillModels;
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
        var wDao = new WarehouseDao();
        var wdDao = new WarehouseDetailDao();
        var sDao = new SupplierDao();
        var eDao = new EmployeeDao();
        // Kh?i t?o 1 hóa don nh?p
        var importBill = new ImportBill
        {
          CreateDate = DateTime.Now,
          Supplier = sDao.Supplier(entities, supplierId),
          Employee = eDao.Employee(entities, int.Parse(Session["employee"].ToString())),
          Status = true
        };
        if (ModelState.IsValid)
        { // Thêm hóa don vào danh sách hóa don
          entities.ImportBills.Add(importBill);
          for (var i = 1; i < warehouseIds.Length; i++)
          {
            var warehouseId = int.Parse(warehouseIds[i]);
            if (warehouseId == -1)
            {
              return Content("Please select warehouse!");
            }
            // L?y ra danh sách s?n ph?m và thông tin kèm theo tuong ?ng v?i m?i kho
            var productIds = formCollection["productId_" + warehouseIds[i]].Split(',');
            var quantities = formCollection["quantity_" + warehouseIds[i]].Split(',');
            if (productIds.Length == 1)
            {
              return Content("Please enter product!");
            }
            for (var j = 1; j < productIds.Length; j++)
            { // C?p nh?t thông tin s?n ph?m trong danh sách các s?n ph?m
              var product = pDao.Product(entities, warehouseId);
              var quantity = int.Parse(quantities[i]);
              // T?o chi ti?t hóa don tuong ?ng v?i m?i s?n ph?m
              var importBillDetail = new ImportBillDetail
              {
                Product = product,
                Price = product.Price.Value,
                Quantity = quantity,
                ImportBill = importBill
              };
              if (!ModelState.IsValid) continue;
              entities.ImportBillDetails.Add(importBillDetail);
              //
              var productInWarehouse = wdDao.WarehouseDetail(entities, int.Parse(warehouseIds[i]), product.Id);
              if (productInWarehouse != null)
              {
                productInWarehouse.Quantity += quantity;
                if (ModelState.IsValid) continue;
                entities.WarehouseDetails.Attach(productInWarehouse);
                entities.Entry(productInWarehouse).State = EntityState.Modified;
              }
              else
              {
                // T?o chi ti?t kho tuong ?ng v?i m?i s?n ph?m trong kho du?c ch?n
                var warehouseDetail = new WarehouseDetail
                {
                  Product = product,
                  Warehouse = wDao.Warehouse(entities, int.Parse(warehouseIds[i])),
                  Quantity = quantity
                };
                if (ModelState.IsValid) continue;
                entities.WarehouseDetails.Add(warehouseDetail);
              }

              // Ki?m tra d? li?u h?p l? và thêm vào danh sách chi ti?t hóa don, chi ti?t kho
              if (!ModelState.IsValid) continue;
              product.Inventory += quantity;
              entities.Products.Attach(product);
              entities.Entry(product).State = EntityState.Modified;
            }
          }
        }
        // Luu l?i các thay d?i và d?y lên CSDL
        entities.SaveChanges();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
      return RedirectToAction("ImportBillManagement");
    }
    //    public ActionResult Details(int id)
    //    {
    //      List<ImportBillDetail> details;
    //      try
    //      {
    //        var pDao = new ProductDao();
    //        var importBillDetails = entities.ImportBillDetails.ToList();
    //        details = importBillDetails.Where(ibd => ibd.ImportBillId.Equals(id)).ToList();
    //        foreach (var detail in details)
    //        {
    //          detail.Product = pDao.Product(entities, detail.ProductId.Value);
    //        }
    //      }
    //      catch (Exception e)
    //      {
    //        Console.WriteLine(e);
    //        throw;
    //      }
    //      return Json(details, JsonRequestBehavior.AllowGet);
    //    }

    public ActionResult ImportBills()
    {
      /*
      var importBills = entities.ImportBills;
      var importBillModels = new List<ImportBillModel>();
      foreach (var importBill in importBills)
      {
        var details = importBill.ImportBillDetails;
        var totalPrice = details.Sum(importBillDetail
          => importBillDetail.Price * importBillDetail.Quantity);
        var importBillModel = new ImportBillModel
        {
          Id = importBill.Id,
          CreateDate = importBill.CreateDate.Value,
          Employee = importBill.Employee.Name,
          Supplier = importBill.Supplier.Name,
          TotalPrice = totalPrice
        };
        importBillModels.Add(importBillModel);
      }
      */

      var importBills = entities.ImportBills;
      var importBillModels =
      from importBill in importBills
      let details = importBill.ImportBillDetails
      let totalPrice = (decimal?)details.Sum(importBillDetail => importBillDetail.Price * importBillDetail.Quantity)
      select new ImportBillModel
      {
        Id = importBill.Id,
        CreateDate = importBill.CreateDate.Value,
        Employee = importBill.Employee.Name,
        Supplier = importBill.Supplier.Name,
        TotalPrice = totalPrice.Value
      };
      return Json(importBillModels, JsonRequestBehavior.AllowGet);
    }
  }
}