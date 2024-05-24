using EmployeeIncomeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeIncomeManagement.Data
{
    public class EmployeeDBContext:DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options):base(options)
        { }
        public DbSet<Employee> Employees { get; set; }
    }
}
