using API_EndPoint_220522.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Contexts
{
    public interface IEmployeeDBContext
    {
        DbSet<City> Cities { get; set; }
        DbSet<Employee> Employees { get; set; }
        Task<int> SaveChangesAsync(CancellationToken token = default);
        EntityEntry Entry(object obj);
        DbSet<T> Set<T>() where T : class;
    }
}