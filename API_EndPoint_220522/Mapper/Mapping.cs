using API_EndPoint_220522.Models;
using API_EndPoint_220522.Models.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<Employee, EmployeeDTO>();

            CreateMap<CityDTO, City>();
            CreateMap<City, CityDTO>();
        }
    }
}
