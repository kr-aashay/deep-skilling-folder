using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApiHandson5.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]  // No auth needed to get the token
public class AuthController : ControllerBase
{
    // GET api/Auth
    // Generates a JWT token for userId=1 with role Admin
    [HttpGet]
    public ActionResult<string> Get()
    {
        var token = GenerateJSONWebToken(1, "Admin");
        return Ok(new { Token = token });
    }

    // Generates a JWT token with userId and userRole as claims
    private string GenerateJSONWebToken(int userId, string userRole)
    {
        var securityKey  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysuperdupersecret"));
        var credentials  = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, userRole),
            new Claim("UserId", userId.ToString())
        };

        // expires: set to 2 minutes to test JWT expiration
        var token = new JwtSecurityToken(
            issuer:             "mySystem",
            audience:           "myUsers",
            claims:             claims,
            expires:            DateTime.Now.AddMinutes(10),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
