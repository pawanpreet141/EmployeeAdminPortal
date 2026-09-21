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
            ErrorMessage = "Login button was clicked.";

            await Task.Delay(100);

            var result = await Api.Login(
                new LoginRequest
                {
                    Email = Email,
                    Password = Password
                });

            if (result == null)
            {
                ErrorMessage = "API returned null.";
                return;
            }

            if (!result.Success)
            {
                ErrorMessage = result.Message;
                return;
            }

            ErrorMessage = "API login successful. Going to employees...";

            UserSession.Login(
                result.Id,
                result.Name,
                result.Email,
                result.Department);

             Navigation.NavigateTo("/employees");
        }
    }
}