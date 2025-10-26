using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.IO;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class UrunController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _env;

        public UrunController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // 🔹 Ürün listesi
        public IActionResult Index()
        {
            var urunler = _context.Uruns
                .Include(u => u.Kategori)
                .OrderBy(u => u.UrunId)
                .ToList();

            return View(urunler);
        }

        // 🔹 Yeni Ürün Formu (GET)
        [HttpGet]
        public IActionResult YeniUrun()
        {
            ViewBag.Kategoriler = _context.Kategoris
                .Select(k => new SelectListItem
                {
                    Text = k.KategoriAd,
                    Value = k.KategoriId.ToString()
                })
                .ToList();

            return View();
        }

        // 🔹 Yeni Ürün Kaydı (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YeniUrun(Urun p, IFormFile UrunGorsel)
        {
            // 🔸 ModelState doğrulama dışında bırakılacak alanlar
            ModelState.Remove(nameof(Urun.Kategori));
            ModelState.Remove(nameof(Urun.SatisHarekets));
            ModelState.Remove(nameof(Urun.UrunGorsel));

            // 🔸 Negatif değerleri sıfırla
            if (p.Stok < 0) p.Stok = 0;
            if (p.AlisFiyat < 0) p.AlisFiyat = 0;
            if (p.SatisFiyat < 0) p.SatisFiyat = 0;

            ViewBag.Kategoriler = _context.Kategoris
                .Select(k => new SelectListItem
                {
                    Text = k.KategoriAd,
                    Value = k.KategoriId.ToString()
                })
                .ToList();

            if (!ModelState.IsValid)
            {
                TempData["ModelErrors"] = "Lütfen gerekli alanları doldurun.";
                return View(p);
            }

            // 🔹 Görsel yükleme
            if (UrunGorsel != null && UrunGorsel.Length > 0)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images/urunler");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Path.GetFileNameWithoutExtension(UrunGorsel.FileName)
                                        + "_" + Path.GetRandomFileName().Substring(0, 4)
                                        + Path.GetExtension(UrunGorsel.FileName);

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    UrunGorsel.CopyTo(fileStream);
                }

                p.UrunGorsel = "/images/urunler/" + uniqueFileName;
            }

            p.Durum = true;
            _context.Uruns.Add(p);
            _context.SaveChanges();

            TempData["Success"] = $"{p.UrunAd} başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        // 🔹 Ürün Güncelleme (GET)
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var urun = _context.Uruns.Find(id);
            if (urun == null) return NotFound();

            ViewBag.Kategoriler = _context.Kategoris
                .Select(k => new SelectListItem
                {
                    Text = k.KategoriAd,
                    Value = k.KategoriId.ToString()
                })
                .ToList();

            return View(urun);
        }

        // 🔹 Ürün Güncelleme (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guncelle(Urun p, IFormFile YeniGorsel)
        {
            ModelState.Remove(nameof(Urun.Kategori));
            ModelState.Remove(nameof(Urun.SatisHarekets));
            ModelState.Remove(nameof(Urun.UrunGorsel));

            // 🔸 Negatif değerleri sıfırla
            if (p.Stok < 0) p.Stok = 0;
            if (p.AlisFiyat < 0) p.AlisFiyat = 0;
            if (p.SatisFiyat < 0) p.SatisFiyat = 0;

            var urun = _context.Uruns.Find(p.UrunId);
            if (urun == null) return NotFound();

            urun.UrunAd = p.UrunAd;
            urun.Marka = p.Marka;
            urun.Stok = p.Stok;
            urun.AlisFiyat = p.AlisFiyat;
            urun.SatisFiyat = p.SatisFiyat;
            urun.Durum = p.Durum;
            urun.KategoriId = p.KategoriId;

            // 🔹 Yeni görsel yüklenmişse eskiyi sil
            if (YeniGorsel != null && YeniGorsel.Length > 0)
            {
                if (!string.IsNullOrEmpty(urun.UrunGorsel))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, urun.UrunGorsel.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                string uploadsFolder = Path.Combine(_env.WebRootPath, "images/urunler");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Path.GetFileNameWithoutExtension(YeniGorsel.FileName)
                                        + "_" + Path.GetRandomFileName().Substring(0, 4)
                                        + Path.GetExtension(YeniGorsel.FileName);

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    YeniGorsel.CopyTo(fileStream);
                }

                urun.UrunGorsel = "/images/urunler/" + uniqueFileName;
            }

            _context.SaveChanges();
            TempData["Success"] = "Ürün bilgileri güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        // 🔹 Ürün Sil
        public IActionResult Sil(int id)
        {
            var urun = _context.Uruns.Find(id);
            if (urun != null)
            {
                if (!string.IsNullOrEmpty(urun.UrunGorsel))
                {
                    string path = Path.Combine(_env.WebRootPath, urun.UrunGorsel.TrimStart('/'));
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                _context.Uruns.Remove(urun);
                _context.SaveChanges();
                TempData["Success"] = "Ürün silindi.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
