using System.Text.Json;
using Employees.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Employees.UI.Components.Pages
{
    public partial class Login
    {
        private string Email = string.Empty;
        private string Password = string.Empty;
        private string ErrorMessage = string.Empty;


        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;


        private async Task LoginUser()
        {
            ErrorMessage = string.Empty;

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
                result.Role,
                result.Department,
                result.Team,
                result.Token);

            //Navigation.NavigateTo("/employees");

            // Notify Blazor authentication 5
            if (AuthStateProvider 
                is CustomAuthStateProvider authProvider) 
            { 
                authProvider.NotifyUserLogin();
            } 
            
            // Redirect based on role/department
            if (result.Role == "Admin") 
            { 
                Navigation.NavigateTo( "/admin-dashboard");
            } 
            else if (result.Department == "HR") 
            { 
                Navigation.NavigateTo( "/hr-dashboard");
            }
            else if (result.Department == "Technical") 
            { 
                Navigation.NavigateTo( "/technical-dashboard"); 
            } 
            else
            { 
                ErrorMessage = "Invalid department assigned to this account."; 
            }
            //5
        }

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