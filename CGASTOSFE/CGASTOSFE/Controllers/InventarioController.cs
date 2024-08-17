using CGASTOSFE.DTOs;
using CGASTOSFE.RestApis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CGASTOSFE.Controllers
{
    [Authorize(Policy = "CustomPolicy")]
    public class InventarioController : Controller
    {
        private readonly ControlGastosAPI _controlGastosAPI;

        // Constructor
        public InventarioController(ControlGastosAPI controlGastosAPI)
        {
            _controlGastosAPI = controlGastosAPI;
        }

        // GET: Inventario
        public async Task<IActionResult> Index()
        {
            try
            {
                var inventarios = await _controlGastosAPI.GetInventariosAsync();
                return View(inventarios);
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

        // GET: Inventario/Details/ById
        public async Task<IActionResult> Details(int id)
        {
            var inventario = await _controlGastosAPI.GetInventariosAsync(id);

            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // GET: Inventario/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                var productos = await _controlGastosAPI.GetProductosAsync();
                ViewBag.Productos = new SelectList(productos, "Id", "Nombre");
                return View();
            }
            catch (Exception ex)
            {
                // Maneja el error (por ejemplo, registrándolo y mostrando un mensaje al usuario)
                ModelState.AddModelError("", "Error al obtener la lista de productos.");
                return View();
            }
        }

        // POST: Inventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventarioDto inventarioDto)
        {
            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.PostInventariosAsync(inventarioDto);//variable asincronica

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "No se pudo ingresar el producto en el inventario.");
            }

            var productos = await _controlGastosAPI.GetProductosAsync();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            return View(inventarioDto);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var inventario = await _controlGastosAPI.GetInventariosAsync(id);

            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, InventarioDto inventarioDto)
        {
            if (id != inventarioDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.PutInventariosAsync(inventarioDto);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "No se pudo actualizar el producto del inventario.");
            }

            return View(inventarioDto);
        }

            public async Task<IActionResult> Delete(int id)
            {
                var inventario = await _controlGastosAPI.GetInventariosAsync(id);

                if (inventario == null)
                {
                    return NotFound();
                }

                return View(inventario);
            }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]

        // POST: Inventario/Delete/ById
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _controlGastosAPI.DeleteInventariosAsync(id);

            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "No se pudo eliminar el producto del inventario.");//mensaje de error
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
