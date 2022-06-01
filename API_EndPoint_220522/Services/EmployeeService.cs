using API_EndPoint_220522.Models;
using API_EndPoint_220522.Models.DTOs;
using API_EndPoint_220522.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Services
{
    public class EmployeeService : IEmployeeService
    {
        IGenericRepo<EmployeeDTO> repo;
        ILogger<IEmployeeService> logger;

        public EmployeeService(IGenericRepo<EmployeeDTO> repo, ILogger<IEmployeeService> logger)
        {
            this.repo = repo; this.logger = logger;
        }

        public async Task<EmployeeDTO> AddEmployeeAsync(EmployeeDTO newEmp)
        {
            try
            {
                var res = await repo.AddAsync(newEmp);
                await repo.SaveChangesAsync();
                return res;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, newEmp);
                return null;
            }
        }
    }
}
