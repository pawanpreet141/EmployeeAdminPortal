

using Employee.Data.Models;

//namespace Employee.Data.Repositories
namespace Repository.Design
{
    public interface IEmployeeRepository
    {
         Task <List<Employee1>> GetAllAsync();

        Task <Employee1?> GetByIdAsync(int id);

      Task <Employee1> AddAsync(Employee1 employee);

       Task<Employee1?> UpdateAsync(Employee1 employee);

        Task<bool> DeleteAsync(int id);
    }
}