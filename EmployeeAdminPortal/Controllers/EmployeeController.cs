//using Employee.Data.Data;
//using Employee.Data.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Cryptography.X509Certificates;

//namespace Employee.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]

//    //1
//    [Authorize]
//    public class EmployeesController : ControllerBase
//    {
//        private readonly EmployeeDbContext _context;

//        public EmployeesController(EmployeeDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Employees?userId=1
//        // Get only employees belonging to this user
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Employee1>>> GetEmployees(
//            [FromQuery] int userId)
//        {
//            if (userId <= 0)
//            {
//                return BadRequest("Invalid UserId.");
//            }

//            var employees = await _context.Employees
//                .Where(x => x.UserId == userId)
//                .ToListAsync();

//            return Ok(employees);
//        }


//        // GET: api/Employees/1?userId=1
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Employee1>> GetEmployee(
//            int id,
//            [FromQuery] int userId)
//        {
//            var employee = await _context.Employees
//                .FirstOrDefaultAsync(x =>
//                    x.Id == id &&
//                    x.UserId == userId);

//            if (employee == null)
//            {
//                return NotFound();
//            }

//            return Ok(employee);
//        }


//        // POST: api/Employees?userId=1
//        // Add employee for the logged-in user
//        [HttpPost]
//        public async Task<ActionResult<Employee1>> CreateEmployee(
//            [FromQuery] int userId,
//            Employee1 employee)
//        {
//            if (userId <= 0)
//            {
//                return BadRequest("Invalid UserId.");
//            }

//            // Automatically assign employee to this user
//            employee.UserId = userId;

//            // Database generates the Id
//            employee.Id = 0;

//            _context.Employees.Add(employee);

//            await _context.SaveChangesAsync();

//            return CreatedAtAction(
//                nameof(GetEmployee),
//                new
//                {
//                    id = employee.Id,
//                    userId = userId
//                },
//                employee);
//        }


//        // PUT: api/Employees/1?userId=1
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateEmployee(
//            int id,
//            [FromQuery] int userId,
//            Employee1 employee)
//        {
//            if (userId <= 0)
//            {
//                return BadRequest("Invalid UserId.");
//            }

//            if (id != employee.Id)
//            {
//                return BadRequest();
//            }

//            // Find employee only if it belongs to this user
//            var existingEmployee =
//                await _context.Employees
//                    .FirstOrDefaultAsync(x =>
//                        x.Id == id &&
//                        x.UserId == userId);

//            if (existingEmployee == null)
//            {
//                return NotFound();
//            }

//            existingEmployee.Name = employee.Name;
//            existingEmployee.Email = employee.Email;
//            existingEmployee.Age = employee.Age;
//            existingEmployee.Department = employee.Department;
//            existingEmployee.Salary = employee.Salary;


//            await _context.SaveChangesAsync();

//            return NoContent();
//        }


//        // DELETE: api/Employees/1?userId=1
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteEmployee(
//            int id,
//            [FromQuery] int userId)
//        {
//            if (userId <= 0)
//            {
//                return BadRequest("Invalid UserId.");
//            }


//            // Find employee only if it belongs to this user
//            var employee =
//                await _context.Employees
//                    .FirstOrDefaultAsync(x =>
//                        x.Id == id &&
//                        x.UserId == userId);

//            if (employee == null)
//            {
//                return NotFound();
//            }

//            _context.Employees.Remove(employee);

//            await _context.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}






//23
using Employee.Data.Data;
using Employee.Data.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        private readonly IValidator<Employee1> _validator;


        
        // CONSTRUCTOR
        

        public EmployeesController(
            EmployeeDbContext context,
            IValidator<Employee1> validator)
        {
            _context = context;

            _validator = validator;
        }


       //get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee1>>>
            GetEmployees(
                [FromQuery] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(
                    "Invalid UserId.");
            }


            var employees =
                await _context.Employees
                    .Where(x =>
                        x.UserId == userId)
                    .ToListAsync();


            return Ok(employees);
        }


        //get
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee1>>
            GetEmployee(
                int id,
                [FromQuery] int userId)
        {
            var employee =
                await _context.Employees
                    .FirstOrDefaultAsync(x =>
                        x.Id == id &&
                        x.UserId == userId);


            if (employee == null)
            {
                return NotFound();
            }


            return Ok(employee);
        }


       //Create
        [HttpPost]
        public async Task<ActionResult<Employee1>>
            CreateEmployee(
                [FromQuery] int userId,
                Employee1 employee)
        {
            if (userId <= 0)
            {
                return BadRequest(
                    "Invalid UserId.");
            }


            // FluentValidation
            var validationResult =
                await _validator.ValidateAsync(employee);


            if (!validationResult.IsValid)
            {
                return BadRequest(
                    validationResult.Errors);
            }


            // Assign UserId
            employee.UserId = userId;


            // Database generates Id
            employee.Id = 0;


            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();


            return CreatedAtAction(
                nameof(GetEmployee),

                new
                {
                    id = employee.Id,

                    userId = userId
                },

                employee);
        }


        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult>
            UpdateEmployee(
                int id,
                [FromQuery] int userId,
                Employee1 employee)
        {
            if (userId <= 0)
            {
                return BadRequest(
                    "Invalid UserId.");
            }


            if (id != employee.Id)
            {
                return BadRequest(
                    "Employee ID does not match.");
            }


            // FluentValidation
            var validationResult =
                await _validator.ValidateAsync(employee);


            if (!validationResult.IsValid)
            {
                return BadRequest(
                    validationResult.Errors);
            }


            // Find existing employee
            var existingEmployee =
                await _context.Employees
                    .FirstOrDefaultAsync(x =>
                        x.Id == id &&
                        x.UserId == userId);


            if (existingEmployee == null)
            {
                return NotFound();
            }


            // Update fields
            existingEmployee.Name =
                employee.Name;

            existingEmployee.Email =
                employee.Email;

            existingEmployee.Age =
                employee.Age;

            existingEmployee.Department =
                employee.Department;

            existingEmployee.Salary =
                employee.Salary;


            await _context.SaveChangesAsync();


            return NoContent();
        }


         //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult>
            DeleteEmployee(
                int id,
                [FromQuery] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(
                    "Invalid UserId.");
            }


            var employee =
                await _context.Employees
                    .FirstOrDefaultAsync(x =>
                        x.Id == id &&
                        x.UserId == userId);


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


