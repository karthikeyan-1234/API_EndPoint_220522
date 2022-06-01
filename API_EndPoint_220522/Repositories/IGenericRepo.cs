using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Repositories
{
    public interface IGenericRepo<T>
    {
        Task<T> AddAsync(T obj);
        T Delete(T obj);
        Task SaveChangesAsync();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindAsync(object obj);
        IEnumerable<T> FindBy(Func<T, bool> predicate);
        T Update(T obj);
    }
}