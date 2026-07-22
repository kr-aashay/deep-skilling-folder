using Microsoft.AspNetCore.Mvc;
using WebApiHandson4.Models;

namespace WebApiHandson4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private static List<Employee> _employees = new List<Employee>
    {
        new Employee { Id = 1, Name = "Alice",   Salary = 60000, Permanent = true,  Department = Department.HR,      DateOfBirth = new DateTime(1990, 5, 15) },
        new Employee { Id = 2, Name = "Bob",     Salary = 80000, Permanent = true,  Department = Department.IT,      DateOfBirth = new DateTime(1988, 3, 22) },
        new Employee { Id = 3, Name = "Charlie", Salary = 75000, Permanent = false, Department = Department.Finance, DateOfBirth = new DateTime(1995, 8, 10) }
    };

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Employee>> Get() => Ok(_employees);

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Employee> Get(int id)
    {
        if (id <= 0) return BadRequest("Invalid employee id");
        var emp = _employees.FirstOrDefault(e => e.Id == id);
        if (emp == null) return BadRequest("Invalid employee id");
        return Ok(emp);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Employee> Post([FromBody] Employee employee)
    {
        if (employee == null || string.IsNullOrWhiteSpace(employee.Name))
            return BadRequest("Invalid employee data.");
        _employees.Add(employee);
        return StatusCode(201, employee);
    }

    // PUT — update employee from request body using [FromBody]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Employee> Put(int id, [FromBody] Employee updated)
    {
        if (id <= 0) return BadRequest("Invalid employee id");

        var emp = _employees.FirstOrDefault(e => e.Id == id);
        if (emp == null) return BadRequest("Invalid employee id");

        emp.Name        = updated.Name;
        emp.Salary      = updated.Salary;
        emp.Permanent   = updated.Permanent;
        emp.Department  = updated.Department;
        emp.DateOfBirth = updated.DateOfBirth;
        emp.Skills      = updated.Skills;

        return Ok(emp);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Delete(int id)
    {
        if (id <= 0) return BadRequest("Invalid employee id");
        var emp = _employees.FirstOrDefault(e => e.Id == id);
        if (emp == null) return BadRequest("Invalid employee id");
        _employees.Remove(emp);
        return Ok($"Employee {id} deleted successfully.");
    }
}
