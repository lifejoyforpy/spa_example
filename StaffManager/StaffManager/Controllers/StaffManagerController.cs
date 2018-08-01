using StaffManager.IServices.StaffManage;
using StaffManager.Models;
using StaffMange.Models.StaffMange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StaffManager.Controllers
{
    public class StaffManagerController : ApiController
    {   
        private IStaffService _service;
        public StaffManagerController(IStaffService staffService)
        {
            _service = staffService;

        }
        [HttpPost]
        public Response<List<StaffWorkInfo>> getOnlieInfo(StaffOnlieModel model)
        {
            Response<List<StaffWorkInfo>> rsp = new Response<List<StaffWorkInfo>>();
            rsp = _service.getOnlieInfo(model);
            return rsp;
        }
        [HttpPost]
        //上班
        public Response<bool> goWork(List<StaffWorkInfo> model)
        {
            Response<bool>rsp = new Response<bool>();
            rsp=_service.goWork(model);
            return rsp;
        }
        [HttpPost]
        //调岗
        public Response<bool> transWork(List<StaffWorkInfo> model)
        {
            Response<bool> rsp = new Response<bool>();
            _service.transWork(model);
            return rsp;
        }
   
        [HttpPost]
        public int test()
        {
            int j = _service.test();
            return j;

        }
        [HttpPost]
        public Response<List<string>> getLine()
        {
            Response<List<string>> rsp = new Response<List<string>>();
            rsp=_service.getLine();
            return rsp;
        }
        [HttpPost]

       public Response<List<StaffWorkInfo>> getpostinfo(StaffOnlieModel model)
        {


            Response<List<StaffWorkInfo>> rsp = new Response<List<StaffWorkInfo>>();
            rsp = _service.getpostinfo(model);
            return rsp;

        }
        [HttpPost]
        public Response<List<string>> getoffreasonlist()
        {
            Response<List<string>> rsp = new Response<List<string>>();
            rsp = _service.getoffreasonlist();
            return rsp;
        }
        [HttpPost]
        public Response<bool> offpost(List<StaffWorkInfo> model)
        {
            Response<bool> rsp = new Response<bool>();
            rsp = _service.offpost(model);
            return rsp;

        }
        [HttpPost]
        public Response<bool> returnpost(List<StaffWorkInfo> model)
        {
            Response<bool> rsp = new Response<bool>();
            rsp = _service.returnpost(model);
            return rsp;

        }
        [HttpPost]
        public Response<bool> offwork(List<StaffWorkInfo> model)
        {
            Response<bool> rsp = new Response<bool>();
            rsp = _service.offwork(model);
            return rsp;

        }

        [HttpPost]
        public Response<bool> tranpost(List<StaffWorkInfo> model)
        {

            Response<bool> rsp = new Response<bool>();
            rsp = _service.tranpost(model);
            return rsp;

        }
        [HttpPost]
        public Response<User> login(User user)
        {

            Response<User> rsp = new Response<User>();
            rsp = _service.login(user);
            return rsp;
        }
    }  
}
