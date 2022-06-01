using API_EndPoint_220522.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private IEmployeeDBContext db;
        private DbSet<T> table;

        public GenericRepo(IEmployeeDBContext db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public async Task<T> AddAsync(T obj)
        {
            await table.AddAsync(obj);
            return obj;
        }

        public T Delete(T obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            return obj;
        }

        public T Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            return obj;
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> FindAsync(object obj)
        {
            return await table.FindAsync(obj);
        }

        public IEnumerable<T> FindBy(Func<T,bool> predicate)
        {
            return table.Where(predicate);
        }
    }
}
