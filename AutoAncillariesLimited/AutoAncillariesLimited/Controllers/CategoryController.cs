using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;
using AutoAncillariesLimited.Models.Dao;
using AutoAncillariesLimited.Models.View_Models;

namespace AutoAncillariesLimited.Controllers
{
  public class CategoryController : Controller
  {
    private readonly AALEntities entities = new AALEntities();
    // GET: CategoryController
    public ActionResult CategoryManagement()
    {
      return View();
    }

    // Fill data to categories list table
    [HttpGet]
    public ActionResult Categories() 
      => Json(entities.Categories.ToList(), JsonRequestBehavior.AllowGet);
    // Insert category to database
    [HttpPost]
    public ActionResult CategoryInsert(Category category)
    {
      if (ModelState.IsValid)
      {
        try
        {
          entities.Categories.Add(category);
          entities.SaveChanges();
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
          throw;
        }
      }
      return new EmptyResult();
    }
    // Delete category from database
    [HttpPost]
    public ActionResult CategoryDelete(int id)
    {
      try
      {

      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
      return new EmptyResult();
    }

    public ActionResult ProductsInCategory(int id)
    {
      IEnumerable<Product> products;
      if (!id.Equals(-1))
      {
        var cDao = new CategoryDao();
        products = cDao.Category(entities, id).Products;
      }
      else products = entities.Products;
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