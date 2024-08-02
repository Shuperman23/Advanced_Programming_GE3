using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExamenCGastos.Models;
using BCrypt.Net;
using ExamenCGastos.Data;
using ExamenCGastos.DTOs;

namespace ExamenCGastos
{
    public class Utilities
    {
        private readonly IConfiguration _configuration;
        public Utilities(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Método para encriptar contraseñas con bcrypt
        public string EncriptarContrasena(string contrasena)
        {
            // Generar el hash de la contraseña usando bcrypt
            return BCrypt.Net.BCrypt.HashPassword(contrasena);
        }

        // Método para verificar contraseñas con bcrypt
        public bool VerificarContrasena(string contrasena, string hash)
        {
            // Verificar la contraseña comparándola con el hash almacenado
            return BCrypt.Net.BCrypt.Verify(contrasena, hash);
        }

        public string generarJWT(Usuario modelo)
        {
            // Crear la información del usuario para el token
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, modelo.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, modelo.Correo!)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Crear detalle del token
            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}
