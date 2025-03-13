using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Interfaces;
using LibraryApi.Application.Interfaces;

public class LoginService : ILoginService
{
    private readonly IRepository<User> _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    // Chave secreta e configurações do JWT (de preferência usar IConfiguration para obter do appsettings.json)
    private const string Key = "P0FWi2BjoB86bIL8AODUDlcUVwRJQAcxPWKtbuGoiwfqoR7ufcz75pSXYlZsUOor";
    private const string Issuer = "https://localhost:7076";
    private const string Audience = "https://localhost:7076";

    public LoginService(IRepository<User> userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task<string> GenerateToken(string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, email) // Adicionando Claim de nome
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = Issuer,
            Audience = Audience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = (await _userRepository.GetAllAsync())
            .FirstOrDefault(u => u.Email == email);

        if (user == null)
            throw new Exception("Usuário não encontrado!");

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        if (passwordVerificationResult != PasswordVerificationResult.Success)
            return "Senha incorreta!";

        return "Senha Correcta!";
    }
}
