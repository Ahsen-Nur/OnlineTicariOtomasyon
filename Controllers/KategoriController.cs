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


        private IActionResult AdminKontrol()
        {
            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("GirisYap", "Login");
            return null;
        }


        // Kategorileri Listele (sadece aktif olanlar)
        public IActionResult Index()
        {
            var kategoriler = _context.Kategoris
                                      .AsNoTracking()
                                      .Where(k => k.Durum == true)
                                      .OrderBy(k => k.KategoriId)
                                      .ToList();

            
                          
            return View(kategoriler);
        }

        [HttpGet]
        public IActionResult YeniKategori()
        {
            var kontrol = AdminKontrol();
            if (kontrol != null) return kontrol;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YeniKategori(Kategori k)
        {
            var kontrol = AdminKontrol();
            if (kontrol != null) return kontrol;

            if (!string.IsNullOrWhiteSpace(k?.KategoriAd))
                k.KategoriAd = k.KategoriAd.Trim();

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {string.Join(", ", x.Value!.Errors.Select(e => e.ErrorMessage))}");
                TempData["ModelErrors"] = string.Join(" | ", errors);
                return View(k);
            }

            try
            {
                k.Durum = true;
                _context.Kategoris.Add(k);
                _context.SaveChanges();

                //TempData["Success"] = $"Kategori eklendi: {k.KategoriAd}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["DbError"] = "Veritabanına kaydedilirken hata: " + ex.Message;
                return View(k);
            }
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var kontrol = AdminKontrol();
            if (kontrol != null) return kontrol;

            var kategori = _context.Kategoris.Find(id);
            if (kategori == null)
                return NotFound();

            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guncelle(Kategori k)
        {
            var kontrol = AdminKontrol();
            if (kontrol != null) return kontrol;

            if (!string.IsNullOrWhiteSpace(k?.KategoriAd))
                k.KategoriAd = k.KategoriAd.Trim();

            var mevcut = _context.Kategoris.Find(k.KategoriId);
            if (mevcut == null)
                return NotFound();

            try
            {
                mevcut.KategoriAd = k.KategoriAd;
                _context.SaveChanges();

                //TempData["Success"] = "Kategori güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                TempData["DbError"] = "Güncelleme hatası: " + ex.Message;
                return View(k);
            }
        }

        // Sil (soft delete) — yalnızca kategoriyi pasif yap, ürünlere dokunma
        public IActionResult Sil(int id)
        {
            var kontrol = AdminKontrol();
            if (kontrol != null) return kontrol;

            var kategori = _context.Kategoris.Find(id);
            if (kategori == null)
                return NotFound();

            try
            {
                kategori.Durum = false;

                // ❌ (Önceki davranış) Ürünleri pasif yapma
                // var urunler = _context.Uruns.Where(u => u.KategoriId == id && u.Durum == true).ToList();
                // foreach (var u in urunler) { u.Durum = false; }

                _context.SaveChanges();

                //TempData["Success"] = $"Kategori '{kategori.KategoriAd}' pasif hale getirildi.";
            }
            catch (DbUpdateException ex)
            {
                var root = ex.GetBaseException()?.Message ?? ex.Message;
                TempData["DbError"] = "Silme sırasında veritabanı hatası: " + root;
            }
            catch (Exception ex)
            {
                TempData["DbError"] = "Silme sırasında beklenmedik bir hata: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
