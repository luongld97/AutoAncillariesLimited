using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;

namespace AutoAncillariesLimited.Controllers
{
    
    public class HomeController : Controller
    {
        private AALEntities entity=new AALEntities
        {
          Configuration =
          {
            ProxyCreationEnabled = false
          }
        };
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult UserMenuPartial()
        {
            //get object employee from session
            var username = Session["username"];
            if (username == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Employee employee = entity.Employees.SingleOrDefault(n => n.Username == username);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return PartialView(employee);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection formCollection)
        {
            //get value username and password from form login
            var username = formCollection.Get("Username");
            var password = formCollection.Get("Password");

            //check resutl from 2 value username and password
            var result = entity.Employees.Count(n => (n.Username == username && n.Password == password));

            //check condition
            if (result > 0)
            {
                var employee = entity.Employees.SingleOrDefault(n => n.Username == username);

                //check status of object
                if (employee.Status.Value)
                {
                    Session["username"] = username;
                    return JavaScript("window.location = '" + Url.Action("Index", "Home") + "'");
                }
            }
            return Content("Invalid username or password");
        }

        //log out employee 
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login", "Home");
        }
    }
}