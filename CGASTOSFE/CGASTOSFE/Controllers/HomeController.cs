using CGASTOSFE.Models;
using CGASTOSFE.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CGASTOSFE.Controllers
{
    public class HomeController : Controller
    {
        private IOptions<ControlGastosApiSettingsDto> _options;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IOptions<ControlGastosApiSettingsDto> options)
        {
            _logger = logger;
            _options = options;
        }

        public IActionResult Index()
        {
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
