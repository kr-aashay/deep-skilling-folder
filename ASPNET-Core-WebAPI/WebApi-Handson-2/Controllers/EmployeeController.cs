using Microsoft.AspNetCore.Mvc;

namespace WebApiWithSwagger.Controllers;

// Model
public class Employee
{
    public int    Id         { get; set; }
    public string Name       { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public double Salary     { get; set; }
}

// Step 3: Route changed from "Employee" to "Emp" — accessible at api/Emp
[Route("api/Emp")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private static List<Employee> _employees = new List<Employee>
    {
        new Employee { Id = 1, Name = "Alice",   Department = "HR",      Salary = 60000 },
        new Employee { Id = 2, Name = "Bob",     Department = "IT",      Salary = 80000 },
        new Employee { Id = 3, Name = "Charlie", Department = "Finance", Salary = 75000 }
    };

    // GET api/Emp
    // Returns all employees
    // ProducesResponseType documents what status codes this method returns (visible in Swagger)
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Employee>> Get()
    {
        return Ok(_employees);
    }

    // GET api/Emp/1
    [HttpGet("{id}", Name = "GetEmployeeById")]  // Name attribute gives a friendly name to the route
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Employee> Get(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
            return NotFound($"Employee with ID {id} not found.");

        return Ok(employee);
    }

    // POST api/Emp
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Post([FromBody] Employee employee)
    {
        if (employee == null || string.IsNullOrWhiteSpace(employee.Name))
            return BadRequest("Invalid employee data.");

        _employees.Add(employee);
        return StatusCode(201, employee);
    }

    // PUT api/Emp/1
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Put(int id, [FromBody] Employee updated)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
            return NotFound($"Employee with ID {id} not found.");

        employee.Name       = updated.Name;
        employee.Department = updated.Department;
        employee.Salary     = updated.Salary;

        return Ok(employee);
    }

    // DELETE api/Emp/1
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
            return NotFound($"Employee with ID {id} not found.");

        _employees.Remove(employee);
        return Ok($"Employee {id} deleted.");
    }
}
