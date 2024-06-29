using AutoMapper;
using ControlGastosG3;
using ExamenCGastos.Data;
using ExamenCGastos.DTOs;
using ExamenCGastos.Interfaces;
using ExamenCGastos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamenCGastos.Controllers
{
    [TypeFilter(typeof(ApiKeyAttribute))]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        //private readonly CGASTOSContext _controlGastosContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProveedoresController(/*CGASTOSContext controlGastosContext*/IMapper mapper, IUnitOfWork unitOfWork)
        {
            //_controlGastosContext = controlGastosContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Proveedores/ListaProveedores
        [HttpGet]
        [Route("ListaProveedores")]
        public async Task<ActionResult<IEnumerable<ProveedorDto>>> ListaProveedores()
        {
            try
            {
                var proveedores = await _unitOfWork.Proveedor.GetProveedoresAsync();
                var proveedoresMap = _mapper.Map<List<ProveedorDto>>(proveedores);
                return proveedoresMap;
                /*var proveedores = await _controlGastosContext.Proveedores.ToListAsync();
                return StatusCode(StatusCodes.Status200OK, new { value = proveedores });*/
            }
            catch (Exception ex)
            {

                throw;
            }
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
                
                
                
                /*var proveedor = await _controlGastosContext.Proveedores.FindAsync(id);

                if (proveedor == null)
                {
                    return NotFound();
                }

                return Ok(proveedor);*/
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


                /*if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _controlGastosContext.Proveedores.Add(proveedor);
                await _controlGastosContext.SaveChangesAsync();

                return CreatedAtAction("ObtenerPorId", new { id = proveedor.Id }, proveedor);*/
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // PUT: api/Proveedores/5
        [HttpPut("{id}")]
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



                /*if (id != proveedor.Id)
                {
                    return BadRequest();
                }

                _controlGastosContext.Entry(proveedor).State = EntityState.Modified;

                try
                {
                    await _controlGastosContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();*/
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
                
                /*var proveedor = await _controlGastosContext.Proveedores.FindAsync(id);
                if (proveedor == null)
                {
                    return NotFound();
                }

                _controlGastosContext.Proveedores.Remove(proveedor);
                await _controlGastosContext.SaveChangesAsync();

                return NoContent();*/
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /*private bool ProveedorExists(int id)
        {
            return _controlGastosContext.Proveedores.Any(e => e.Id == id);
        }*/
    }
}
