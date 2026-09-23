using Employee.Data.Models;
using FluentValidation;

namespace Employee.API.Validators
{
    public class EmployeeValidator
        : AbstractValidator<Employee1>
    {
        public EmployeeValidator()
        {
            //Name
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Employee name is required.")

                .MaximumLength(100)
                .WithMessage(
                    "Employee name cannot exceed 100 characters.");


          
            // Email
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")

                .EmailAddress()
                .WithMessage(
                    "Please enter a valid email address.");

            // AGE

            RuleFor(x => x.Age)
                .InclusiveBetween(18, 60)
                .WithMessage(
                    "Age must be between 18 and 60.");

            // DEPARTMENT

            RuleFor(x => x.Department)
                .NotEmpty()
                .WithMessage("Department is required.")

                .Must(IsValidDepartment)
                .WithMessage(
                    "Department must be HR, Developer, Designing, or Sales.");


           //Salary
            RuleFor(x => x.Salary)
                .GreaterThan(0)
                .WithMessage(
                    "Salary must be greater than 0.");
        }


      
        // Department Validation
        private bool IsValidDepartment(string department)
        {
            string[] validDepartments =
            {
                "HR",
                "Developer",
                "Designing",
                "Sales"
            };

            return validDepartments.Contains(
                department,
                StringComparer.OrdinalIgnoreCase);
        }
    }
}
