using Employee.Data.Data;
using Employee.BusinessLogic;
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

        private readonly IEmployeeService _employeeService;

        private readonly IValidator<Employee1> _validator;

        private readonly ILogger<EmployeesController> _logger;



        // CONSTRUCTOR

        public EmployeesController(
    IEmployeeService employeeService,
    IValidator<Employee1> validator,

    ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;

            _validator = validator;

            _logger = logger;
        }


        //get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee1>>>
            GetEmployees(
                [FromQuery] int userId)
        {
            _logger.LogInformation(
                "Getting employees for UserId {UserId}",
                userId);

            if (userId <= 0)
            {
                _logger.LogWarning(
                   "Get employees failed. Invalid UserId {UserId}",
                   userId);

                return BadRequest(
                    "Invalid UserId.");
            }



            var employees = await _employeeService
    .GetAllAsync(userId);

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
            _logger.LogInformation(
              "Getting EmployeeId {EmployeeId} for UserId {UserId}",
              id,
              userId);


            var employee = await _employeeService
    .GetByIdAsync(id, userId);


            if (employee == null)
            {
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
        public async Task<ActionResult<Employee1>> CreateEmployee(
            [FromQuery] int userId,
            Employee1 employee)
        {
            _logger.LogInformation(
                "Creating employee for UserId {UserId}",
                userId);

            if (userId <= 0)
            {
                return BadRequest("Invalid UserId.");
            }

            var validationResult =
                await _validator.ValidateAsync(employee);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _employeeService
                .AddAsync(employee, userId);

            return CreatedAtAction(
                nameof(GetEmployee),
                new
                {
                    id = result.Id,
                    userId = userId
                },
                result);
        }



        //Update

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(
    int id,
    [FromQuery] int userId,
    Employee1 employee)
        {
            _logger.LogInformation(
                "Updating EmployeeId {EmployeeId} for UserId {UserId}",
                id,
                userId);

            if (userId <= 0)
            {
                return BadRequest("Invalid UserId.");
            }

            if (id != employee.Id)
            {
                return BadRequest(
                    "Employee ID does not match.");
            }

            // Fluet Validation
            var validationResult =
                await _validator.ValidateAsync(employee);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _employeeService
                .UpdateAsync(employee, userId);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        //Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(
    int id,
    [FromQuery] int userId)
        {
            _logger.LogInformation(
                "Deleting EmployeeId {EmployeeId} for UserId {UserId}",
                id,
                userId);

            if (userId <= 0)
            {
                return BadRequest("Invalid UserId.");
            }

            var result = await _employeeService
                .DeleteAsync(id, userId);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}


