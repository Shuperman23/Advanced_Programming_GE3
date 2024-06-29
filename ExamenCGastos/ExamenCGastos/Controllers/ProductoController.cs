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
    public class ProductoController : ControllerBase
    {
        private readonly CGASTOSContext _controlGastosContext;

        public ProductoController(CGASTOSContext controlGastosContext)
        {
            _controlGastosContext = controlGastosContext;
        }

        // GET: api/Producto/Lista
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var lista = await _controlGastosContext.Productos.ToListAsync();
                return StatusCode(StatusCodes.Status200OK, new { value = lista });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var producto = await _controlGastosContext.Productos
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (producto == null)
                {
                    return NotFound();
                }

                return Ok(producto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Producto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _controlGastosContext.Productos.Add(producto);
                await _controlGastosContext.SaveChangesAsync();

                return CreatedAtAction("ObtenerPorId", new { id = producto.Id }, producto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // PUT: api/Producto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Producto producto)
        {
            try
            {
                if (id != producto.Id)
                {
                    return BadRequest();
                }

                _controlGastosContext.Entry(producto).State = EntityState.Modified;

                try
                {
                    await _controlGastosContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(id))
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

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var producto = await _controlGastosContext.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound();
                }

                _controlGastosContext.Productos.Remove(producto);
                await _controlGastosContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private bool ProductoExists(int id)
        {
            return _controlGastosContext.Productos.Any(p => p.Id == id);
        }
    }
}
