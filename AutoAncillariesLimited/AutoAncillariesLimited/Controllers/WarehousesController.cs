using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;

namespace AutoAncillariesLimited.Controllers
{
    public class WarehousesController : Controller
    {
        // GET: Warehouses
        private readonly AALEntities entities = new AALEntities
        {
          Configuration =
          {
            ProxyCreationEnabled = false
          }
        };

      public ActionResult Warehouses() => Json(entities.Warehouses.ToList(), JsonRequestBehavior.AllowGet);
    }
}