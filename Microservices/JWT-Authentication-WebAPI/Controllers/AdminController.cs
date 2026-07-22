using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthMicroservice.Controllers;

// Question 3: Role-based authorization
// GET api/admin/dashboard — only accessible with a token that has Role = "Admin"
// Login as "admin" / "admin123" to get an Admin token
// Login as "user" / "user123" → 403 Forbidden (valid token but wrong role)
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    [HttpGet("dashboard")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAdminDashboard()
    {
        return Ok(new { Message = "Welcome to the admin dashboard.", Role = "Admin" });
    }
}
