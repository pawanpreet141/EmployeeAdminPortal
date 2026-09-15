////using Employee.Data;
////using Employee.Data.Models;
//using Employees.UI.Components.Pages.Account;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
////using Microsoft.EntityFrameworkCore;
//using System.ComponentModel.DataAnnotations;

//namespace Employees.UI.Components.Pages.Account
//{
//    public class LoginModel : PageModel
//    {
//        private readonly EmployeeDbContext _context;
//        private readonly PasswordHasher<User> _passwordHasher;

//        public LoginModel(EmployeeDbContext context)
//        {
//            _context = context;
//            _passwordHasher = new PasswordHasher<User>();
//        }

//        [BindProperty]
//        [Required]
//        [EmailAddress]
//        public string Email { get; set; } = string.Empty;

//        [BindProperty]
//        [Required]
//        public string Password { get; set; } = string.Empty;

//        public void OnGet()
//        {
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }

//            var user = await _context.Users
//                .FirstOrDefaultAsync(x => x.Email == Email);

//            if (user == null)
//            {
//                ModelState.AddModelError(
//                    string.Empty,
//                    "Invalid email or password.");

//                return Page();
//            }

//            var result = _passwordHasher.VerifyHashedPassword(
//                user,
//                user.PasswordHash,
//                Password);

//            if (result == PasswordVerificationResult.Failed)
//            {
//                ModelState.AddModelError(
//                    string.Empty,
//                    "Invalid email or password.");

//                return Page();
//            }

//            HttpContext.Session.SetInt32("UserId", user.Id);
//            HttpContext.Session.SetString("UserName", user.Name);
//            HttpContext.Session.SetString("UserEmail", user.Email);

//            return RedirectToPage("/Index");
//        }
//    }
//}
