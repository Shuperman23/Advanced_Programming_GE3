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
    [TypeFilter(typeof(ApiKeyAttribute))]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        //private readonly CGASTOSContext _controlGastosContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductoController(/*CGASTOSContext controlGastosContext*/IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_controlGastosContext = controlGastosContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        // GET: api/Producto/Lista
        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Lista()
        {
            try
            {
                var lista = await _unitOfWork.Producto.GetProductosAsync();
                var listmap = _mapper.Map<List<ProductoDto>>(lista);

                return listmap;
                
                //return StatusCode(StatusCodes.Status200OK, new { value = lista });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> ObtenerPorId(int id)
        {
            try
            {
                var producto = await _unitOfWork.Producto.GetProductoById/*(p => p.Id == id)*/(id);

                if (producto == null)
                {
                    return NotFound();
                }

                var productoById = _mapper.Map<ProductoDto>(producto);
                return productoById;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> Crear(ProductoDto productoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _unitOfWork.Producto.CreateNewProductoAsync (productoDto);

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

        // PUT: api/Producto/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductoDto>> Actualizar(/*int id, [FromBody] Producto producto*/ProductoDto productoDto, int id)
        {
            try
            {
                var response = await _unitOfWork.Producto.UpdateProductoAsync(productoDto);

                if (response != null && response.SpResponse == 1)
                {
                    return Ok();
                }
                else
                    return NotFound();

                /*
                if (id != productoDto.Id)
                {
                    return BadRequest();
                } 
                 _controlGastosContext.Entry(producto).State = EntityState.Modified;
                _unitOfWork.Producto.UpdateProductoAsync(productoDto).State = EntityState.Modified;

                try
                {
                    var response = await _unitOfWork.Producto.UpdateProductoAsync (productoDto);

                    if (response != null && response.SpResponse == 1)
                    {
                        return Ok();
                    }
                    else
                        return NotFound();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!productoDto(id))
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

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductoDto>> Eliminar(int id)
        {
            try
            {
                var producto = await _unitOfWork.Producto.FindByIdAsync(id);
                if (producto == null)
                {
                    return NotFound("No hay datos con el ID indicado");
                }

                _unitOfWork.Producto.Delete(producto);
                await _unitOfWork.SaveChangesAsync();

                return Ok("Registro Eliminado.");
        
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /*private bool ProductoExists(int id)
        {
            return _controlGastosContext.Productos.Any(p => p.Id == id);
        }*/
    }
}
