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
    [TypeFilter(typeof(ApiKeyAttribute))]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly CGASTOSContext _controlGastosContext;

        public InventarioController(/*CGASTOSContext controlGastosContext*/ IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_controlGastosContext = controlGastosContext;
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

                /*var inventario = await _controlGastosContext.Inventarios
                    .Include(i => i.Producto) // Incluir la relación con Producto si es necesaria
                    .ToListAsync();

                return Ok(new { value = inventario });*/
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
                    return NotFound();
                }

                var inventarioMap = _mapper.Map<InventarioDto>(inventario);
                return inventarioMap;
                
                /*var inventario = await _controlGastosContext.Inventarios
                    .Include(i => i.Producto) // Incluir la relación con Producto si es necesaria
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (inventario == null)
                {
                    return NotFound();
                }

                return Ok(inventario);*/
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: api/Inventario
        [HttpPost]
        public async Task<ActionResult<InventarioDto>> Crear(/*[FromBody] Inventario inventario*/InventarioDto inventarioDto)
        {
            try
            {
                var response = await _unitOfWork.Inventario.CreateNewInventarioAsync(inventarioDto);

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

                _controlGastosContext.Inventarios.Add(inventario);
                await _controlGastosContext.SaveChangesAsync();

                return CreatedAtAction("ObtenerPorId", new { id = inventario.Id }, inventario);*/
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // PUT: api/Inventario/5
        [HttpPut("{id}")]
        public async Task<ActionResult<InventarioDto>> Actualizar(InventarioDto inventarioDto/*int id, [FromBody] Inventario inventario*/)
        {
            try
            {
                var response = await _unitOfWork.Inventario.UpdateInventarioAsync(inventarioDto);

                if (response != null && response.SpResponse == 1)
                {
                    return Ok();
                }
                else
                    return NotFound();

                /*if (id != inventario.Id)
                {
                    return BadRequest();
                }

                _controlGastosContext.Entry(inventario).State = EntityState.Modified;

                try
                {
                    await _controlGastosContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioExists(id))
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

                /*var inventario = await _controlGastosContext.Inventarios.FindAsync(id);
                if (inventario == null)
                {
                    return NotFound();
                }

                _controlGastosContext.Inventarios.Remove(inventario);
                await _controlGastosContext.SaveChangesAsync();

                return NoContent();*/
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /*private bool InventarioExists(int id)
        {
            return _controlGastosContext.Inventarios.Any(i => i.Id == id);
        }*/
    }
}
//