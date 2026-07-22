using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApiHandson3.Models;
using WebApiHandson3.Filters;

namespace WebApiHandson3.Controllers;

[Route("api/[controller]")]
[ApiController]
[CustomAuthFilter]          // Intercepts all requests to check Authorization header
[ServiceFilter(typeof(CustomExceptionFilter))]  // Catches unhandled exceptions
public class EmployeeController : ControllerBase
{
    // GET api/Employee
    // Returns full list of employees with custom model
    [HttpGet]
    [AllowAnonymous]  // Bypasses auth for this specific action
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<List<Employee>> GetStandard()
    {
        // Uncomment to test CustomExceptionFilter:
        // throw new Exception("Test exception from GET action method");

        var employees = GetStandardEmployeeList();
        return Ok(employees);
    }

    // POST api/Employee
    // [FromBody] reads Employee object from request body (not query string)
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Employee> Post([FromBody] Employee employee)
    {
        if (employee == null || string.IsNullOrWhiteSpace(employee.Name))
            return BadRequest("Invalid employee data.");

        return StatusCode(201, employee);
    }

    // PUT api/Employee/1
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Employee> Put(int id, [FromBody] Employee employee)
    {
        if (id <= 0)
            return BadRequest("Invalid employee id");

        var list = GetStandardEmployeeList();
        var existing = list.FirstOrDefault(e => e.Id == id);
        if (existing == null)
            return BadRequest("Invalid employee id");

        existing.Name       = employee.Name;
        existing.Salary     = employee.Salary;
        existing.Permanent  = employee.Permanent;
        existing.Department = employee.Department;

        return Ok(existing);
    }

    // Private method that returns hardcoded employee list
    private List<Employee> GetStandardEmployeeList()
    {
        return new List<Employee>
        {
            new Employee
            {
                Id          = 1,
                Name        = "Alice",
                Salary      = 60000,
                Permanent   = true,
                Department  = Department.HR,
                DateOfBirth = new DateTime(1990, 5, 15),
                Skills      = new List<Skill>
                {
                    new Skill { Id = 1, Name = "Communication" },
                    new Skill { Id = 2, Name = "Recruitment" }
                }
            },
            new Employee
            {
                Id          = 2,
                Name        = "Bob",
                Salary      = 80000,
                Permanent   = true,
                Department  = Department.IT,
                DateOfBirth = new DateTime(1988, 3, 22),
                Skills      = new List<Skill>
                {
                    new Skill { Id = 3, Name = "C#" },
                    new Skill { Id = 4, Name = "Azure" }
                }
            },
            new Employee
            {
                Id          = 3,
                Name        = "Charlie",
                Salary      = 75000,
                Permanent   = false,
                Department  = Department.Finance,
                DateOfBirth = new DateTime(1995, 8, 10),
                Skills      = new List<Skill>
                {
                    new Skill { Id = 5, Name = "Accounting" },
                    new Skill { Id = 6, Name = "Excel" }
                }
            }
        };
    }
}
