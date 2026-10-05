using Employee.API.Controllers;
using FluentValidation;

namespace Employee.API.Validators
{
    public class SignupRequestValidator
        : AbstractValidator<AccountController.SignupRequest>
    {
        public SignupRequestValidator()
        {
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters.");

          
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Please enter a valid email address.");


            RuleFor(x => x.Department)
                .NotEmpty()
                .WithMessage("Department is required.")
                .Must(IsValidDepartment)
                .WithMessage(
                    "Department must be HR, Developer, Designing, or Sales.");

            // Team 5
             RuleFor(x => x.Team) 
                .NotEmpty()
                .WithMessage("Team is required.") 
                .Must((request, team) => IsValidTeam(request.Department, team)) 
                .WithMessage( "Invalid team for the selected department.");


            RuleFor(x => x.Password)
    .NotEmpty()
    .WithMessage("Password is required.")

                .Must(password =>
                    password.Length >= 8 &&
                    password.Any(char.IsLower) &&
                    password.Any(char.IsUpper) &&
                    password.Any(char.IsDigit) &&
                    password.Any(ch => !char.IsLetterOrDigit(ch)))
                .WithMessage(
                    "Password must be at least 8 characters long, include at least one uppercase letter, one number, and one special character.");
        
        }


        //private bool IsValidDepartment(string department)
        //{
        //    string[] validDepartments =
        //    {
        //        "HR",
        //        "Developer",
        //        "Designing",
        //        "Sales"
        //    };

        //    return validDepartments.Contains(
        //        department,
        //        StringComparer.OrdinalIgnoreCase);
        //}

        //5

        private bool IsValidDepartment(string department) 
        { 
            string[] validDepartments = 
                { 
                "HR",
                "Technical" 
            }; 
            return validDepartments.Contains(
                department, StringComparer.OrdinalIgnoreCase);
        }
        private bool IsValidTeam(string department, 
            string team)
        {
            if (string.Equals(
                department, 
                "HR", 
                StringComparison.OrdinalIgnoreCase))
            { 
                string[] hrTeams =
                    { 
                    "Designers", 
                    "Development", 
                    "SEO", 
                    "Sales" 
                }; 
                return hrTeams.Contains(
                    team, 
                    StringComparer.OrdinalIgnoreCase);
            } 
            if (string.Equals(
                department, 
                "Technical",
                StringComparison.OrdinalIgnoreCase)) 
            { 
                string[] technicalTeams = 
                    { 
                    "System Engineer",
                    "DevOps"
                }; 
                return technicalTeams.Contains(
                    team, 
                    StringComparer.OrdinalIgnoreCase);
            } 
            return false;
        }
    }
}

