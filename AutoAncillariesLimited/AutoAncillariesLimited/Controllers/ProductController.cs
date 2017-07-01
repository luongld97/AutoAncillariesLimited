using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoAncillariesLimited.Models;
using AutoAncillariesLimited.Models.Dao;
using AutoAncillariesLimited.Models.View_Models;
using Newtonsoft.Json;
using static System.String;

namespace AutoAncillariesLimited.Controllers
{
  public class ProductController : Controller
  {
    private readonly AALEntities entities = new AALEntities();

    public ActionResult ProductInsertForm()
    {
      var categories = entities.Categories.Select(category => new SelectListItem
      {
        Value = category.Id.ToString(),
        Text = category.Name
      }).ToList();
      var productViewModel = new ProductViewModel
      {
        Categories = categories
      };
      return PartialView("_ProductInsert", productViewModel);
    }

    public ActionResult ProductUpdateForm(int id)
    {
      var product = entities.Products.Find(id);
      var categories = entities.Categories.Select(category => new SelectListItem
      {
        Value = category.Id.ToString(),
        Text = category.Name
      }).ToList();
      var productViewModel = new ProductViewModel
      {
        Product = product,
        Categories = categories
      };
      return PartialView("_ProductUpdate", productViewModel);
    }
    // GET: Product
    public ActionResult ProductManagement()
    {
      ViewBag.Warehouses = entities.Warehouses.ToList();
      ViewBag.Categories = entities.Categories.ToList();
      ViewBag.Suppliers = entities.Suppliers.ToList();
      ViewBag.Products = entities.Products.ToList();
      return View();
    }

    // Fill data from database to list products table

    public ActionResult Products(bool status = true)
    {
//      var productModels = entities.Products.Select(product => new ProductModel
//      {
//        Id = product.Id,
//        Name = product.Name,
//        Inventory = product.Inventory,
//        Price = product.Price.Value,
//        Description = product.Description,
//        CategoryId = product.CategoryId.Value,
//        CategoryName = product.Category.Name
//      });
      var products = (from product in entities.Products
        where product.Status.Value == status
        select new ProductModel
        {
          Id = product.Id,
          Name = product.Name,
          Price = product.Price.Value,
          Inventory = product.Inventory.Value,
          CategoryId = product.CategoryId.Value,
          Description = product.Description,
          CategoryName = product.Category.Name
        }).ToList();
      return Json(products, JsonRequestBehavior.AllowGet);
    }

    public ActionResult ProductInsert(ProductViewModel productViewModel)
    {
      if (!ModelState.IsValid) return new EmptyResult();
      var pDao = new ProductDao();
      if (pDao.IsExist(productViewModel.Product, entities.Products)) return Content("This product name is exist!");
      try
      {
        entities.Products.Add(productViewModel.Product);
        entities.SaveChanges();
      }
      catch (Exception)
      {
        // ignored
      }
      return new EmptyResult();
    }
    public ActionResult ProductUpdate(ProductViewModel productViewModel, string returnUrl)
    { var pDao = new ProductDao();
      var product = productViewModel.Product;
      var updatedProduct = entities.Products.Find(product.Id);
      if (!ModelState.IsValid) return PartialView("_ProductUpdate");
      if (pDao.IsExist(product, entities.Products)) return PartialView("_ProductUpdate");
      if (updatedProduct == null) return new EmptyResult();
      try
      {

        updatedProduct.CategoryId = product.CategoryId;
        updatedProduct.Description = product.Description;
        updatedProduct.Name = product.Name;
        updatedProduct.Price = product.Price;

        entities.Products.Attach(updatedProduct);
        entities.Entry(updatedProduct).State = EntityState.Modified;
        entities.SaveChanges();

      }
      catch (Exception)
      {
        // ignored
      }
      return JavaScript("window.location = '" + returnUrl + "'");
    }

    public ActionResult ProductDelete(int id)
    {
      
      return new EmptyResult();
    }

    public ActionResult ProductPrice(int id)
    {
      var product = entities.Products.SingleOrDefault(pro => pro.Id.Equals(id));
      return Json(product?.Price, JsonRequestBehavior.AllowGet);
    }
  }
}