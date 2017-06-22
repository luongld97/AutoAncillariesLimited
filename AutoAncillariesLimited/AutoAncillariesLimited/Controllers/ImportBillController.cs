using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;
using AutoAncillariesLimited.Models.Dao;
using AutoAncillariesLimited.Models.View_Models;
using Newtonsoft.Json;

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

    public ActionResult ImportBillInsert(string bill)
    { // get data from client
      var importBillViewModel = JsonConvert.DeserializeObject<ImportBillViewModel>(bill);
      var type = importBillViewModel.GetType().ToString();
      // insert data to database
      var ibdDao = new ImportBillDetailDao();
      var importBill = importBillViewModel.ImportBill;
      var importBillDetails = importBillViewModel.Details;
      var warehouseId = importBillViewModel.WarehouseId;
      importBill.EmployeeId = int.Parse(Session["employee"].ToString());
      importBill.CreateDate = DateTime.Now;
      if (ModelState.IsValid)
      {
        try
        {
          entities.ImportBills.Add(importBill);
          entities.SaveChanges();
          ibdDao.ImportBillDetailInsert(importBill, importBillDetails, warehouseId);
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
          throw;
        }
      }

      return Json(true);
    }

    public ActionResult Details(int id)
    {
      List<ImportBillDetail> details;
      try
      {
        var pDao = new ProductDao();
        var importBillDetails = entities.ImportBillDetails.ToList();
        details = importBillDetails.Where(ibd => ibd.ImportBillId.Equals(id)).ToList();
        foreach (var detail in details)
        {
          detail.Product = pDao.Product(detail.ProductId.Value);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
      return Json(details, JsonRequestBehavior.AllowGet);
    }
  }
}