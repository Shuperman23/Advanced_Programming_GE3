using CGASTOSFE.DTOs;
using CGASTOSFE.RestApis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CGASTOSFE.Controllers
{
    [Authorize(Policy = "CustomPolicy")]
    public class ProveedorController : Controller
    {
        private readonly ControlGastosAPI _controlGastosAPI;

        public ProveedorController(ControlGastosAPI controlGastosAPI)
        {
            _controlGastosAPI = controlGastosAPI;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var proveedores = await _controlGastosAPI.GetProveedoresAsync();
                return View(proveedores);
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
            var proveedor = await _controlGastosAPI.GetProveedoresAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ProveedorDto proveedorDto)
        {
            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.PostProveedoresAsync(proveedorDto);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "No se pudo ingresar un nuevo proveedor.");
            }

            return View(proveedorDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var proveedor = await _controlGastosAPI.GetProveedoresAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, ProveedorDto proveedorDto)
        {
            if (id != proveedorDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.PutProveedoresAsync(proveedorDto);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "No se pudo actualizar el proveedor.");
            }

            return View(proveedorDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var proveedor = await _controlGastosAPI.GetProveedoresAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _controlGastosAPI.DeleteProveedoresAsync(id);

            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "No se pudo eliminar el proveedor.");

            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
