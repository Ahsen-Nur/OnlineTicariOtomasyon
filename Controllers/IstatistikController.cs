using Microsoft.AspNetCore.Mvc;
using MVCTicariOtomasyonWeb.Models.sınıflar;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class IstatistikController : BaseAdminController
    {
        private readonly Context _context;

        public IstatistikController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 📊 Genel Durum
            ViewBag.d1 = _context.Carilers.Count();      
            ViewBag.d2 = _context.Uruns.Count();         
            ViewBag.d3 = _context.Personels.Count();     
            ViewBag.d4 = _context.Kategoris.Count();     

            // 📦 Ürün & Stok
            ViewBag.d5 = _context.Uruns.Sum(x => x.Stok);      
            ViewBag.d7 = _context.Uruns.Count(x => x.Stok <= 20);

            // 🔹 KATEGORİK STOK DURUMU
            ViewBag.KategoriStoklari = _context.Uruns
                .GroupBy(u => u.Kategori.KategoriAd)
                .Select(g => new
                {
                    Kategori = g.Key,
                    ToplamStok = g.Sum(x => x.Stok)
                })
                .OrderByDescending(x => x.ToplamStok)
                .ToList();

            // 💰 Satış & Fiyat
            ViewBag.d8 = _context.Uruns
                .OrderByDescending(x => x.SatisFiyat)
                .Select(x => x.UrunAd)
                .FirstOrDefault();

            ViewBag.d9 = _context.Uruns
                .OrderBy(x => x.SatisFiyat)
                .Select(x => x.UrunAd)
                .FirstOrDefault();

            ViewBag.d13 = _context.SatisHarekets
                .GroupBy(x => x.Urun.UrunAd)
                .OrderByDescending(g => g.Sum(x => x.Adet))
                .Select(g => g.Key)
                .FirstOrDefault();

            ViewBag.d17 = _context.SatisHarekets
                .GroupBy(x => x.Urun.UrunAd)
                .OrderBy(g => g.Sum(x => x.Adet))
                .Select(g => g.Key)
                .FirstOrDefault();

            // 🧾 Günlük Durum
            ViewBag.d15 = _context.SatisHarekets
                .Count(x => x.Tarih.Date == DateTime.Today);

            ViewBag.d16 = _context.SatisHarekets
                .Where(x => x.Tarih.Date == DateTime.Today)
                .Sum(x => (decimal?)x.ToplamTutar) ?? 0;

            // 📈 Ek Satış Analitiği
            ViewBag.d20 = _context.SatisHarekets.Any()
                ? Math.Round(_context.SatisHarekets.Average(x => x.ToplamTutar), 2)
                : 0;

            ViewBag.d21 = _context.SatisHarekets
                .Where(x => x.Tarih.Date == DateTime.Today)
                .Sum(x => (int?)x.Adet) ?? 0;

            return View();
        }
    }
}
