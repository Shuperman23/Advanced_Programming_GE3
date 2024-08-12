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
        public async Task<IActionResult> Login(LoginDto objeto)
        {
            try
            {
                var usuarioEncontrado = await _controlGastosContext.Usuarios
                    .Where(u => u.Correo == objeto.Correo)
                    .FirstOrDefaultAsync();

                if (usuarioEncontrado == null || !_utilidades.VerificarContrasena(objeto.Clave, usuarioEncontrado.Clave))
                {
                    return Ok(new ApiRequestResultDto<string>
                    {
                        Success = false,
                        Message = "Usuario o contraseña incorrectos."
                    });
                }

                var token = _utilidades.generarJWT(usuarioEncontrado);
                return Ok(new ApiRequestResultDto<string>
                {
                    Success = true,
                    Result = token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiRequestResultDto<string>
                {
                    Success = false,
                    Message = "Ocurrió un error interno."
                });
            }
        }

        [HttpPost]
        [Route("VerificarUsuario")]
        public async Task<IActionResult> VerificarUsuario(VerificarUsuarioDTO objeto)
        {
            try
            {
                var usuarioEncontrado = await _controlGastosContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == objeto.Correo);

                if (usuarioEncontrado == null)
                {
                    return BadRequest("No se encontró ese usuario.");
                }

                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, message = "Usuario encontrado." });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPut]
        [Route("CambiarContrasena")]
        public async Task<IActionResult> CambiarContrasena(CambiarContrasenaDTO objeto)
        {
            try
            {
                var usuarioEncontrado = await _controlGastosContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == objeto.Correo);

                if (usuarioEncontrado == null)
                {
                    return BadRequest("No se encontró ese usuario.");
                }

                usuarioEncontrado.Clave = _utilidades.EncriptarContrasena(objeto.NuevaClave); // Usar bcrypt para encriptar la nueva contraseña

                _controlGastosContext.Usuarios.Update(usuarioEncontrado);
                await _controlGastosContext.SaveChangesAsync();

                string emailBody = $@"
         <html>
          <body>
           <h1> Hola, {usuarioEncontrado.Nombre} </h1>
          <p> Tu contraseña ha sido actualizada. </p>
        </body>
       </html>";

                await _configuracionesEmail.SendEmailAsync(usuarioEncontrado.Correo, "Contraseña Actualizada", emailBody);

                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
    
