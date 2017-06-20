using System.Linq;
using System.Web.Mvc;
using AutoAncillariesLimited.Models;

namespace AutoAncillariesLimited.Controllers
{
  public class CustomerController : Controller
  {
    private readonly AALEntities entities = new AALEntities
    {
      Configuration =
      {
        ProxyCreationEnabled = false
      }
    };
    public ActionResult CustomerManagement()
    {
      return View();
    }

    public ActionResult Customers() => Json(entities.Customers.ToList(), JsonRequestBehavior.AllowGet);
  }
}