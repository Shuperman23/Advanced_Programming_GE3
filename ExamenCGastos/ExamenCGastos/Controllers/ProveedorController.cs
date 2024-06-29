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
    public class ProveedoresController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProveedoresController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Proveedores/ListaProveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetProveedor()
        {
            var proveedores = await _unitOfWork.Proveedor.GetProveedoresAsync();

            var proveedoresMap = _mapper.Map<List<ProveedorDto>>(proveedores);
            return proveedoresMap;
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorDto>> ObtenerPorId(int id)
        {
            try
            {
                var proveedores = await _unitOfWork.Proveedor.GetProveedorById(id);
                if (proveedores == null)
                {
                    return NotFound();
                }

                var proveedoresById = _mapper.Map<ProveedorDto>(proveedores);
                return proveedoresById;
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: api/Proveedores
        [HttpPost]
        public async Task<ActionResult<ProveedorDto>> Crear(ProveedorDto proveedorDto)
        {
            try
            {
                var response = await _unitOfWork.Proveedor.CreateNewProveedorAsync(proveedorDto);
                if (response != null && response.SpResponse == 1)
                {
                    return Ok();
                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // PUT: api/Proveedores/5
        [HttpPut]
        public async Task<ActionResult<ProveedorDto>> Actualizar(ProveedorDto proveedorDto)
        {
            try
            {
                var response = await _unitOfWork.Proveedor.UpdateProveedorAsync(proveedorDto);
                if (response != null && response.SpResponse == 1)
                {
                    return Ok();
                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProveedorDto>> Eliminar(int id)
        {
            try
            {
                var proveedores = await _unitOfWork.Proveedor.FindByIdAsync(id);
                if (proveedores == null)
                {
                    return NotFound("No hay datos con el ID indicado");
                }

                _unitOfWork.Proveedor.Delete(proveedores);
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
