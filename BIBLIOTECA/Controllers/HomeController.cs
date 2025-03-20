using System.Diagnostics;
using BIBLIOTECA.Models;
using Microsoft.AspNetCore.Mvc;

namespace BIBLIOTECA.Controllers
{
    /// <summary>
    /// Controlador principal que maneja las vistas de la p�gina de inicio y privacidad.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor del controlador que recibe un logger para registrar informaci�n y errores.
        /// </summary>
        /// <param name="logger">Instancia de ILogger para el registro de logs.</param>

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// M�todo que maneja la solicitud para la p�gina de inicio.
        /// </summary>
        /// <returns>Vista de la p�gina de inicio.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// M�todo que maneja la solicitud para la p�gina de privacidad.
        /// </summary>
        /// <returns>Vista de la p�gina de privacidad.</returns>
        public IActionResult Privacy()
        {
            return View();
        }


        /// <summary>
        /// Maneja errores y muestra una vista personalizada de error.
        /// </summary>
        /// <returns>Vista de error con informaci�n sobre el RequestId.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
