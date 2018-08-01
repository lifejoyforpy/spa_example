using StaffManager.IServices.StaffManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StaffManager.Controllers
{
    public class HomeController : Controller
    {
    //    private IStaffService _service;
    //    public HomeController(IStaffService service)
    //    {
    //        _service = service;

    //    }
        public ActionResult Index()
        {
           
            return Redirect("~/dist/index.html");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}