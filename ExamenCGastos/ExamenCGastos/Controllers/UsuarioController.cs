using AutoMapper;
using ExamenCGastos.Data;
using ExamenCGastos.DTOs;
using ExamenCGastos.Interfaces;
using ExamenCGastos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamenCGastos.Controllers
{
    //[TypeFilter(typeof(ApiKeyAttribute))]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsuarioController(/*CGASTOSContext controlGastosContext*/IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_controlGastosContext = controlGastosContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

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
