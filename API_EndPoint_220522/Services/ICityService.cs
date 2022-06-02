using API_EndPoint_220522.Models;
using API_EndPoint_220522.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Services
{
    public interface ICityService
    {
        public Task<CityDTO> AddCityAsync(CityDTO newEmp);
        public Task<IEnumerable<CityDTO>> GetAllCitiesAsync();
        public Task<CityDTO> DeleteCityAsync(CityDTO city);
        public Task<CityDTO> UpdateCityAsync(CityDTO city);
    }
}