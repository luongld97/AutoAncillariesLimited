using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;

namespace AutoAncillariesLimited.Controllers
{
  public class CategoryController : Controller
  {
    private readonly AALEntities entities = new AALEntities
    {
      Configuration =
          {
            ProxyCreationEnabled = false
          }
    };
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

  }
}