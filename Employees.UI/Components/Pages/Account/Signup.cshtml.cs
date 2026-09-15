//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace Employees.UI.Components.Pages.Account
//{
//    public class SignupModel : PageModel
//    {
//        public void OnGet()
//        {
//        }
//    }
//}




//using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace Employees.UI.Components.Pages.Account
//{
//    public class SignupModel : PageModel
//    {
//        private readonly EmployeeDbContext _context;
//        private readonly PasswordHasher<User> _passwordHasher;

//        public SignupModel(EmployeeDbContext context)
//        {
//            _context = context;
//            _passwordHasher = new PasswordHasher<User>();
//        }

//        [BindProperty]
//        [Required]
//        public string Name { get; set; } = string.Empty;

//        [BindProperty]
//        [Required]
//        [EmailAddress]
//        public string Email { get; set; } = string.Empty;

//        [BindProperty]
//        [Required]
//        [MinLength(6)]
//        public string Password { get; set; } = string.Empty;

//        [BindProperty]
//        [Required]
//        [Compare("Password")]
//        public string ConfirmPassword { get; set; } = string.Empty;

//        public void OnGet()
//        {
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }

//            var emailExists = _context.Users
//                .Any(x => x.Email == Email);

//            if (emailExists)
//            {
//                ModelState.AddModelError(
//                    "Email",
//                    "Email already exists.");

//                return Page();
//            }

//            var user = new User
//            {
//                Name = Name,
//                Email = Email
//            };

//            user.PasswordHash =
//                _passwordHasher.HashPassword(user, Password);

//            _context.Users.Add(user);

//            await _context.SaveChangesAsync();

//            return RedirectToPage("/Account/Login");
//        }
//    }

//    internal class User
//    {
//        internal string PasswordHash;

//        public string Name { get; set; }
//        public string Email { get; set; }
//    }
//}

