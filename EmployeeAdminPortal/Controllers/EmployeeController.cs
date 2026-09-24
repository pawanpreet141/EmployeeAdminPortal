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

        // Serilog / ILogger /24
        private readonly ILogger<EmployeesController> _logger;



        // CONSTRUCTOR


        public EmployeesController(
            EmployeeDbContext context,
            IValidator<Employee1> validator,

            //serilog 24
             ILogger<EmployeesController> logger)
        {
            _context = context;

            _validator = validator;

            //serilog24
            _logger = logger;
        }


       //get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee1>>>
            GetEmployees(
                [FromQuery] int userId)
        {
            //serilog 24
            _logger.LogInformation(
                "Getting employees for UserId {UserId}",
                userId);

            if (userId <= 0)
            {
                //serilog24
                _logger.LogWarning(
                   "Get employees failed. Invalid UserId {UserId}",
                   userId);

                return BadRequest(
                    "Invalid UserId.");
            }


            var employees =
                await _context.Employees
                    .Where(x =>
                        x.UserId == userId)
                    .ToListAsync();

            //serilog24
            _logger.LogInformation(
            "Retrieved {EmployeeCount} employees for UserId {UserId}",
            employees.Count,
            userId);


            return Ok(employees);
        }


        //get
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee1>>
            GetEmployee(
                int id,
                [FromQuery] int userId)
        {
            //serilog 24
            _logger.LogInformation(
              "Getting EmployeeId {EmployeeId} for UserId {UserId}",
              id,
              userId);

            var employee =
                await _context.Employees
                    .FirstOrDefaultAsync(x =>
                        x.Id == id &&
                        x.UserId == userId);


            if (employee == null)
            {
                //serilog24
                _logger.LogWarning(
                  "Employee not found. EmployeeId {EmployeeId}, UserId {UserId}",
                  id,
                  userId);

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
            //serilog 24
            _logger.LogInformation(
                "Creating employee for UserId {UserId}",
                userId);

            if (userId <= 0)
            {
                //serilog24
                _logger.LogWarning(
                 "Create employee failed. Invalid UserId {UserId}",
                 userId);

                return BadRequest(
                    "Invalid UserId.");
            }


            // FluentValidation
            var validationResult =
                await _validator.ValidateAsync(employee);


            if (!validationResult.IsValid)
            {
                //serilog24
                _logger.LogWarning(
                 "Employee validation failed for UserId {UserId}",
                 userId);

                return BadRequest(
                    validationResult.Errors);
            }


            // Assign UserId
            employee.UserId = userId;


            // Database generates Id
            employee.Id = 0;


            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            //serilog24
            _logger.LogInformation(
               "Employee created successfully. EmployeeId {EmployeeId}, UserId {UserId}",
               employee.Id,
               userId);


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
            //serilog24
            _logger.LogInformation(
               "Updating EmployeeId {EmployeeId} for UserId {UserId}",
               id,
               userId);

            if (userId <= 0)
            {
                //serilog24
                _logger.LogWarning(
                "Update employee failed. Invalid UserId {UserId}",
                userId);

                return BadRequest(
                    "Invalid UserId.");
            }


            if (id != employee.Id)
            {
                //serilog24
                _logger.LogWarning(
                   "Update employee failed. Employee ID mismatch. RouteId {RouteId}, EmployeeId {EmployeeId}",
                   id,
                   employee.Id);

                return BadRequest(
                    "Employee ID does not match.");
            }


            // FluentValidation
            var validationResult =
                await _validator.ValidateAsync(employee);


            if (!validationResult.IsValid)
            {
                //serilog 24
                _logger.LogWarning(
                 "Employee validation failed during update. EmployeeId {EmployeeId}, UserId {UserId}",
                 id,
                 userId);

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
                //serilog24
                _logger.LogWarning(
                 "Update failed. Employee not found. EmployeeId {EmployeeId}, UserId {UserId}",
                 id,
                 userId);

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

            //serilog24
            _logger.LogInformation(
             "Employee updated successfully. EmployeeId {EmployeeId}, UserId {UserId}",
             id,
             userId);


            return NoContent();
        }


         //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult>
            DeleteEmployee(
                int id,
                [FromQuery] int userId)
        {
            //serilog24
            _logger.LogInformation(
           "Deleting EmployeeId {EmployeeId} for UserId {UserId}",
           id,
           userId);

            if (userId <= 0)
            {
                //serilog 24
                _logger.LogWarning(
                  "Delete employee failed. Invalid UserId {UserId}",
                  userId);

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
                //serilog24
                _logger.LogWarning(
                 "Delete failed. Employee not found. EmployeeId {EmployeeId}, UserId {UserId}",
                 id,
                 userId);

                return NotFound();
            }


            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            //serilog24
            _logger.LogInformation(
                "Employee deleted successfully. EmployeeId {EmployeeId}, UserId {UserId}",
                id,
                userId);


            return NoContent();
        }
    }
}


