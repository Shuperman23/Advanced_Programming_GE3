using ExamenCGastos.DTOs;
using ExamenCGastos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamenCGastos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginRequest)
        {
            var token = await _unitOfWork.Auth.AuthenticateAsync(loginRequest.Correo, loginRequest.Clave);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }
    }
}