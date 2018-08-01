using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffMange.Models.StaffMange
{
    public class OffWorkDetail
    {
       public string emp_no { get; set; }
        public string work_date { get; set; }
        public int ligang_ci { get; set; }
        public DateTime ligang_time { get; set; }
        public DateTime huigang_time { get; set; }
        public double ligang_time_length { get; set; }
        public string shift_no { get; set; }

    }
}
