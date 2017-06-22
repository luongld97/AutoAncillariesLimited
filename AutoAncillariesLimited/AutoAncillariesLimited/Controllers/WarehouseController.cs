using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;

namespace AutoAncillariesLimited.Controllers
{
    public class WarehouseController : Controller
    {
      private readonly AALEntities entities = new AALEntities
      {
        Configuration =
        {
          ProxyCreationEnabled = false
        }
      };
    // GET: Warehouse
    public ActionResult Warehouses => Json(entities.Warehouses.ToList(), JsonRequestBehavior.AllowGet);
    }
}