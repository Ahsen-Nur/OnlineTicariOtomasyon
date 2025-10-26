using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // DbUpdateException
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class KategoriController : Controller
    {
        private readonly Context _context;

        public KategoriController(Context context)
        {
            _context = context;
        }

        // 🔹 Kategorileri Listele
        public IActionResult Index()
        {
            // Performans için takip etmeyelim; sadece listeleme
            var kategoriler = _context.Kategoris
                                      .AsNoTracking()
                                      .OrderBy(k => k.KategoriId)
                                      .ToList();
            return View(kategoriler);
        }

        // 🔹 Yeni kategori ekleme formu (GET)
        [HttpGet]
        public IActionResult YeniKategori()
        {
            return View();
        }

        // 🔹 Formdan gelen veriyi kaydet (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YeniKategori(Kategori k)
        {
            // Sunucu tarafı ekstra koruma: boş/whitespace gelmesini engelle
            if (!string.IsNullOrWhiteSpace(k?.KategoriAd))
            {
                k.KategoriAd = k.KategoriAd.Trim();
            }

            if (!ModelState.IsValid)
            {
                // Hangi alan ve neden hatalı? Görünür kılalım.
                var errors = ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {string.Join(", ", x.Value!.Errors.Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage))}");

                TempData["ModelErrors"] = string.Join(" | ", errors);
                Console.WriteLine("❌ Model hatalı! " + TempData["ModelErrors"]);
                return View(k);
            }

            try
            {
                _context.Kategoris.Add(k);
                var affected = _context.SaveChanges();
                Console.WriteLine($"✅ Kategori kaydedildi: {k.KategoriAd} (rows: {affected})");
                TempData["Success"] = $"Kategori eklendi: {k.KategoriAd}";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                // Çoğunlukla burada: kolon uzunluğu, null constraint, FK/PK, bağlantı sorunları vs.
                var root = ex.GetBaseException()?.Message ?? ex.Message;
                TempData["DbError"] = "Veritabanına kaydedilirken hata oluştu: " + root;
                Console.WriteLine("❌ DbUpdateException: " + root);
                return View(k);
            }
            catch (Exception ex)
            {
                TempData["DbError"] = "Beklenmeyen bir hata oluştu: " + ex.Message;
                Console.WriteLine("❌ Exception: " + ex);
                return View(k);
            }
        }

        // 🔹 Güncelle (GET)
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var kategori = _context.Kategoris.Find(id);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        // 🔹 Güncelle (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guncelle(Kategori k)
        {
            if (!string.IsNullOrWhiteSpace(k?.KategoriAd))
            {
                k.KategoriAd = k.KategoriAd.Trim();
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {string.Join(", ", x.Value!.Errors.Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage))}");
                TempData["ModelErrors"] = string.Join(" | ", errors);
                return View(k);
            }

            var mevcut = _context.Kategoris.Find(k.KategoriId);
            if (mevcut == null)
            {
                return NotFound();
            }

            try
            {
                mevcut.KategoriAd = k.KategoriAd;
                var affected = _context.SaveChanges();
                Console.WriteLine($"📝 Kategori güncellendi: {mevcut.KategoriId} -> {mevcut.KategoriAd} (rows: {affected})");
                TempData["Success"] = "Kategori güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                var root = ex.GetBaseException()?.Message ?? ex.Message;
                TempData["DbError"] = "Güncelleme sırasında hata: " + root;
                Console.WriteLine("❌ DbUpdateException: " + root);
                return View(k);
            }
        }

        // 🔹 Sil (GET → pratik; prod’da POST + AntiForgery tercih edilir)
        public IActionResult Sil(int id)
        {
            var kategori = _context.Kategoris.Find(id);
            if (kategori != null)
            {
                try
                {
                    _context.Kategoris.Remove(kategori);
                    var affected = _context.SaveChanges();
                    Console.WriteLine($"🗑️ Kategori silindi: {kategori.KategoriId} (rows: {affected})");
                    TempData["Success"] = "Kategori silindi.";
                }
                catch (DbUpdateException ex)
                {
                    var root = ex.GetBaseException()?.Message ?? ex.Message;
                    TempData["DbError"] = "Silme sırasında hata: " + root;
                    Console.WriteLine("❌ DbUpdateException: " + root);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
