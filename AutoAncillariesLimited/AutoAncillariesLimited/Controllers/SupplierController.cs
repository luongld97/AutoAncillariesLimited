using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;

namespace AutoAncillariesLimited.Controllers
{
  public class SupplierController : Controller
  {
    private readonly AALEntities entities = new AALEntities
    {
      Configuration =
      {
        ProxyCreationEnabled = false
      }
    };
    public ActionResult SupplierManagement()
    {
      return View();
    }

    public ActionResult Suppliers() => Json(entities.Suppliers.ToList(), JsonRequestBehavior.AllowGet);
  }
}
