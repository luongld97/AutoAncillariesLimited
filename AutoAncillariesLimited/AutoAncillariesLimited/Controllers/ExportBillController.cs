using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;
using AutoAncillariesLimited.Models.Dao;
using AutoAncillariesLimited.Models.View_Models;
using Newtonsoft.Json;

namespace AutoAncillariesLimited.Controllers
{
    public class ExportBillController : Controller
    {
        private AALEntities entities = new AALEntities();
        // GET: ExportBill

        [HttpGet]
        public ActionResult ExportBillManagement()
        {
            ViewBag.ExportBills = entities.ExportBills.ToList();
            ViewBag.Products = entities.Products.ToList();
            ViewBag.Customers = entities.Customers.ToList();
            ViewBag.Employees = entities.Employees.Where(n => n.RoleId == 1).ToList();
            ViewBag.Warehouses = entities.Warehouses.ToList();
            var username = Session["username"];
            Employee employee = entities.Employees.SingleOrDefault(n => n.Username == username);
            ViewBag.Fullname = employee.Name;
            ViewBag.Id = employee.Id;
            return View();
        }

        [HttpPost]
        public ActionResult ExportBillManagement(FormCollection collection)
        {
            var warehouseIds = collection.Get("warehouseId").Split(',');
            var employeeCreateIds = collection.Get("employeeCreateId").Split(',');
            var employeeOutIds = collection.Get("employeeOutId").Split(',');
            var customerIds = collection.Get("customerId").Split(',');

            var employeeCreateId = employeeCreateIds[0];
            var employeeOutId = employeeOutIds[0];
            var customerId = customerIds[0];

            ExportBill exportBill = new ExportBill();
            exportBill.CreateDate = DateTime.Now;
            exportBill.EmployeeId = Convert.ToInt32(employeeCreateId);
            exportBill.CustomerId = Convert.ToInt32(customerId);
            entities.ExportBills.Add(exportBill);

            if (warehouseIds.Length == 1)
            {
                return Content("Please! check warehouse, product and quanity before create export bill");
            }

            for (int i = 1; i < warehouseIds.Length; i++)
            {
                var productIds = collection.Get("productId_" + warehouseIds[i]).Split(',');
                var quantities = collection.Get("quantity_" + warehouseIds[i]).Split(',');
                var warehouseId = Convert.ToInt32(warehouseIds[i]);
                if (warehouseId == 0 || productIds.Length == 1)
                {
                    return Content("Please! check warehouse, product and quanity before create export bill");
                }
                for (int j = 1; j < productIds.Length; j++)
                {
                    var productId = Convert.ToInt32(productIds[j]);
                    var quantity = Convert.ToInt32(quantities[j]);

                    if (quantity == 0)
                    {
                        return Content("Quantity isn't equal 0");
                    }

                    Product product = entities.Products.SingleOrDefault(n => n.Id == productId);
                    product.Inventory -= quantity;
                    entities.Products.Attach(product);
                    entities.Entry(product).State = EntityState.Modified;
          
                    WarehouseDetail warehouseDetail =
                        entities.WarehouseDetails.SingleOrDefault(
                            n => n.ProductId == productId && n.WarehouseId == warehouseId);
                    warehouseDetail.Quantity -= quantity;
                    entities.WarehouseDetails.Attach(warehouseDetail);
                    entities.Entry(warehouseDetail).State = EntityState.Modified;

                    Warehouse warehouse = entities.Warehouses.SingleOrDefault(n => n.Id == warehouseId);

                    if (warehouseDetail.Quantity < quantity)
                    {
                        return Content(warehouseDetail.Product.Name + " in " + warehouse.Name +" don't enough quantity.");
                    }

                    ExportBillDetail exportBillDetail = new ExportBillDetail();
                    exportBillDetail.ProductId = product.Id;
                    exportBillDetail.ExportBillId = exportBill.Id;
                    exportBillDetail.Quantity = quantity;
                    exportBillDetail.Price = product.Price;
                    entities.ExportBillDetails.Add(exportBillDetail);
                }
            }
            entities.SaveChanges();
            TempData["error-create-bill"] = "Good job";
            return JavaScript("window.location ='/ExportBill/ExportBillManagement'");
        }


        //load data table export bill
        [HttpGet]
        public ActionResult ExportBills()
            => Json(entities.ExportBills.ToList(), JsonRequestBehavior.AllowGet);

        //get product in warehouse
        public ActionResult ProductsInWarehouse(int warehouseId)
        {
            var warehouses = new WarehouseDetailDao().WarehouseDetails(entities, warehouseId);
            List<ProuductInWarehouseViewModel> products = new List<ProuductInWarehouseViewModel>();
            foreach (var item in warehouses)
            {
                Product product = entities.Products.SingleOrDefault(n => n.Id == item.ProductId);
                products.Add(new ProuductInWarehouseViewModel { productId = product.Id, productName = product.Name });
            }
            var json = JsonConvert.SerializeObject(products);
            return Content(json, "application/json");
        }

        //get max product quantity in warehouse
        public ActionResult getMaxQuantity(int warehouseId, int productId)
        {
            var warehouses = new WarehouseDetailDao().MaxQuantityDetails(entities, warehouseId, productId);
            return Content(warehouses.Quantity.ToString(), "application/json");
        }
    }
    public class JavaScriptResult : ContentResult
    {
        public JavaScriptResult(string script)
        {
            this.Content = script;
            this.ContentType = "application/javascript";
        }
    }
}