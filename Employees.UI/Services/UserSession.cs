using Microsoft.AspNetCore.Identity;

namespace Employees.UI.Services
{
    public class UserSession
    {
        public int UserId { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public string Department { get; private set; } = string.Empty;

        
        public bool IsLoggedIn =>
            UserId > 0;
        

        public void Login(
            int userId,
            string name,
            string email,
            string department)
        {
            UserId = userId;

            Name = name;

            Email = email;

            Department = department;
        }


        public void Logout()
        {
            UserId = 0;

            Name = string.Empty;

            Email = string.Empty;

            Department = string.Empty;

        }
    }
}


