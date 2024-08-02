using Microsoft.AspNetCore.Mvc;
using CGASTOSFE.DTOs;
using CGASTOSFE.RestApis;

namespace CGASTOSFE.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ControlGastosAPI _controlGastosAPI;

        public ProductoController(ControlGastosAPI controlGastosAPI)
        {
            _controlGastosAPI = controlGastosAPI;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _controlGastosAPI.GetProductosAsync();
            return View(productos);
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

        public IActionResult Create()
        {
            return View();
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

            if(producto == null)
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

            if(success)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
