using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CGASTOSFE.RestApis;
using CGASTOSFE.DTOs;

namespace CGASTOSFE.Controllers
{
    [Authorize(Policy = "CustomPolicy")]
    public class UsuarioController : Controller
    {
        private readonly ControlGastosAPI _controlGastosAPI;

        public UsuarioController(ControlGastosAPI controlGastosAPI)
        {
            _controlGastosAPI = controlGastosAPI;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var usuarios = await _controlGastosAPI.GetUsuariosAsync();
                return View(usuarios);
            }
            catch (UnauthorizedAccessException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al obtener los usuarios. Intenta nuevamente más tarde.";
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var usuario = await _controlGastosAPI.GetUsuariosAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Create(UsuarioDTO usuarioDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _controlGastosAPI.PostUsuariosAsync(usuarioDTO);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al crear el usuario. Intenta nuevamente más tarde.";
                return RedirectToAction("Index", "Home");
            }

            return View(usuarioDTO);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _controlGastosAPI.GetUsuariosAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Edit(int id, UsuarioDTO usuarioDTO)
        {
            try
            {
                if (id != usuarioDTO.IdUsuario)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    await _controlGastosAPI.PutUsuariosAsync(usuarioDTO);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al editar el usuario. Intenta nuevamente más tarde.";
                return RedirectToAction("Index", "Home");
            }

            return View(usuarioDTO);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var usuario = await _controlGastosAPI.GetUsuariosAsync(id);

                if (usuario == null)
                {
                    return NotFound();
                }

                return View(usuario);
            }
            catch (UnauthorizedAccessException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al obtener el usuario. Intenta nuevamente más tarde.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _controlGastosAPI.DeleteUsuariosAsync(id);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Delete), new { id });
            }
            catch (UnauthorizedAccessException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al obtener el usuario. Intenta nuevamente más tarde.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
