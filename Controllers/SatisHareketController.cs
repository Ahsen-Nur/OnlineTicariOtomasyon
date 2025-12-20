using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class SatisHareketController : Controller
    {
        private readonly Context _context;
        public SatisHareketController(Context context) => _context = context;

        // LİSTELEME
        public IActionResult Index()
        {
            var satislar = _context.SatisHarekets
                .Include(x => x.Urun)
                .Include(x => x.Cariler)
                .Include(x => x.Personel)
                .OrderByDescending(x => x.SatisId)
                .ToList();

            return View(satislar);
        }

        // DETAY
        public IActionResult Detay(int id)
        {
            var s = _context.SatisHarekets
                .Include(x => x.Urun)
                .Include(x => x.Cariler)
                .Include(x => x.Personel)
                .FirstOrDefault(x => x.SatisId == id);

            if (s == null) return NotFound();

            return View(s);
        }

        // YENİ SATIŞ GET
        [HttpGet]
        public IActionResult YeniSatis()
        {
            ViewBag.Urunler = _context.Uruns.ToList();
            ViewBag.Cariler = _context.Carilers.Where(c => c.Durum == true).ToList();
            ViewBag.Personeller = _context.Personels.Where(p => p.Durum == true).ToList();

            return View();
        }

        // YENİ SATIŞ POST
       [HttpPost]
       [ValidateAntiForgeryToken]
       public IActionResult YeniSatis(SatisHareket s)
       {
            s.Tarih = DateTime.Now;
            s.ToplamTutar = s.Adet * s.Fiyat;

            // stok kontrolü
            var urun = _context.Uruns.Find(s.UrunId);
            if (urun.Stok < s.Adet)
            {
                ModelState.AddModelError("", "Bu üründe yeterli stok bulunmuyor.");
                return View(s);
            }

            // satış kaydı
            _context.SatisHarekets.Add(s);

           // stok güncelle
           urun.Stok -= (short)s.Adet;

           _context.SaveChanges();

           return RedirectToAction(nameof(Index));
        }


        // GÜNCELLE GET
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var s = _context.SatisHarekets.Find(id);
            if (s == null) return NotFound();

            ViewBag.Urunler = _context.Uruns.ToList();
            ViewBag.Cariler = _context.Carilers.Where(c => c.Durum == true).ToList();
            ViewBag.Personeller = _context.Personels.Where(p => p.Durum == true).ToList();

            return View(s);
        }

        // GÜNCELLE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guncelle(SatisHareket s)
        {
            var eski = _context.SatisHarekets
                            .AsNoTracking()
                            .FirstOrDefault(x => x.SatisId == s.SatisId);

            if (eski == null) return NotFound();

            var urun = _context.Uruns.Find(s.UrunId);

            int fark = s.Adet - eski.Adet;

            // eğer satış arttıysa stok düşer
            if (fark > 0)
            {
                if (urun.Stok < fark)
                {
                    ModelState.AddModelError("", "Stok yetersiz! Bu güncelleme yapılamaz.");
                    return View(s);
                }
                urun.Stok -= (short)fark;
            }
            else if (fark < 0)
            {
            // satış azaldıysa stok iade edilir
                urun.Stok += (short)Math.Abs(fark);
            }

            var satis = _context.SatisHarekets.Find(s.SatisId);
            satis.Adet = s.Adet;
            satis.Fiyat = s.Fiyat;
            satis.ToplamTutar = s.Adet * s.Fiyat;
            satis.CariId = s.CariId;
            satis.UrunId = s.UrunId;
            satis.PersonelId = s.PersonelId;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        // SİL
        [HttpPost]
        public IActionResult Sil(int id)
        {
            var satis = _context.SatisHarekets.FirstOrDefault(x => x.SatisId == id);
            if (satis == null)
                return NotFound();

            // Ürün bulunsun diye güvenli kontrol
            var urun = _context.Uruns.FirstOrDefault(x => x.UrunId == satis.UrunId);

            if (urun != null)
                urun.Stok += (short)satis.Adet;   // stok iade
            
            _context.SatisHarekets.Remove(satis);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
}


    }
}
