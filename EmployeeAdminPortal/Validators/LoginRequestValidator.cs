//23
using Employee.API.Controllers;
using FluentValidation;

namespace Employee.API.Validators
{
    public class LoginRequestValidator
        : AbstractValidator<AccountController.LoginRequest>
    {
        public LoginRequestValidator()
        {
             
            //Email
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")

                .EmailAddress()
                .WithMessage(
                    "Please enter a valid email address.");


            //Password
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}