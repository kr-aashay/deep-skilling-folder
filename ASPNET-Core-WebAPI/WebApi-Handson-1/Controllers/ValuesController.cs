using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers;

// Controller inherits from ControllerBase (equivalent of ApiController in .NET 4.5)
// [Route] defines the URL pattern: api/values
// [ApiController] enables automatic model validation, binding source inference etc.

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    // In-memory data store to simulate a database
    private static List<string> _values = new List<string>
    {
        "Value1",
        "Value2",
        "Value3"
    };

    // GET api/values
    // Returns all values — HttpStatusCode: 200 OK
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return Ok(_values);
    }

    // GET api/values/1
    // Returns a single value by index — HttpStatusCode: 200 OK or 404 NotFound
    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {
        if (id < 0 || id >= _values.Count)
            return NotFound($"No value found at index {id}.");

        return Ok(_values[id]);
    }

    // POST api/values
    // Adds a new value — HttpStatusCode: 201 Created or 400 BadRequest
    [HttpPost]
    public ActionResult Post([FromBody] string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return BadRequest("Value cannot be empty.");

        _values.Add(value);
        return StatusCode(201, $"'{value}' added successfully.");
    }

    // PUT api/values/1
    // Updates an existing value — HttpStatusCode: 200 OK or 400 BadRequest or 404 NotFound
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] string value)
    {
        if (id < 0 || id >= _values.Count)
            return NotFound($"No value found at index {id}.");

        if (string.IsNullOrWhiteSpace(value))
            return BadRequest("Value cannot be empty.");

        _values[id] = value;
        return Ok($"Index {id} updated to '{value}'.");
    }

    // DELETE api/values/1
    // Deletes a value — HttpStatusCode: 200 OK or 404 NotFound
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        if (id < 0 || id >= _values.Count)
            return NotFound($"No value found at index {id}.");

        _values.RemoveAt(id);
        return Ok($"Value at index {id} deleted successfully.");
    }
}
