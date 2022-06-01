using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Models.DTOs
{
    public class EmployeeDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public float age { get; set; }
        public int city_id { get; set; }

        public City City_Obj { get; set; }
    }
}
