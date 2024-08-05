using ExamenCGastos.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExamenCGastos.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;

        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string?> AuthenticateAsync(string username, string password)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var validUsername = jwtSettings["Username"];
            var validPassword = jwtSettings["Password"];

            if (string.IsNullOrEmpty(validUsername) || string.IsNullOrEmpty(validPassword))
            {
                throw new Exception("Username or Password is not configured in the appsettings.json file.");
            }

            if (username == validUsername && password == validPassword)
            {
                return await Task.FromResult(GenerateToken());
            }

            return null;
        }

        private string GenerateToken()
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = jwtSettings["Key"];
            var tokenLifetime = jwtSettings["TokenLifetime"];

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(tokenLifetime))
            {
                throw new Exception("JWT configuration is not properly set in the appsettings.json file.");
            }

            var keyBytes = Encoding.ASCII.GetBytes(key!);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, jwtSettings["Username"]!)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(tokenLifetime.Split(":")[1])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}