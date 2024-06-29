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
    public class ProveedoresController : ControllerBase
    {
        private readonly CGASTOSContext _controlGastosContext;

        public ProveedoresController(CGASTOSContext controlGastosContext)
        {
            _controlGastosContext = controlGastosContext;
        }

        // GET: api/Proveedores/ListaProveedores
        [HttpGet]
        [Route("ListaProveedores")]
        public async Task<IActionResult> ListaProveedores()
        {
            try
            {
                var proveedores = await _controlGastosContext.Proveedores.ToListAsync();
                return StatusCode(StatusCodes.Status200OK, new { value = proveedores });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var proveedor = await _controlGastosContext.Proveedores.FindAsync(id);

                if (proveedor == null)
                {
                    return NotFound();
                }

                return Ok(proveedor);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: api/Proveedores
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Proveedor proveedor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _controlGastosContext.Proveedores.Add(proveedor);
                await _controlGastosContext.SaveChangesAsync();

                return CreatedAtAction("ObtenerPorId", new { id = proveedor.Id }, proveedor);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // PUT: api/Proveedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Proveedor proveedor)
        {
            try
            {
                if (id != proveedor.Id)
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

                return NoContent();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var proveedor = await _controlGastosContext.Proveedores.FindAsync(id);
                if (proveedor == null)
                {
                    return NotFound();
                }

                _controlGastosContext.Proveedores.Remove(proveedor);
                await _controlGastosContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private bool ProveedorExists(int id)
        {
            return _controlGastosContext.Proveedores.Any(e => e.Id == id);
        }
    }
}
