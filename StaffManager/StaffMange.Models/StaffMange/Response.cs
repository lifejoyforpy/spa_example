using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffMange.Models.StaffMange
{
    public class Response<T> 
    {
        public int flag { get; set; } = 1;
        public T data { get; set; } =default(T);
        public string msg { get; set; }
    }
}
