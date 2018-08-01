using StaffManager.IServices.StaffManage;
using StaffManager.Services.StaffManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StaffManager.Controllers
{
    public class StaffController : Controller
    {
        private IStaffService _service;
        public StaffController(IStaffService  service)
        {
            _service = service;

        }
        public ActionResult StaffOnlieManage()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }

    }
}