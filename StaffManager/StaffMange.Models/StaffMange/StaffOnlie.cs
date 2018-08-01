using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffMange.Models.StaffMange
{
    public class StaffOnlie
    {
        public int id { get; set; }
        public string work_date { get; set; }
        public string empNo { get; set; }
        public string empName { get; set; }
        public string line { get; set; }
        public string Status_flag { get; set; }
        public DateTime online_time { get; set; }
        public DateTime offline_time { get; set; }
        public string offline_Mark { get; set; }
        public string shift_no { get; set; }
        public DateTime ligang_time { get; set; }
        public DateTime huigang_time { get; set; }

        public string ligang_mark { get; set; }

        public string old_line { get; set; }
        public string new_line { get; set; }
        public DateTime new_line_time { get; set; }


    }
}
