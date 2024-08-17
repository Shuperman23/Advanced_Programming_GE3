using CGASTOSFE.DTOs;
using CGASTOSFE.RestApis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CGASTOSFE.Controllers
{
    // Requiere que el usuario esté autorizado bajo la política "CustomPolicy".
    [Authorize(Policy = "CustomPolicy")]
    public class ProductoController : Controller
    {
        private readonly ControlGastosAPI _controlGastosAPI;

        // Constructor
        public ProductoController(ControlGastosAPI controlGastosAPI)
        {
            _controlGastosAPI = controlGastosAPI;
        }

        //// Acción para mostrar la lista de productos.
        public async Task<IActionResult> Index()
        {
            try
            {
                var productos = await _controlGastosAPI.GetProductosAsync();// Obtener productos desde la API
                return View(productos);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Manejar caso específico de autorización
                TempData["ErrorMessage"] = ex.Message; // Guardar mensaje de error en TempData
                return RedirectToAction("Index", "Home"); // Redirigir a la vista de inicio
            }
            catch (Exception ex)
            {
                // Manejar excepciones generales
                TempData["ErrorMessage"] = "Ocurrió un error al obtener los productos. Intenta nuevamente más tarde."; // Guardar mensaje de error en TempData
                return RedirectToAction("Index", "Home"); // Redirigir a la vista de inicio
            }
        }

        // Acción para mostrar los detalles de un producto.
        public async Task<IActionResult> Details(int id)
        {
            var producto = await _controlGastosAPI.GetProductosAsync(id);// Obtener producto desde la API

            if (producto == null)
            {
                return NotFound();// Producto no encontrado
            }

            return View(producto);
        }

        // Acción para crear un nuevo producto.
        public async Task<IActionResult> Create()
        {
            try
            {
                var proveedores = await _controlGastosAPI.GetProveedoresAsync();// Obtener proveedores desde la API
                ViewBag.Proveedores = new SelectList(proveedores, "Id", "Nombre");// Crear lista de proveedores
                return View();
            }
            catch (Exception ex)
            {
                // Maneja el error (por ejemplo, registrándolo y mostrando un mensaje al usuario)
                ModelState.AddModelError("", "Error al obtener la lista de proveedores.");
                return View();
            }
        }

        // Acción para crear un nuevo producto.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ProductoDto productoDto)
        {
            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.PostProductosAsync(productoDto);// Crear producto en la API

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "No se pudo crear el producto.");// Error al crear el producto
            }

            var productos = await _controlGastosAPI.GetProductosAsync();// Obtener productos desde la API
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");// Crear lista de productos
            return View(productoDto);// Retornar vista con el modelo
        }

        // Acción para editar un producto.
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var producto = await _controlGastosAPI.GetProductosAsync(id);// Obtener producto desde la API
                if (producto == null)
                {
                    return NotFound();
                }

                var proveedores = await _controlGastosAPI.GetProveedoresAsync();// Obtener proveedores desde la API
                ViewBag.Proveedores = new SelectList(proveedores, "Id", "Nombre", producto.ProveedorId);// Crear lista de proveedores

                return View(producto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al obtener la lista de proveedores.");
                return View();
            }
        }

        // Acción para editar un producto.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, ProductoDto productoDto)
        {
            if (id != productoDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.PutProductosAsync(productoDto);// Actualizar producto en la API

                if (success)
                {
                    return RedirectToAction(nameof(Index));// Redirigir a la lista de productos
                }
                ModelState.AddModelError("", "No se pudo actualizar el producto.");// Error al actualizar el producto
            }

            var productos = await _controlGastosAPI.GetProductosAsync();    // Obtener productos desde la API
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");// Crear lista de productos
            return View(productoDto);
        }

        // Acción para eliminar un producto.
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _controlGastosAPI.GetProductosAsync(id);// Obtener producto desde la API

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // Acción para eliminar un producto.

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _controlGastosAPI.DeleteProductosAsync(id);// Eliminar producto en la API

            if (success)
            {
                return RedirectToAction(nameof(Index));// Redirigir a la lista de productos
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
