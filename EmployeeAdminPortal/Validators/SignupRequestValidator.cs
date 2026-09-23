////23
//using Employee.API.Controllers;
//using FluentValidation;
//using Microsoft.AspNetCore.Identity;

//namespace Employee.API.Validators
//{
//    public class SignupRequestValidator
//        : AbstractValidator<AccountController.SignupRequest>
//    {
//        public SignupRequestValidator()
//        {
//            //ClassLevelCascadeMode = CascadeMode.Stop;


//            //Name
//            RuleFor(x => x.Name)
//                .NotEmpty()
//                .WithMessage("Name is required.")

//                .MaximumLength(100)
//                .WithMessage(
//                    "Name cannot exceed 100 characters.");




//            //Email
//            RuleFor(x => x.Email)
//                .NotEmpty()
//                .WithMessage("Email is required.")

//                .EmailAddress()
//                .WithMessage(
//                    "Please enter a valid email address.");


//            //Department
//            RuleFor(x => x.Department)
//                .NotEmpty()
//                .WithMessage("Department is required.")

//                .Must(IsValidDepartment)
//                .WithMessage(
//                    "Department must be HR, Developer, Designing, or Sales.");


//            // Password

//            //    RuleFor(x => x.Password)
//            //        .NotEmpty()
//            //        .WithMessage("Password is required.")




//            //        .MinimumLength(8)
//            //        .WithMessage(
//            //            "Password must be at least 8 characters long.")


//            //        .Matches("[a-z]")
//            //        .WithMessage(
//            //            "Password must contain at least one lowercase letter.")


//            //        .Matches("[A-Z]")
//            //        .WithMessage(
//            //            "Password must contain at least one uppercase letter.")


//            //        .Matches("[0-9]")
//            //        .WithMessage(
//            //            "Password must contain at least one number.")


//            //        .Matches(@"[^a-zA-Z0-9]")
//            //        .WithMessage(
//            //            "Password must contain at least one special character.");

//            //}


//            RuleFor(x => x.Password)
//    .NotEmpty()
//    .WithMessage("Password is required.")

//    .Must(password =>
//        password.Length >= 8 &&
//        password.Any(char.IsLower) &&
//        password.Any(char.IsUpper) &&
//        password.Any(char.IsDigit) &&
//        password.Any(ch => !char.IsLetterOrDigit(ch)))
//    .WithMessage(
//        "Password must be at least 8 characters long, include at least one uppercase letter, one number, and one special character.");
//        }

//         // Department Validation
//        private bool IsValidDepartment(string department)
//        {
//            string[] validDepartments =
//            {
//                "HR",
//                "Developer",
//                "Designing",
//                "Sales"
//            };

//            return validDepartments.Contains(
//                department,
//                StringComparer.OrdinalIgnoreCase);
//        }
//    }
//}





//using Employee.API.Controllers;
//using FluentValidation;

//namespace Employee.API.Validators
//{
//    public class SignupRequestValidator
//        : AbstractValidator<AccountController.SignupRequest>
//    {
//        public SignupRequestValidator()
//        {
//            RuleFor(x => x)
//                .Must(IsDepartmentValid)
//                .WithMessage("Department is required.")
//                .When(x => string.IsNullOrWhiteSpace(x.Department));

//            RuleFor(x => x)
//                .Must(IsDepartmentValid)
//                .WithMessage("Department must be HR, Developer, Designing, or Sales.")
//                .When(x => !string.IsNullOrWhiteSpace(x.Department));

//            RuleFor(x => x.Password)
//                .NotEmpty()
//                .WithMessage("Password is required.")
//                .MinimumLength(8)
//                .WithMessage("Password must be at least 8 characters long.")
//                .Matches("[a-z]")
//                .WithMessage("Password must contain at least one lowercase letter.")
//                .Matches("[A-Z]")
//                .WithMessage("Password must contain at least one uppercase letter.")
//                .Matches("[0-9]")
//                .WithMessage("Password must contain at least one number.")
//                .Matches(@"[^a-zA-Z0-9]")
//                .WithMessage("Password must contain at least one special character.");
//        }

//        private bool IsDepartmentValid(
//            AccountController.SignupRequest request)
//        {
//            string[] validDepartments =
//            {
//                "HR",
//                "Developer",
//                "Designing",
//                "Sales"
//            };

//            return validDepartments.Contains(
//                request.Department,
//                StringComparer.OrdinalIgnoreCase);
//        }
//    }
//}





using Employee.API.Controllers;
using FluentValidation;

namespace Employee.API.Validators
{
    public class SignupRequestValidator
        : AbstractValidator<AccountController.SignupRequest>
    {
        public SignupRequestValidator()
        {
            
            // 1. NAME
           

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters.");


            
            // 2. EMAIL
           

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Please enter a valid email address.");


          
            // 3. DEPARTMENT
         

            RuleFor(x => x.Department)
                .NotEmpty()
                .WithMessage("Department is required.")
                .Must(IsValidDepartment)
                .WithMessage(
                    "Department must be HR, Developer, Designing, or Sales.");



            // 4. PASSWORD


            //RuleFor(x => x.Password)
            //    .NotEmpty()
            //    .WithMessage("Password is required.")
            //    .MinimumLength(8)
            //    .WithMessage(
            //        "Password must be at least 8 characters long.")
            //    .Matches("[a-z]")
            //    .WithMessage(
            //        "Password must contain at least one lowercase letter.")
            //    .Matches("[A-Z]")
            //    .WithMessage(
            //        "Password must contain at least one uppercase letter.")
            //    .Matches("[0-9]")
            //    .WithMessage(
            //        "Password must contain at least one number.")
            //    .Matches(@"[^a-zA-Z0-9]")
            //    .WithMessage(
            //        "Password must contain at least one special character.");



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



        // DEPARTMENT VALIDATION


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

