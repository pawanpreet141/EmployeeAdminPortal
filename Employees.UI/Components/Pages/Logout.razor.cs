using Employees.UI.Services;
using Microsoft.AspNetCore.Components;

namespace Employees.UI.Components.Pages
{
    public partial class Logout
    {
        [Inject]
        public UserSession Session { get; set; }
        UserSession session;
        protected override void OnInitialized()
        {
            Session.Logout();
            Navigation.NavigateTo("/login");
        }
    }
}