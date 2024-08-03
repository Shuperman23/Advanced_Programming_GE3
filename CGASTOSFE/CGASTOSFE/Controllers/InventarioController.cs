using Microsoft.AspNetCore.Mvc;
using CGASTOSFE.DTOs;
using CGASTOSFE.RestApis;

namespace CGASTOSFE.Controllers
{
    public class InventarioController : Controller
    {
        private readonly ControlGastosAPI _controlGastosAPI;

        public InventarioController(ControlGastosAPI controlGastosAPI)
        {
            _controlGastosAPI = controlGastosAPI;
        }

        public async Task<IActionResult> Index()
        {
            var inventarios = await _controlGastosAPI.GetInventariosAsync();
            return View(inventarios);
        }

        public async Task<IActionResult> Details(int id)
        {
            var inventario = await _controlGastosAPI.GetInventariosAsync(id);

            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(InventarioDto inventarioDto)
        {
            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.PostInventariosAsync(inventarioDto);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "No se pudo ingresar el producto en el inventario.");
            }

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

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _controlGastosAPI.DeleteInventariosAsync(id);

            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "No se pudo eliminar el producto del inventario.");//probar, si sirve aplicar este cambio al resto de controllers
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
