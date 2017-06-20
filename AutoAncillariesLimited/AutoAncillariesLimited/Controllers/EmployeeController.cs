using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;

namespace AutoAncillariesLimited.Controllers
{
    public class EmployeeController : Controller
    {

        private AALEntities entity = new AALEntities
        {
            Configuration =
      {
        ProxyCreationEnabled = false
      }
        };
        // GET: Employee
        public ActionResult EmployeeManagement()
        {
            return View();
        }

        public ActionResult Employees() => Json(entity.Employees.ToList(), JsonRequestBehavior.AllowGet);
        [HttpGet]
        //profile employee
        public ActionResult Profile(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Employee employee = entity.Employees.SingleOrDefault(n => n.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        public ActionResult InputPassword(FormCollection formCollection)
        {
            var password = formCollection.Get("inputPassword");
            var username = formCollection.Get("inputUsername");

            Employee employee = entity.Employees.SingleOrDefault(n => n.Username == username);

            if (password == "" || employee.Password != password)
            {
                return Content("Password don't match");
            }
            return JavaScript("ShowFormChangeProfile();");

        }


        [HttpPost]
        public ActionResult ChangeProfile(Employee employee, string returnUrl)
        {
            //update employee profile
            Employee currentEmployee = entity.Employees.SingleOrDefault(n => n.Id == employee.Id);
            if (ModelState.IsValid)
            {
                currentEmployee.Username = employee.Username;
                currentEmployee.Name = employee.Name;
                currentEmployee.Address = employee.Address;
                currentEmployee.Email = employee.Email;
                currentEmployee.Phone = employee.Phone;
                currentEmployee.Password = employee.Password;
                entity.Employees.Attach(currentEmployee);
                entity.Entry(currentEmployee).State = EntityState.Modified;
                entity.Configuration.ValidateOnSaveEnabled = false;
                entity.SaveChanges();
                TempData["alertChangedProfile"] = "Good job!";
                return JavaScript("window.location ='" + returnUrl + "'");
            }
            return PartialView(currentEmployee);

        }
    }
}