using Employees.UI.Models;

namespace Employees.UI.Components.Pages
{
    public partial class Employee
    {

        private List<Employee1>? employees;

        private Employee1 employee = new();


        private bool showForm = false;

        private int editingId = 0;


        protected override async Task OnInitializedAsync()
        {
            await LoadEmployees();
        }


        private async Task LoadEmployees()
        {
            employees =
                await HttpClient.GetAsync<List<Employee1>>(
                    "api/Employees")
                ?? new List<Employee1>();
        }


        private void ShowAddForm()
        {
            employee = new Employee1();

            editingId = 0;

            showForm = true;
        }

        private void EditEmployee(Employee1 selectedEmployee)
        {
            employee = new Employee1
            {
                Id = selectedEmployee.Id,
                Name = selectedEmployee.Name,
                Email = selectedEmployee.Email,
                Salary = selectedEmployee.Salary,
                Department = selectedEmployee.Department,
                Age = selectedEmployee.Age
            };

            editingId = selectedEmployee.Id;

            showForm = true;
        }



        private async Task SaveEmployee()
        {
            if (editingId == 0)
            {
                await HttpClient.PostAsync(
            "api/Employees",
            employee);

            }
            else
            {
                await HttpClient.PutAsync(
        $"api/Employees/{editingId}",
        employee);




            }

            showForm = false;

            employee = new Employee1();

            editingId = 0;

            await LoadEmployees();
        }


        private async Task DeleteEmployee(int id)
        {
            var response = await HttpClient.DeleteAsync(
                $"api/Employees/{id}");

            if (response.IsSuccessStatusCode)
            {
                await LoadEmployees();
            }
        }



        private void Cancel()
        {
            showForm = false;

            employee = new Employee1();

            editingId = 0;
        }

        // 1111
        // private void Logout()
        // {
        // 	Navigation.NavigateTo("/");
        // }
    }
}