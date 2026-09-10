using Employee.Data.Data;
using Employee.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace EmpolyeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    //public class EmployeesController : Controller
    {
        private readonly EmployeeDbContext _context;

        public EmployeesController(EmployeeDbContext context)
        {
            _context = context;
        }


        //...serilog
        //public class EmployeesController : ControllerBase
        //{
        //    private readonly EmployeeDbContext _context;
        //    private readonly ILogger<EmployeesController> _logger;

        //    public EmployeesController(
        //        EmployeeDbContext context,
        //        ILogger<EmployeesController> logger)
        //    {
        //        _context = context;
        //        _logger = logger;
        //    }
        //}




        [HttpPost]
        public async Task<ActionResult<Employee1>> CreateEmployee(Employee1 employee)
        {
            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = employee.Id },
                employee
            );
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee1>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }


        //.....serilog
        //[HttpGet]
        //public async Task<IActionResult> GetEmployees()
        //{
        //    _logger.LogInformation("Getting all employees");

        //    var employees = await _context.Employees.ToListAsync();

        //    _logger.LogInformation(
        //        "Retrieved {EmployeeCount} employees",
        //        employees.Count);

        //    return Ok(employees);
        //}



        //error occur
        //[HttpGet]
        //public async Task<IActionResult> GetEmployees()
        //{
        //    try
        //    {
        //        _logger.LogInformation("Getting all employees");

        //        var employees = await _context.Employees.ToListAsync();

        //        _logger.LogInformation(
        //            "Retrieved {EmployeeCount} employees",
        //            employees.Count);

        //        return Ok(employees);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(
        //            ex,
        //            "Error occurred while getting employees");

        //        return StatusCode(
        //            500,
        //            "An error occurred while getting employees.");
        //    }
        //}





        [HttpGet("{id}")]
        public async Task<ActionResult<Employee1>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }


        //...serilog
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetEmployee(int id)
        //{
        //    _logger.LogInformation(
        //        "Getting employee with ID {EmployeeId}",
        //        id);

        //    var employee = await _context.Employees.FindAsync(id);

        //    if (employee == null)
        //    {
        //        _logger.LogWarning(
        //            "Employee with ID {EmployeeId} was not found",
        //            id);

        //        return NotFound();
        //    }

        //    _logger.LogInformation(
        //        "Employee with ID {EmployeeId} found",
        //        id);

        //    return Ok(employee);
        //}




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(
            int id,
            Employee1 employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            var existingEmployee = await _context.Employees.FindAsync(id);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Email = employee.Email;
            existingEmployee.Age = employee.Age;
            existingEmployee.Department = employee.Department;
            existingEmployee.Salary = employee.Salary;

            await _context.SaveChangesAsync();

            return NoContent();
        }


      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

