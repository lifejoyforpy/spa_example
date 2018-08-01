using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffMange.Models.StaffMange
{
    public class StaffInfo
    {
        public string WorkID { get; set; }
        public string WorkerName { get; set; }
        public string FactoryName { get; set; }
        public string Department { get; set; }
        public string Office { get; set; }
        public string PostName { get; set; }
        public string Ascription { get; set; }
        public string IsFixed { get; set; }
        public string IsEnabled { get; set; }
        public int ID { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public string line { get; set; }
        public int cardID { get; set; }
        public string shift_no { get; set; }
    }
}
