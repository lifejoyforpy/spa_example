using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffMange.Models.StaffMange
{
    public class StaffWorkInfo
    {
        public string empname { get; set; }
        public string empno { get; set; }

        public string job { get; set; }
        public string shift { get; set; }

        public string line { get; set; }

        public string new_line { get; set; }

        public string status { get; set; }

        public bool check { get; set; } = false;

        public string Status_flag { get; set; }

        public string ligang_mark { get; set; }

        public DateTime ligang_time { get; set; }

        public string source_line { get; set; }

    }
}
