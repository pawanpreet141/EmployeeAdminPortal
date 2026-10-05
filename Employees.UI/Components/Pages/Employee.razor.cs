using Employees.UI.Models;
using Employees.UI.Services;

namespace Employees.UI.Components.Pages
{
    public partial class Employee
    {
        public List<Employee1> employees { get; set; }

        private Employee1 employee = new();

        private bool showForm = false;

        private int editingId = 0;

        protected override async Task OnInitializedAsync()
        {
            if (!UserSession.IsLoggedIn)
            {
                Navigation.NavigateTo("/login");
                return;
            }


            await LoadEmployees();
        }


        private async Task LoadEmployees()
        {
            employees =
                await HttpClient.GetAsync<List<Employee1>>(
                    $"api/Employees?userId={UserSession.UserId}")
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
                Team = selectedEmployee.Team,
                Age = selectedEmployee.Age,

                UserId = UserSession.UserId
            };

            editingId = selectedEmployee.Id;

            showForm = true;
        }


        private async Task SaveEmployee()
        {
            if (editingId == 0)
            {
                await HttpClient.PostAsync(
                    $"api/Employees?userId={UserSession.UserId}",
                    employee);
            }
            else
            {
                await HttpClient.PutAsync(
                    $"api/Employees/{editingId}?userId={UserSession.UserId}",
                    employee);
            }

            showForm = false;

            employee = new Employee1();

            editingId = 0;

            await LoadEmployees();
        }


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


        private void Cancel()
        {
            showForm = false;

            employee = new Employee1();

            editingId = 0;
        }

    }
}




//using Employees.UI.Models;
//using Employees.UI.Services;
//using Microsoft.AspNetCore.Components;

//namespace Employees.UI.Components.Pages
//{
//    public partial class Employee
//    {
//        [Inject]
//       private HttpClientWrapper HttpClient { get; set; } = default!;

//        [Inject]
//        private UserSession UserSession { get; set; } = default!;

//      [Inject]
//      private NavigationManager Navigation { get; set; } = default!;

//        [Parameter]
//        [SupplyParameterFromQuery(Name = "department")]
//        public string? DepartmentFromQuery { get; set; }

//        [Parameter]
//        [SupplyParameterFromQuery(Name = "team")]
//        public string? TeamFromQuery { get; set; }

//        private List<Employee1>? employees;

//        private Employee1 employee = new();

//        private bool showForm = false;

//        private int editingId = 0;

//        protected override async Task OnInitializedAsync()
//        {
//            employee = new Employee1();

//            await LoadEmployees();
//        }

//        private async Task LoadEmployees()
//        {
//            try
//            {
//                employees = await HttpClient.GetAsync<List<Employee1>>(
//                    "api/Employee");

//                if (employees == null)
//                {
//                    employees = new List<Employee1>();
//                    return;
//                }

//                // Filter by department selected by Admin
//                if (!string.IsNullOrWhiteSpace(DepartmentFromQuery))
//                {
//                    employees = employees
//                        .Where(x =>
//                            x.Department.Equals(
//                                DepartmentFromQuery,
//                                StringComparison.OrdinalIgnoreCase))
//                        .ToList();
//                }

//                // Filter by team selected by Admin
//                if (!string.IsNullOrWhiteSpace(TeamFromQuery))
//                {
//                    employees = employees
//                        .Where(x =>
//                            x.Team.Equals(
//                                TeamFromQuery,
//                                StringComparison.OrdinalIgnoreCase))
//                        .ToList();
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);

//                employees = new List<Employee1>();
//            }
//        }
//    }
//}

