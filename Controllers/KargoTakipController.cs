using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class KargoTakipController : Controller
    {
        private readonly Context _context;

        public KargoTakipController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int? cariId = HttpContext.Session.GetInt32("CariId");
            if (cariId == null)
                return RedirectToAction("GirisYap", "Login");

            var kargolar = _context.Kargos
                .Include(k => k.KargoDetaylar)
                    .ThenInclude(kd => kd.SatisHareket)
                        .ThenInclude(s => s.Urun)
                .Where(k => k.KargoDetaylar
                    .Any(kd => kd.SatisHareket.CariId == cariId))
                .ToList();

            return View(kargolar);
        }

        public IActionResult Takip(string kod)
        {
            var kargo = _context.Kargos
                .Include(k => k.KargoDetaylar)
                    .ThenInclude(kd => kd.SatisHareket)
                        .ThenInclude(s => s.Urun)
                .FirstOrDefault(k => k.TakipKodu == kod);

            if (kargo == null)
            {
                ViewBag.Hata = "Takip kodu bulunamadı.";
                return View();
            }

            return View(kargo);
        }
    }
}
