using System;
using System.Collections;
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

    public ActionResult getnumber()
    {
      var importBill = new ImportBill();
      var jsonResult = System.Web.HttpContext.Current.Request.Form["bill"];
      var importBillViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ImportBillViewModel>(jsonResult);
      try
      {
        importBillViewModel.ImportBill.CreateDate = DateTime.Now;
        entities.ImportBills.Add(importBillViewModel.ImportBill);
        entities.SaveChanges();
        foreach (var importBillDetail in importBillViewModel.Details)
        {
          importBillDetail.ImportBillId = importBillViewModel.ImportBill.Id;
          entities.ImportBillDetails.Add(importBillDetail);
        }
        entities.SaveChanges();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
      
      return Json(true);
    }
  }
}