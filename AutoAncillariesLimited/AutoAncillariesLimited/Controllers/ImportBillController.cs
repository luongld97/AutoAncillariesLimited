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
          TotalPrice = totalPrice.Value
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
        var whDao = new WarehouseDao();
        var sDao = new SupplierDao();
        var eDao = new EmployeeDao();
        // Khởi tạo 1 hóa đơn nhập
        var importBill = new ImportBill
        {
          CreateDate = DateTime.Now,
          Supplier = sDao.Supplier(entities, supplierId),
          Employee = eDao.Employee(entities, int.Parse(Session["employee"].ToString())),
          Status = true
        };
        if (ModelState.IsValid)
        { // Thêm hóa đơn vào danh sách hóa đơn
          entities.ImportBills.Add(importBill);
          for (var i = 1; i < warehouseIds.Length; i++)
          { // Lấy ra danh sách sản phẩm và thông tin kèm theo tương ứng với mỗi kho
            var productIds = formCollection["productId_" + warehouseIds[i]].Split(',');
            var quantities = formCollection["quantity_" + warehouseIds[i]].Split(',');
            var prices = formCollection["price_" + warehouseIds[i]].Split(',');
            for (var j = 1; j < productIds.Length; j++)
            { // Cập nhật thông tin sản phẩm trong danh sách các sản phẩm
              var product = pDao.Product(entities, int.Parse(productIds[j]));
              var quantity = int.Parse(quantities[i]);
              var price = decimal.Parse(prices[j]);
              // Tạo chi tiết kho tương ứng với mỗi sản phẩm trong kho được chọn
              var warehouseDetail = new WarehouseDetail
              {
                Product = product,
                Warehouse = whDao.Warehouse(entities, int.Parse(warehouseIds[i])),
                Quantity = quantity
              };
              // Tạo chi tiết hóa đơn tương ứng với mỗi sản phẩm
              var importBillDetail = new ImportBillDetail
              {
                Product = product,
                Price = price,
                Quantity = quantity,
                ImportBill = importBill
              };
              // Kiểm tra dữ liệu hợp lệ và thêm vào danh sách chi tiết hóa đơn, chi tiết kho
              if (!ModelState.IsValid) continue;
              product.Inventory += quantity;
              entities.Products.Attach(product);
              entities.Entry(product).State = EntityState.Modified;
              entities.WarehouseDetails.Add(warehouseDetail);
              entities.ImportBillDetails.Add(importBillDetail);
            }
          }
        }
        // Lưu lại các thay đổi và đẩy lên CSDL
        entities.SaveChanges();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
      return RedirectToAction("ImportBillManagement");
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

    public ActionResult ImportBills()
    {
      
//      var importBills = entities.ImportBills;
//      var importBillModels = new List<ImportBillModel>();
//      foreach (var importBill in importBills)
//      {
//        var details = importBill.ImportBillDetails;
//        var totalPrice = details.Sum(importBillDetail
//          => importBillDetail.Price * importBillDetail.Quantity);
//        var importBillModel = new ImportBillModel
//        {
//          Id = importBill.Id,
//          CreateDate = importBill.CreateDate.Value,
//          Employee = importBill.Employee.Name,
//          Supplier = importBill.Supplier.Name,
//          TotalPrice = totalPrice
//        };
//        importBillModels.Add(importBillModel);
//      }
      

      var importBills = entities.ImportBills;
      var importBillModels =
      from importBill in importBills
        let details = importBill.ImportBillDetails
        let totalPrice = (decimal?) details.Sum(importBillDetail =>  importBillDetail.Price * importBillDetail.Quantity)
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