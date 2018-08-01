using StaffMange.Models.StaffMange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager.IServices.StaffManage
{
    public interface IStaffService
    {
         int test();
        Response<List<StaffWorkInfo>> getOnlieInfo(StaffOnlieModel model);
        Response<bool> goWork(List<StaffWorkInfo> model);

        Response<bool> transWork(List<StaffWorkInfo> model);

        Response<List<string>> getLine();

        string getUserLine(string user);
        Response<bool> offpost(List<StaffWorkInfo> model);

        Response<bool> returnpost(List<StaffWorkInfo> model);

        Response<bool> offwork(List<StaffWorkInfo> model);

        Response<List<StaffWorkInfo>> getpostinfo(StaffOnlieModel model);
        Response<List<string>> getoffreasonlist();
        Response<bool> tranpost(List<StaffWorkInfo> model);

        Response<User> login(User user);
    }
}
