using CGASTOSFE.DTOs;
using CGASTOSFE.RestApis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CGASTOSFE.Controllers
{
    [Authorize(Policy = "CustomPolicy")]
    public class ProductoController : Controller
    {
        private readonly ControlGastosAPI _controlGastosAPI;

        public ProductoController(ControlGastosAPI controlGastosAPI)
        {
            _controlGastosAPI = controlGastosAPI;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var productos = await _controlGastosAPI.GetProductosAsync();
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

        public async Task<IActionResult> Details(int id)
        {
            var producto = await _controlGastosAPI.GetProductosAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var proveedores = await _controlGastosAPI.GetProveedoresAsync();
                ViewBag.Proveedores = new SelectList(proveedores, "Id", "Nombre");
                return View();
            }
            catch (Exception ex)
            {
                // Maneja el error (por ejemplo, registrándolo y mostrando un mensaje al usuario)
                ModelState.AddModelError("", "Error al obtener la lista de proveedores.");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ProductoDto productoDto)
        {
            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.PostProductosAsync(productoDto);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "No se pudo crear el producto.");
            }

            return View(productoDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _controlGastosAPI.GetProductosAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }


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
                var success = await _controlGastosAPI.PutProductosAsync(productoDto);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "No se pudo actualizar el producto.");
            }

            return View(productoDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _controlGastosAPI.GetProductosAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _controlGastosAPI.DeleteProductosAsync(id);

            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
