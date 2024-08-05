using CGASTOSFE.Models;
using CGASTOSFE.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CGASTOSFE.RestApis;

namespace CGASTOSFE.Controllers
{
    public class HomeController : Controller
    {
        private IOptions<ControlGastosApiSettingsDto> _options;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.AuthenticateAsync(loginDto);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "No se pudo encontrar el usuario.");
            }

            return View();
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
