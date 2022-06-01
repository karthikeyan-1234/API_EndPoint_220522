using API_EndPoint_220522.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Contexts
{
    public class EmployeeDBContext : DbContext, IEmployeeDBContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<City> Cities { get; set; }

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var Employees = modelBuilder.Entity<Employee>();
            var Cities = modelBuilder.Entity<City>();

            Employees.Property(e => e.id).ValueGeneratedOnAdd();
            Employees.HasIndex(e => e.id).IsUnique();

            Cities.Property(c => c.id).ValueGeneratedOnAdd();
            Cities.HasIndex(c => c.id).IsUnique();

            Employees.HasOne(e => e.City_Obj).WithMany(e => e.Employee_Objs).HasForeignKey(e => e.city_id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
