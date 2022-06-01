using API_EndPoint_220522.Models;
using API_EndPoint_220522.Models.DTOs;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> AddEmployeeAsync(EmployeeDTO newEmp);
    }
}