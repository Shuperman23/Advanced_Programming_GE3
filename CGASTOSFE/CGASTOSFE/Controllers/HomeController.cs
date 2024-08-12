using CGASTOSFE.DTOs;
using CGASTOSFE.Models;
using CGASTOSFE.RestApis;
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

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserToken");
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
