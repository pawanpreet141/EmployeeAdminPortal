using Employees.UI.Services;

namespace Employees.UI.Components.Pages
{
    public partial class Login
    {
        private string Email = string.Empty;

        private string Password = string.Empty;

        private string ErrorMessage = string.Empty;

        //private async Task LoginUser()
        //{
        //    ErrorMessage = string.Empty;

        //    if (string.IsNullOrWhiteSpace(Email) ||
        //        string.IsNullOrWhiteSpace(Password))
        //    {
        //        ErrorMessage =
        //            "Please enter email and password.";

        //        return;
        //    }

        //    var result = await Api.Login(
        //        new LoginRequest
        //        {
        //            Email = Email,
        //            Password = Password
        //        });

        //    //if (result == null || !result.Success)
        //    //{
        //    //    ErrorMessage =
        //    //        result?.Message ??
        //    //        "Login failed.";

        //    //    return;
        //    //}

        //    if (result == null) {
        //        ErrorMessage = "No response from server.";
        //        return;
        //    }
        //    //if (!result.Success) { 
        //    //    ErrorMessage = result.Message;
        //    //    return;
        //    //}

        //    if (!result.Success) { 
        //        ErrorMessage = 
        //            string.IsNullOrWhiteSpace(result.Message) 
        //            ? "Login failed." 
        //            : result.Message; return; }


        //    // Save logged-in user information
        //    UserSession.Login(
        //        result.Id,
        //        result.Name,
        //        result.Email,
        //        result.Department);

        //    //  Navigation.NavigateTo("/");
        //    // Login successful
        //    Navigation.NavigateTo("/employees");
        //}



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

            //// temporary add 
            //Console.WriteLine("===== SESSION AFTER LOGIN =====");
            //Console.WriteLine($"UserId: {UserSession.UserId}");
            //Console.WriteLine($"Name: {UserSession.Name}");
            //Console.WriteLine($"Email: {UserSession.Email}");
            //Console.WriteLine($"IsLoggedIn: {UserSession.IsLoggedIn}");
            //Console.WriteLine("===============================");

             Navigation.NavigateTo("/employees");
        }
    }
}