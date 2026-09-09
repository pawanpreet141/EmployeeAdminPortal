using Employee.Data.Data;
using Employee.Data.Models;
using System.Security.Permissions;


namespace Repository.Design;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeDbContext _context;

    public EmployeeRepository(EmployeeDbContext context)
    {
        _context = context;
    }

    public List<Employee1> GetAll()
    {
        return _context.Employees.ToList();
    }

    public async Task<Employee1?> Employee1GetByIdAsync(int id)
    {
        return _context.Employees
            .FirstOrDefault(e => e.Id == id);
    }

    public void Add(Employee1 employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void Update(Employee1 employee)
    {
        _context.Employees.Update(employee);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var employee = _context.Employees
            .FirstOrDefault(e => e.Id == id);

        if (employee != null)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }

    public Task<List<Employee1>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Employee1?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Employee1> AddAsync(Employee1 employee)
    {
        throw new NotImplementedException();
    }

    public Task<Employee1?> UpdateAsync(Employee1 employee)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
