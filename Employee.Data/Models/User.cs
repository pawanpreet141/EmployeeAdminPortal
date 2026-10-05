//namespace Employee.Data.Models
//{
//    public class User
//    {
//        public int Id { get; set; }

//        public string Name { get; set; } = string.Empty;

//        public string Email { get; set; } = string.Empty;

//        public string PasswordHash { get; set; } = string.Empty;
//        public string Department { get; set; } = string.Empty;

//        // One user can have many employees
//        public ICollection<Employee1> Employees { get; set; }
//    = new List<Employee1>();

//    }
//}

//5
namespace Employee.Data.Models
{
    public class User
    {
        public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        // Admin / Employee
        public string Role { get; set; } = "Employee";

        // HR / Technical
        public string? Department { get; set; } = string.Empty;

        // Designers / Development / SEO / Sales
        // System Engineer / DevOps
        public string? Team { get; set; } = string.Empty;

        // One User -> Many Employees
        public ICollection<Employee1> Employees { get; set; }
            = new List<Employee1>();
    }

}

