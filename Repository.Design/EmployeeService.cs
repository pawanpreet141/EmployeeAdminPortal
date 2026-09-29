using Employee.Data.Models;

namespace Employee.BusinessLogic;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<List<Employee1>> GetAllAsync(int userId)
    {
        return await _employeeRepository.GetAllAsync(userId);
    }

    public async Task<Employee1?> GetByIdAsync(
        int id,
        int userId)
    {
        return await _employeeRepository
            .GetByIdAsync(id, userId);
    }
    
    public async Task<Employee1> AddAsync(
        Employee1 employee,
        int userId)
    {
        employee.UserId = userId;
        employee.Id = 0;

        return await _employeeRepository
            .AddAsync(employee);
    }

    public async Task<Employee1?> UpdateAsync(
        Employee1 employee,
        int userId)
    {
        return await _employeeRepository
            .UpdateAsync(employee, userId);
    }

    public async Task<bool> DeleteAsync(
        int id,
        int userId)
    {
        return await _employeeRepository
            .DeleteAsync(id, userId);
    }
}
