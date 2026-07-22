using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthMicroservice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthMicroservice.Controllers;

// Question 1: JWT token generation on login
// Question 3: Role claim added to support role-based authorization
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    // POST api/auth/login
    // Returns JWT token on valid credentials
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        if (IsValidUser(model))
        {
            var token = GenerateJwtToken(model.Username);
            return Ok(new { Token = token });
        }
        return Unauthorized(new { Message = "Invalid username or password." });
    }

    // Hardcoded user validation — replace with DB lookup in production
    private bool IsValidUser(LoginModel model)
    {
        return (model.Username == "admin" && model.Password == "admin123") ||
               (model.Username == "user"  && model.Password == "user123");
    }

    private string GenerateJwtToken(string username)
    {
        // Question 3: Add Role claim so [Authorize(Roles = "Admin")] works
        var role = username == "admin" ? "Admin" : "User";

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer:             _config["Jwt:Issuer"],
            audience:           _config["Jwt:Audience"],
            claims:             claims,
            expires:            DateTime.Now.AddMinutes(
                                    Convert.ToDouble(_config["Jwt:DurationInMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
