using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;
using AutoAncillariesLimited.Models.Dao;
using AutoAncillariesLimited.Models.View_Models;

namespace AutoAncillariesLimited.Controllers
{
  public class SupplierController : Controller
  {
    private readonly AALEntities entities = new AALEntities();
    public ActionResult SupplierManagement()
    {
      return View();
    }

    public ActionResult Suppliers() => Json(entities.Suppliers.ToList(), JsonRequestBehavior.AllowGet);

    public ActionResult ProductsOfSupplier(int id)
    {
      List<Product> products;
      if (!id.Equals(-1))
      {
        var sDao = new SupplierDao();
        //        var ibdDao = new ImportBillDetailDao();
        var importBills = sDao.Supplier(entities, id).ImportBills;
        products = new List<Product>();
        foreach (var importBill in importBills)
        {
          var importBillDetails = importBill.ImportBillDetails;
          foreach (var importBillDetail in importBillDetails)
          {
            var product = importBillDetail.Product;
            if (products.SingleOrDefault(pro => pro.Id.Equals(product.Id)) == null)
              products.Add(product);
          }
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
