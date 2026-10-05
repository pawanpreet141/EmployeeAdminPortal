//5

using Employees.UI.Services;
using Microsoft.AspNetCore.Components;

namespace Employees.UI.Components
{
    public partial class AdminDashboard
    // public partial class AdminDashboard : ComponentBase
    {
        private string SelectedDepartment { get; set; }
        = string.Empty;

        private string SelectedTeam { get; set; }
            = string.Empty;

        protected override void OnInitialized()
        {
            // Only Admin can access this dashboard
            if (!UserSession.IsLoggedIn)
            {
                Navigation.NavigateTo("/login");
                return;
            }

            if (UserSession.Role != "Admin")
            {
                Navigation.NavigateTo("/login");
                return;
            }
        }

        private void OpenEmployeePage()
        {
            Navigation.NavigateTo("/employees");
        }

        private void Logout()
        {
            UserSession.Logout();

            Navigation.NavigateTo("/login");
        }
    }

}



//using Employees.UI.Services;
//using Microsoft.AspNetCore.Components;

//namespace Employees.UI.Components.Pages
//{
//    public partial class AdminDashboard
//    {
//        private string SelectedDepartment = string.Empty;
//        private string SelectedTeam = string.Empty;

//        [Inject]
//        private UserSession UserSession { get; set; } = default!;

//        [Inject]
//        private NavigationManager Navigation { get; set; } = default!;

//        private void OpenEmployeePage()
//        {
//            if (string.IsNullOrWhiteSpace(SelectedDepartment))
//            {
//                return;
//            }

//            var url = $"/employees?department={Uri.EscapeDataString(SelectedDepartment)}";

//            if (!string.IsNullOrWhiteSpace(SelectedTeam))
//            {
//                url += $"&team={Uri.EscapeDataString(SelectedTeam)}";
//            }

//            Navigation.NavigateTo(url);
//        }

//        private void Logout()
//        {
//            UserSession.Logout();
//            Navigation.NavigateTo("/login");
//        }
//    }
//}

