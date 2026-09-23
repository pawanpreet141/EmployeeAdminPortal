//23
using Employee.Data.Data;
using Employee.Data.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Employee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        private readonly PasswordHasher<User> _passwordHasher;

        private readonly IConfiguration _configuration;

        private readonly IValidator<SignupRequest> _signupValidator;

        private readonly IValidator<LoginRequest> _loginValidator;
        


       // Constructor
        public AccountController(
            EmployeeDbContext context,
            IConfiguration configuration,
            IValidator<SignupRequest> signupValidator,
            IValidator<LoginRequest> loginValidator)
        {
            _context = context;

            _configuration = configuration;

            _passwordHasher = new PasswordHasher<User>();

            _signupValidator = signupValidator;

            _loginValidator = loginValidator;
        }


        
        // SIGNUP
        

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(
            [FromBody] SignupRequest request)
        {
            // FluentValidation
            var validationResult =
                await _signupValidator.ValidateAsync(request);

            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(
            //        validationResult.Errors);
            //}

            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(
            //        validationResult.Errors.Select(x => new
            //        {
            //            field = x.PropertyName,
            //            message = x.ErrorMessage
            //        })
            //    );
            //}


            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(new
            //    {
            //        message = string.Join(" ",
            //            validationResult.Errors
            //                .Select(x => x.ErrorMessage)
            //                .Distinct())
            //    });
            //}

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = validationResult.Errors
                        .First()
                        .ErrorMessage
                });
            }


            // Check email already exists
            var emailExists =
                await _context.Users
                    .AnyAsync(x =>
                        x.Email == request.Email);

            if (emailExists)
            {
                return BadRequest(
                    "Email already exists.");
            }


            // Create user
            var user = new User
            {
                Name = request.Name,

                Email = request.Email,

                Department = request.Department
            };


            // Hash password
            user.PasswordHash =
                _passwordHasher.HashPassword(
                    user,
                    request.Password);


            try
            {
                _context.Users.Add(user);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,

                    innerException =
                        ex.InnerException?.Message
                });
            }


            return Ok(new
            {
                message =
                    "Account created successfully."
            });
        }


        
        // LOGIN
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request)
        {
            // FluentValidation
            var validationResult =
                await _loginValidator.ValidateAsync(request);

            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(
            //        validationResult.Errors);
            //}

            if (!validationResult.IsValid)
            {
                return BadRequest(
                    validationResult.Errors.Select(x => new
                    {
                        field = x.PropertyName,
                        message = x.ErrorMessage
                    })
                );
            }


            // Find user
            var user =
                await _context.Users
                    .FirstOrDefaultAsync(x =>
                        x.Email == request.Email);

            //if (user == null)
            //{
            //    return Unauthorized(
            //        "Invalid email or password.");
            //}

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }


            // Verify password
            var result =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    request.Password);

            //if (result ==
            //    PasswordVerificationResult.Failed)
            //{
            //    return Unauthorized(
            //        "Invalid email or password.");
            //}

            if (result ==
    PasswordVerificationResult.Failed)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }


            // Generate JWT
            var token =
                GenerateJwtToken(user);


            return Ok(new
            {
                id = user.Id,

                name = user.Name,

                email = user.Email,

                department = user.Department,

                token = token,

                message = "Login successful."
            });
        }


        
        // GENERATE JWT
        

        private string GenerateJwtToken(User user)
        {
            var key =
                _configuration["Jwt:Key"];

            var issuer =
                _configuration["Jwt:Issuer"];

            var audience =
                _configuration["Jwt:Audience"];


            var securityKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key!));


            var credentials =
                new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256);


            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.Name),

                new Claim(
                    ClaimTypes.Email,
                    user.Email),

                new Claim(
                    "Department",
                    user.Department)
            };


            var tokenDescriptor =
                new SecurityTokenDescriptor
                {
                    Subject =
                        new ClaimsIdentity(claims),

                    Expires =
                        DateTime.UtcNow.AddHours(2),

                    Issuer = issuer,

                    Audience = audience,

                    SigningCredentials =
                        credentials
                };


            var tokenHandler =
                new JwtSecurityTokenHandler();


            var token =
                tokenHandler.CreateToken(
                    tokenDescriptor);


            return tokenHandler.WriteToken(token);
        }


        
        // SIGNUP REQUEST
        

        public class SignupRequest
        {
            public string Name { get; set; }
                = string.Empty;

            public string Email { get; set; }
                = string.Empty;

            public string Department { get; set; }
                = string.Empty;

            public string Password { get; set; }
                = string.Empty;
        }


        
        // LOGIN REQUEST
        

        public class LoginRequest
        {
            public string Email { get; set; }
                = string.Empty;

            public string Password { get; set; }
                = string.Empty;
        }
    }
}