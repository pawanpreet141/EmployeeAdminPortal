using Microsoft.EntityFrameworkCore;
using Employee.Data.Models;

namespace Employee.Data.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee1> Employees { get; set; }
    }
}

//dotnet add package Serilog.AspNetCore
//dotnet add package Serilog.Sinks.Console
//dotnet add package Serilog.Sinks.File





