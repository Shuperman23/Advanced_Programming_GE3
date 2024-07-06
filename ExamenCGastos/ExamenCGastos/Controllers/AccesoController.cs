using ExamenCGastos.Data;
using ExamenCGastos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExamenCGastos.DTOs;
using Microsoft.EntityFrameworkCore;
using ExamenCGastos.Interfaces;

namespace ExamenCGastos.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly CGASTOSContext _controlGastosContext;
        private readonly Utilities _utilidades;
        private readonly ConfiguracionEmail _configuracionesEmail;

        public AccesoController(CGASTOSContext controlGastosContext, Utilities utilidades, ConfiguracionEmail configuracionesEmail)
        {
            _controlGastosContext = controlGastosContext;
            _utilidades = utilidades;
            _configuracionesEmail = configuracionesEmail;
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(UsuarioDTO objeto)
        {
            try
            {
                var existingUser = await _controlGastosContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == objeto.Correo);

                if (existingUser != null)
                {
                    return BadRequest("El correo ya está en uso.");
                }

                var modeloUsuario = new Usuario
                {
                    Nombre = objeto.Nombre,
                    Correo = objeto.Correo,
                    Clave = _utilidades.EncriptarContrasena(objeto.Clave) // Usar bcrypt para encriptar la contraseña
                };

                await _controlGastosContext.Usuarios.AddAsync(modeloUsuario);
                await _controlGastosContext.SaveChangesAsync();

                if (modeloUsuario.IdUsuario != 0)
                {
                    string emailBody = $@"
             <html>
              <body>
               <h1> Bienvenido, {modeloUsuario.Nombre} </h1>
              <p> Tu contraseña es : <strong> {objeto.Clave} </strong></p>
            </body>
           </html>";

                    await _configuracionesEmail.SendEmailAsync(modeloUsuario.Correo, "Registro Exitoso", emailBody);

                    return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            try
            {
                var usuarioEncontrado = await _controlGastosContext.Usuarios
                    .Where(u => u.Correo == objeto.Correo)
                    .FirstOrDefaultAsync();

                if (usuarioEncontrado == null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, message = "No se encontró ese usuario." });
                }


                if (usuarioEncontrado == null || usuarioEncontrado.Clave == null || !_utilidades.VerificarContrasena(objeto.Clave, usuarioEncontrado.Clave)) // Usar bcrypt para verificar la contraseña
                {
                    return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, message = "Contraseña Incorrecta." });
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.generarJWT(usuarioEncontrado) });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}