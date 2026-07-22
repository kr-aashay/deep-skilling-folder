namespace JwtAuthMicroservice.Models;

// Question 1: User login request model
public class LoginModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
