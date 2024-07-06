using AutoMapper;
using Azure;
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
    public class InventarioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InventarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Inventario/Lista
        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<IEnumerable<InventarioDto>>> Lista()
        {
            try
            {
                var inventario = await _unitOfWork.Inventario.GetInventariosAsync();
                var inventarioMap = _mapper.Map<List<InventarioDto>>(inventario);

                return inventarioMap;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: api/Inventario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventarioDto>> ObtenerPorId(int id)
        {
            try
            {
                var inventario = await _unitOfWork.Inventario.GetInventarioById(id);
                if(inventario == null)
                {
                    return NotFound("No hay datos con el ID indicado");
                }

                var inventarioMap = _mapper.Map<InventarioDto>(inventario);
                return inventarioMap;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: api/Inventario
        [HttpPost]
        public async Task<ActionResult<InventarioDto>> Crear(InventarioDto inventarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _unitOfWork.Inventario.CreateNewInventarioAsync(inventarioDto);

                if (response != null && response.SpResponse == 1)
                {
                    return Ok();
                }
                else
                    return NotFound("No hay datos con el ID indicado");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // PUT: api/Inventario/5
        [HttpPut]
        public async Task<ActionResult<InventarioDto>> Actualizar(InventarioDto inventarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _unitOfWork.Inventario.UpdateInventarioAsync(inventarioDto);

                if (response != null && response.SpResponse == 1)
                {
                    return Ok();
                }
                else
                    return NotFound("No hay datos con el ID indicado");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // DELETE: api/Inventario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InventarioDto>> Eliminar(int id)
        {
            try
            {
                var inventario = await _unitOfWork.Inventario.FindByIdAsync(id);

                if (inventario == null)
                {  
                    return NotFound("No hay datos con el ID indicado"); 
                }

                _unitOfWork.Inventario.Delete(inventario);
                await _unitOfWork.SaveChangesAsync();

                return Ok("Registro Eliminado.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
