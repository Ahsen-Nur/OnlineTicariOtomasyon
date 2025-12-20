using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class CarilerController : Controller
    {
        private readonly Context _context;
        public CarilerController(Context context) => _context = context;

        // LISTE
        public IActionResult Index()
        {
            var cariler = _context.Carilers
                .Where(c => c.Durum == true)
                .OrderBy(c => c.CariId)
                .ToList();

            return View(cariler);
        }

        [HttpGet]
        public IActionResult YeniCari() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YeniCari(Cariler c)
        {
            c.Durum = true;
            _context.Carilers.Add(c);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var cari = _context.Carilers.Find(id);
            if (cari == null) return NotFound();

            return View(cari);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guncelle(Cariler c)
        {
            var cari = _context.Carilers.Find(c.CariId);
            if (cari == null) return NotFound();

            cari.CariAd = c.CariAd;
            cari.CariSoyad = c.CariSoyad;
            cari.CariSehir = c.CariSehir;
            cari.CariMail = c.CariMail;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Sil(int id)
        {
            var cari = _context.Carilers.Find(id);
            if (cari == null) return NotFound();

            cari.Durum = false;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CariSatis(int id)
        {
            var satislar = _context.SatisHarekets
                .Where(s => s.CariId == id)
                .Include(s => s.Urun)
                .Include(s => s.Personel)
                .OrderByDescending(s => s.SatisId)
                .ToList();

            ViewBag.CariAd = _context.Carilers
                .Where(c => c.CariId == id)
                .Select(c => c.CariAd + " " + c.CariSoyad)
                .FirstOrDefault();

            return View(satislar);
        }
    }
}
