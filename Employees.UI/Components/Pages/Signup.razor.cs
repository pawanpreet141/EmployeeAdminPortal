using Employees.UI.Services;

namespace Employees.UI.Components.Pages
{
    public partial class Signup
    {

        private string Name = string.Empty;

        private string Email = string.Empty;

        private string Department = string.Empty;

        private string Password = string.Empty;

        private string ConfirmPassword = string.Empty;

        private string ErrorMessage = string.Empty;

        private string SuccessMessage = string.Empty;

        private async Task SignupUser()
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Department) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage =
                    "Please fill all fields.";

                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage =
                    "Passwords do not match.";

                return;
            }

            var result = await Api.Signup(
                new SignupRequest
                {
                    Name = Name,
                    Email = Email,
                    Password = Password,
                    Department = Department
                });

            if (result == null || !result.Success)
            {
                ErrorMessage =
                    result?.Message ??
                    "Signup failed.";

                return;
            }

            SuccessMessage =
                "Account created successfully.";

            await Task.Delay(1000);

            Navigation.NavigateTo("/login");

            // Signup successful → Login
            //Navigation.NavigateTo("/");
        }
    }
}