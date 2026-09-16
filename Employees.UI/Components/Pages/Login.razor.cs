using Employees.UI.Services;

namespace Employees.UI.Components.Pages
{
    public partial class Login
    {
        private string Email = string.Empty;

        private string Password = string.Empty;

        private string ErrorMessage = string.Empty;

        private async Task LoginUser()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage =
                    "Please enter email and password.";

                return;
            }

            var result = await Api.Login(
                new LoginRequest
                {
                    Email = Email,
                    Password = Password
                });

            if (result == null || !result.Success)
            {
                ErrorMessage =
                    result?.Message ??
                    "Login failed.";

                return;
            }

            //  Navigation.NavigateTo("/");
            // Login successful
            Navigation.NavigateTo("/employees");
        }
    }
}