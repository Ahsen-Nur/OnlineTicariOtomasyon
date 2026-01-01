using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MVCTicariOtomasyonWeb.Models;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Session kontrolü
            var adminId = HttpContext.Session.GetInt32("AdminId");
            var cariId = HttpContext.Session.GetInt32("CariId");

            // Admin girişliyse → Admin Dashboard
            if (adminId != null)
            {
                return RedirectToAction("Index", "Admin");
            }

            // Cari girişliyse → Cari Dashboard
            if (cariId != null)
            {
                return RedirectToAction("Index", "Hesabim");
            }

            // Kimse giriş yapmamışsa → Landing (Kapak) Sayfası
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}
