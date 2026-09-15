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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("TblUsers");

            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
        }
    }
}






