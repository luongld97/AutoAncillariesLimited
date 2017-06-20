using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;
using AutoAncillariesLimited.Models.View_Models;

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
      var categories = entities.Categories.Select(category => new SelectListItem
      {
        Value = category.Id.ToString(),
        Text = category.Name
      }).ToList();
      var productViewModel = new ProductViewModel
      {
        Categories = categories
      };
      return View(productViewModel);
    }

    // Fill data from database to list products table

    public ActionResult Products() => Json(entities.Products.ToList(), JsonRequestBehavior.AllowGet);

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
      try
      {
        entities.Products.Attach(productViewModel.Product);
        entities.Entry(productViewModel.Product).State = EntityState.Modified;
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