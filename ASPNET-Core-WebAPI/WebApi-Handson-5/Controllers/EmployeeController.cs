using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiHandson5.Models;

namespace WebApiHandson5.Controllers;

[Route("api/[controller]")]
[ApiController]
// Authorize attribute — checks for valid Bearer JWT token in Authorization header
// Roles = "POC,Admin" — token must have either POC or Admin role
// Test 1: Use Authorize(Roles = "POC") → 401 Unauthorized (token has Admin, not POC)
// Test 2: Use Authorize(Roles = "POC,Admin") → 200 OK (token has Admin)
[Authorize(Roles = "POC,Admin")]
public class EmployeeController : ControllerBase
{
    private static List<Employee> _employees = new List<Employee>
    {
        new Employee { Id = 1, Name = "Alice",   Salary = 60000, Permanent = true,  Department = Department.HR,      DateOfBirth = new DateTime(1990, 5, 15) },
        new Employee { Id = 2, Name = "Bob",     Salary = 80000, Permanent = true,  Department = Department.IT,      DateOfBirth = new DateTime(1988, 3, 22) },
        new Employee { Id = 3, Name = "Charlie", Salary = 75000, Permanent = false, Department = Department.Finance, DateOfBirth = new DateTime(1995, 8, 10) }
    };

    // GET api/Employee — requires valid JWT with Admin or POC role
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<List<Employee>> Get()
    {
        return Ok(_employees);
    }

    // GET api/Employee/1
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<Employee> Get(int id)
    {
        if (id <= 0) return BadRequest("Invalid employee id");
        var emp = _employees.FirstOrDefault(e => e.Id == id);
        if (emp == null) return BadRequest("Invalid employee id");
        return Ok(emp);
    }

    // PUT api/Employee/1
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

        return Ok(emp);
    }

    // DELETE api/Employee/1
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Delete(int id)
    {
        if (id <= 0) return BadRequest("Invalid employee id");
        var emp = _employees.FirstOrDefault(e => e.Id == id);
        if (emp == null) return BadRequest("Invalid employee id");
        _employees.Remove(emp);
        return Ok($"Employee {id} deleted.");
    }
}
