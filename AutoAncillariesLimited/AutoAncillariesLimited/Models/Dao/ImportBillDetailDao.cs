﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models.Dao
{
  public class ImportBillDetailDao
  {
    private readonly AALEntities entities = new AALEntities
    {
      Configuration =
      {
        ProxyCreationEnabled = false
      }
    };

    public bool ImportBillDetailInsert(ImportBill importBill, List<ImportBillDetail> details, int id)
    {
      try
      {
        var wdDao = new WarehouseDetailDao();
        foreach (var importBillDetail in details)
        {
          importBillDetail.ImportBillId = importBill.Id;
          entities.ImportBillDetails.Add(importBillDetail);
          wdDao.ImportWarehouseDetail(id, importBillDetail);
        }
        entities.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        // ignored
      }
      return false;
    }
  }
}