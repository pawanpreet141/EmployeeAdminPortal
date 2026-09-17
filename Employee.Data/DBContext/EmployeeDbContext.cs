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

            // TblUsers table
            modelBuilder.Entity<User>()
                .ToTable("TblUsers");

            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);


            // Employees table
            modelBuilder.Entity<Employee1>().ToTable("Employees");
            
            modelBuilder.Entity<Employee1>().HasKey(x => x.Id);

             
            // One User -> Many Employees
            modelBuilder.Entity<Employee1>()
                .HasOne(x => x.User)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}






