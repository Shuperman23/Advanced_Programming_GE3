using CGASTOSFE.DTOs;
using CGASTOSFE.Models;
using CGASTOSFE.RestApis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CGASTOSFE.Controllers
{

    public class HomeController : Controller
    {
        private readonly IOptions<ControlGastosApiSettingsDto> _options;
        private readonly ILogger<HomeController> _logger;
        private readonly ControlGastosAPI _controlGastosAPI;

        public HomeController(ILogger<HomeController> logger, IOptions<ControlGastosApiSettingsDto> options, ControlGastosAPI controlGastosAPI)
        {
            _logger = logger;
            _options = options;
            _controlGastosAPI = controlGastosAPI;
        }
        [Authorize(Policy = "CustomPolicy")]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserToken")))
            {
                return RedirectToAction(nameof(Login));
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.AuthenticateAsync(loginDto);

                if (success)
                {
                    HttpContext.Session.SetString("UserToken", _controlGastosAPI.GetToken());
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Credenciales incorrectas o usuario no encontrado.");
                }
            }

            return View(loginDto);
        }
        public IActionResult VerificarUsuario()
        {
            return View(new VerificarUsuarioDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerificarUsuario(VerificarUsuarioDTO verificarUsuarioDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _controlGastosAPI.VerificarUsuarioAsync(verificarUsuarioDto);

                if (response == null)
                {
                    ModelState.AddModelError("", "usuario no encontrado.");
                    return View(verificarUsuarioDto);
                }

                if (response.Success)
                {
                    return RedirectToAction(nameof(CambiarContrasena), new { correo = verificarUsuarioDto.Correo });
                }
                else
                {
                    ModelState.AddModelError("", response.Message ?? "No se pudo verificar el usuario.");
                }
            }

            return View(verificarUsuarioDto);
        }


        public IActionResult CambiarContrasena(string correo)
        {
            var model = new CambiarContrasenaDTO { Correo = correo };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarContrasena(CambiarContrasenaDTO cambiarContrasenaDto)
        {
            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.CambiarContrasenaAsync(cambiarContrasenaDto);

                if (success)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo cambiar la contraseña.");
                }
            }

            return View(cambiarContrasenaDto);
        }

        [Authorize(Policy = "CustomPolicy")]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserToken");
            return RedirectToAction(nameof(Login));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
