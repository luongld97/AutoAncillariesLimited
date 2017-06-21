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
  public class ImportBillController : Controller
  {
    private readonly AALEntities entities = new AALEntities
    {
      Configuration =
        {
          ProxyCreationEnabled = false
        }
    };
    // GET: ImportBill
    public ActionResult ImportBillManagement()
    {
      ViewBag.ImportBills = entities.ImportBills.ToList();
      return View();
    }

    public ActionResult ImportBillInsertForm() => PartialView("_ImportBillInsert");
    public ActionResult ImportBillInsert(ImportBillViewModel viewModel)
    {
      if (!ModelState.IsValid) return new EmptyResult();
      try
      {
        var dao = new ImportBillDetailDao();
        var importBill = viewModel.ImportBill;
        entities.ImportBills.Add(importBill);

      }
      catch (Exception)
      {
        // ignored
      }
      return new EmptyResult();
    }
  }
}