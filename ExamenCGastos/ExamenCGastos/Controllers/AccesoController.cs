using ExamenCGastos.Data;
using ExamenCGastos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExamenCGastos.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public AccesoController(IUnitOfWork unitOfWork, IMapper mapper, Utilities utilidades, ConfiguracionEmail configuracionesEmail)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _utilidades = utilidades;
            _configuracionesEmail = configuracionesEmail;
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(UsuarioDTO objeto)
        {
            try
            {
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

                if (usuarioEncontrado == null || usuarioEncontrado.Clave == null || !_utilidades.VerificarContrasena(objeto.Clave, usuarioEncontrado.Clave)) // Usar bcrypt para verificar la contraseña
                {
                    return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _unitOfWork.Usuario.GetUsuarioAsync();
            var usuarioDtos = _mapper.Map<List<UsuarioDTO>>(usuarios);
            return usuarioDtos;
        }

        // GET: api/Acceso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _unitOfWork.Usuario.GetUsuarioById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
            return usuarioDto;
        }

        // PUT: api/Acceso/5
        [HttpPut]
        public async Task<IActionResult> PutUsuario(UsuarioDTO usuarioDto)
        {
            var response = await _unitOfWork.Usuario.UpdateUsuarioAsync(usuarioDto);

            if (response != null && response.SpResponse == 1)
            {
                return Ok();
            }
            else
                return NotFound();
        }

        // POST: api/Acceso
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario(UsuarioDTO usuarioDto)
        {
            var response = await _unitOfWork.Usuario.CreateNewUsuarioAsync(usuarioDto);

            if (response != null && response.SpResponse == 1)
            {
                return Ok();
            }
            else
                return NotFound();
        }

        // DELETE: api/Acceso/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _unitOfWork.Usuario.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _unitOfWork.Usuario.Delete(usuario);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
    }
}
