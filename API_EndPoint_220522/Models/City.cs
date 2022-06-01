using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Models
{
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public string pincode { get; set; }

        public ICollection<Employee> Employee_Objs { get; set; }
    }
}
