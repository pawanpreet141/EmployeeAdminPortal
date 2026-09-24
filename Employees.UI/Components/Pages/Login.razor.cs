
using System.Text.Json;
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

            // Validate fields individually
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Email is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Password is required.";
                return;
            }

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
                ErrorMessage = GetMessage(
                    result.Message,
                    "Login failed.");

                return;
            }

            UserSession.Login(
                result.Id,
                result.Name,
                result.Email,
                result.Department,
                result.Token);

            Navigation.NavigateTo("/employees");
        }
// 24
        private string GetMessage(
            string? message,
            string defaultMessage)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return defaultMessage;
            }

            try
            {
                using JsonDocument document =
                    JsonDocument.Parse(message);

                if (document.RootElement.TryGetProperty(
                    "message",
                    out JsonElement messageElement))
                {
                    return messageElement.GetString()
                           ?? defaultMessage;
                }
            }
            catch (JsonException)
            {
                // Message is already plain text
            }

            return message;
        }

    }
}