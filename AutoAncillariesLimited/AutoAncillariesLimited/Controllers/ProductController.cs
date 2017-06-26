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

    public ActionResult Products()
    {
      var productModels = entities.Products.Select(product => new ProductModel
      {
        Id = product.Id,
        Name = product.Name,
        Inventory = product.Inventory.Value,
        Price = product.Price.Value,
        Description = product.Description,
        CategoryId = product.CategoryId.Value,
        CategoryName = product.Category.Name
      });
      return Json(productModels, JsonRequestBehavior.AllowGet);
    }

    public ActionResult ProductInsert(ProductViewModel productViewModel)
    {
      if (!ModelState.IsValid) return new EmptyResult();
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
    public ActionResult ProductUpdate(ProductViewModel productViewModel)
    {
      var product = productViewModel.Product;
      var updatedProduct = entities.Products.Find(product.Id);
      if (!ModelState.IsValid) return new EmptyResult();
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
      return new EmptyResult();
    }

    public ActionResult ProductDelete(int id)
    {
      try
      {
        var product = entities.Products.Find(id);
        entities.Products.Remove(product);
        entities.SaveChanges();
      }
      catch (Exception)
      {
        // ignored
      }
      return new EmptyResult();
    }
  }
}