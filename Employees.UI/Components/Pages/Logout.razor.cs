namespace Employees.UI.Components.Pages
{
    public partial class Logout
    {
        protected override void OnInitialized()
        {
            Navigation.NavigateTo("/login");
        }
    }
}