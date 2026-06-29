using Microsoft.AspNetCore.Mvc;

namespace VetSysCli.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username == "admin" && request.Password == "123456@")
        {
            return Ok(new { success = true, token = "demo-token", username = request.Username });
        }

        return Unauthorized(new { success = false, message = "Credenciais inválidas" });
    }
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
