using LibraryApi.Application.Interfaces;
using LibraryApi.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _userService;
    private readonly IConfiguration _config;

    public LoginController(ILoginService userService, IConfiguration config)
    {
        _userService = userService;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto userDto)
    {
        try
        {
            string login = await _userService.LoginAsync(userDto.Email, userDto.PasswordHash);
            if (login == "Senha Correcta!")
            {
                var token = GenerateToken(userDto.Email, "Admin");

                return Ok(new { Token = token });
            }

            return Unauthorized(new { Message = "Credenciais inválidas" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Erro interno no servidor", Detalhes = ex.Message });
        }
    }

    private string GenerateToken(string username, string role)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
