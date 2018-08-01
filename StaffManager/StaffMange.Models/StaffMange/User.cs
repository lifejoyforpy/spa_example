using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffMange.Models.StaffMange
{
     public   class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserJobNo { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int DataFlag { get; set; } = 1;
        public string Created_By { get; set; }
        public string Created_By_Id { get; set; }
        public string Created_Time { get; set; }
        public string Last_Modify_By { get; set; }
        public string Last_Modify_By_Id { get; set; }
        public string Last_Modify_Time { get; set; }
        public string Factory { get; set; }
        public string Department { get; set; }
    }
}
