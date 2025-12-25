using Microsoft.AspNetCore.Mvc;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class GrafikController : Controller
    {
        private readonly Context _context;

        public GrafikController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index", "Kategori");

            return View();
        }

        // ==========================
        // AJAX API ENDPOINTLERİ
        // ==========================

        public IActionResult CariSehirDagilim()
        {
            var data = _context.Carilers
                .Where(x => x.CariSehir != null)
                .GroupBy(x => x.CariSehir)
                .Select(g => new {
                    label = g.Key,
                    value = g.Count()
                })
                .ToList();

            return Json(data);
        }

        public IActionResult UrunMarkaDagilim()
        {
            var data = _context.Uruns
                .Where(x => x.Marka != null)
                .GroupBy(x => x.Marka)
                .Select(g => new {
                    label = g.Key,
                    value = g.Count()
                })
                .ToList();

            return Json(data);
        }

        public IActionResult GunlukSatis()
        {
            var data = _context.SatisHarekets
                .AsEnumerable()
                .GroupBy(x => x.Tarih.Date)
                .Select(g => new {
                    label = g.Key.ToString("dd.MM"),
                    value = g.Sum(x => x.ToplamTutar)
                })
                .OrderBy(x => x.label)
                .ToList();

            return Json(data);
        }

        // EN ÇOK SATAN ÜRÜNLER
        public IActionResult EnCokSatanUrunler()
        {
            var data = _context.SatisHarekets
                .GroupBy(x => x.Urun.UrunAd)
                .Select(g => new {
                    label = g.Key,
                    value = g.Sum(x => x.Adet)
                })
                .OrderByDescending(x => x.value)
                .Take(5)
                .ToList();
            return Json(data);
        }

// CARİ BAZLI HARCAMA
        public IActionResult CariHarcama()
        {
            var data = _context.SatisHarekets
                .GroupBy(x => x.Cariler.CariAd + " " + x.Cariler.CariSoyad)
                .Select(g => new {
                    label = g.Key,
                    value = g.Sum(x => x.ToplamTutar)
                })
                .OrderByDescending(x => x.value)
                .ToList();
            return Json(data);
        }

    }
}
