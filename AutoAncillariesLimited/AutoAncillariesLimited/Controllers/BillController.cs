using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;

namespace AutoAncillariesLimited.Controllers
{
    public class BillController : Controller

    {
      private readonly AALEntities entities = new AALEntities
      {
        Configuration =
        {
          ProxyCreationEnabled = false
        }
      };
    // GET: Bill
    public ActionResult BillManagement()
        {
            return View();
        }

      public ActionResult Bills() => Json(entities.Bills.ToList(), JsonRequestBehavior.AllowGet);
    }
}