using Employees.UI.Models;
using Employees.UI.Services;

namespace Employees.UI.Components.Pages
{
    public partial class Employee
    {
        private List<Employee1>? employees;

        private Employee1 employee = new();

        private bool showForm = false;

        private int editingId = 0;


        // Load employees when page opens
        protected override async Task OnInitializedAsync()
        {
            // Check if user is logged in
            if (!UserSession.IsLoggedIn)
            {
                Navigation.NavigateTo("/login");

                return;
            }

            await LoadEmployees();
        }


        // Load only employees belonging to logged-in user
        private async Task LoadEmployees()
        {
            employees =
                await HttpClient.GetAsync<List<Employee1>>(
                    $"api/Employees?userId={UserSession.UserId}")
                ?? new List<Employee1>();
        }


        // Show Add Employee form
        private void ShowAddForm()
        {
            employee = new Employee1();

            editingId = 0;

            showForm = true;
        }


        // Edit employee
        private void EditEmployee(Employee1 selectedEmployee)
        {
            employee = new Employee1
            {
                Id = selectedEmployee.Id,
                Name = selectedEmployee.Name,
                Email = selectedEmployee.Email,
                Salary = selectedEmployee.Salary,
                Department = selectedEmployee.Department,
                Age = selectedEmployee.Age,

                // Keep current user's Id
                UserId = UserSession.UserId
            };

            editingId = selectedEmployee.Id;

            showForm = true;
        }


        // Add or Update employee
        private async Task SaveEmployee()
        {
            if (editingId == 0)
            {
                // Add employee
                await HttpClient.PostAsync(
                    $"api/Employees?userId={UserSession.UserId}",
                    employee);
            }
            else
            {
                // Update employee
                await HttpClient.PutAsync(
                    $"api/Employees/{editingId}?userId={UserSession.UserId}",
                    employee);
            }

            showForm = false;

            employee = new Employee1();

            editingId = 0;

            await LoadEmployees();
        }


        // Delete employee
        private async Task DeleteEmployee(int id)
        {
            var response =
                await HttpClient.DeleteAsync(
                    $"api/Employees/{id}?userId={UserSession.UserId}");

            if (response.IsSuccessStatusCode)
            {
                await LoadEmployees();
            }
        }


        // Cancel form
        private void Cancel()
        {
            showForm = false;

            employee = new Employee1();

            editingId = 0;
        }

    }
}



