using ControlGastosG3;
using ExamenCGastos.Data;
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
        private readonly CGASTOSContext _controlGastosContext;

        public InventarioController(CGASTOSContext controlGastosContext)
        {
            _controlGastosContext = controlGastosContext;
        }

        // GET: api/Inventario/Lista
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var inventario = await _controlGastosContext.Inventarios
                    .Include(i => i.Producto) // Incluir la relación con Producto si es necesaria
                    .ToListAsync();

                return Ok(new { value = inventario });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: api/Inventario/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var inventario = await _controlGastosContext.Inventarios
                    .Include(i => i.Producto) // Incluir la relación con Producto si es necesaria
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (inventario == null)
                {
                    return NotFound();
                }

                return Ok(inventario);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: api/Inventario
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Inventario inventario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _controlGastosContext.Inventarios.Add(inventario);
                await _controlGastosContext.SaveChangesAsync();

                return CreatedAtAction("ObtenerPorId", new { id = inventario.Id }, inventario);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // PUT: api/Inventario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Inventario inventario)
        {
            try
            {
                if (id != inventario.Id)
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

                return NoContent();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // DELETE: api/Inventario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var inventario = await _controlGastosContext.Inventarios.FindAsync(id);
                if (inventario == null)
                {
                    return NotFound();
                }

                _controlGastosContext.Inventarios.Remove(inventario);
                await _controlGastosContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private bool InventarioExists(int id)
        {
            return _controlGastosContext.Inventarios.Any(i => i.Id == id);
        }
    }
}
//