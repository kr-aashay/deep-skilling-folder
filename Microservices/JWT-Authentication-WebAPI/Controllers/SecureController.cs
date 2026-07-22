using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthMicroservice.Controllers;

// Question 2: Restrict endpoint with [Authorize]
// GET api/secure/data — requires valid JWT token in Authorization header
// Without token → 401 Unauthorized
// With valid token → 200 OK
[ApiController]
[Route("api/[controller]")]
public class SecureController : ControllerBase
{
    [HttpGet("data")]
    [Authorize]
    public IActionResult GetSecureData()
    {
        var username = User.Identity?.Name;
        return Ok(new { Message = "This is protected data.", User = username });
    }
}
