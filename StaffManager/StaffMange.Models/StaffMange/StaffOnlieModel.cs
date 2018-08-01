using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffMange.Models.StaffMange
{
    public class StaffOnlieModel
    {
        public string user { get; set; }

        public string line { get; set; }

        public string shift { get; set; }

        public int status { get; set; } = 0;

        public string workdate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
    }
}
