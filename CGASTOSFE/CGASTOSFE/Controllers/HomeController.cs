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
        // Inyección de dependencias para las configuraciones, el logger, y la API de Control de Gastos.
        private readonly IOptions<ControlGastosApiSettingsDto> _options;
        private readonly ILogger<HomeController> _logger;
        private readonly ControlGastosAPI _controlGastosAPI;

        // Constructor que inicializa las dependencias inyectadas.
        public HomeController(ILogger<HomeController> logger, IOptions<ControlGastosApiSettingsDto> options, ControlGastosAPI controlGastosAPI)
        {
            _logger = logger;
            _options = options;
            _controlGastosAPI = controlGastosAPI;
        }

        // Método que muestra la vista principal de la aplicación.
        [Authorize(Policy = "CustomPolicy")]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserToken")))// Se verifica si el usuario está autenticado.
            {
                return RedirectToAction(nameof(Login));// Se redirige a la acción de login.
            }

            return View();
        }
        // Método que muestra la vista de login.
        public IActionResult Login()
        {
            return View();
        }

        // Método que recibe los datos del formulario de login y autentica al usuario.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Se valida que el modelo sea correcto.
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.AuthenticateAsync(loginDto);// Se autentica al usuario.

                if (success)
                {
                    HttpContext.Session.SetString("UserName", loginDto.Correo);
                    HttpContext.Session.SetString("UserToken", _controlGastosAPI.GetToken());// Se guarda el token en la sesión.
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Credenciales incorrectas o usuario no encontrado.");// Se agrega un error al modelo.
                }
            }

            return View(loginDto);// Se regresa la vista con el modelo.
        }
        // Método que muestra la vista de registro.
        public IActionResult VerificarUsuario()
        {
            return View(new VerificarUsuarioDTO());// Se regresa la vista con un nuevo modelo.
        }

// Método que recibe los datos del formulario de registro y verifica si el usuario existe.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerificarUsuario(VerificarUsuarioDTO verificarUsuarioDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _controlGastosAPI.VerificarUsuarioAsync(verificarUsuarioDto);// Se verifica si el usuario existe.

                if (response == null)
                {
                    ModelState.AddModelError("", "usuario no encontrado.");// Se agrega un error al modelo.
                    return View(verificarUsuarioDto);
                }

                if (response.Success)
                {
                    return RedirectToAction(nameof(CambiarContrasena), new { correo = verificarUsuarioDto.Correo });// Se redirige a la acción de cambiar contraseña.
                }
                else
                {
                    ModelState.AddModelError("", response.Message ?? "No se pudo verificar el usuario.");// Se agrega un error al modelo.
                }
            }

            return View(verificarUsuarioDto);
        }

        // Método que muestra la vista de cambiar contraseña.
        public IActionResult CambiarContrasena(string correo)
        {
            var model = new CambiarContrasenaDTO { Correo = correo };// Se crea un nuevo modelo con el correo recibido.
            return View(model);
        }


        // Método que recibe los datos del formulario de cambiar contraseña y cambia la contraseña del usuario.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarContrasena(CambiarContrasenaDTO cambiarContrasenaDto)
        {
            if (ModelState.IsValid)
            {
                var success = await _controlGastosAPI.CambiarContrasenaAsync(cambiarContrasenaDto);// Se cambia la contraseña del usuario.

                if (success)
                {
                    return RedirectToAction(nameof(Login));// Se redirige a la acción de login.
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo cambiar la contraseña.");// Se agrega un error al modelo.
                }
            }

            return View(cambiarContrasenaDto);
        }

        // Método que muestra la vista de privacidad.
        [Authorize(Policy = "CustomPolicy")]
        public IActionResult Privacy()
        {
            return View();
        }

        // Método que muestra la vista de logout.
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserToken");// Se remueve el token de la sesión.
            return RedirectToAction(nameof(Login));// Se redirige a la acción de login.
        }

        // Método que muestra la vista de error.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]// Se especifica que no se guarde en caché.
        
        // Se regresa la vista con un nuevo modelo.
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });// Se regresa la vista con un nuevo modelo.
        }
    }
}
