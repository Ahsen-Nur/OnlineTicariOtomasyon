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

        private IActionResult AdminKontrol()
        {
            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("GirisYap", "Login");
            return null;
        }

        public IActionResult Index()
        {
            var urunler = _context.Uruns
                .Include(u => u.Kategori)
                .Where(u => u.Durum == true)
                .OrderBy(u => u.UrunId)
                .ToList();
            


            return View(urunler);
        }

        [HttpGet]
        public IActionResult YeniUrun()
        {
            var kontrol = AdminKontrol();
            if (kontrol != null) return kontrol;

            ViewBag.Kategoriler = _context.Kategoris
                .Where(k => k.Durum == true)
                .Select(k => new SelectListItem
                {
                    Text = k.KategoriAd,
                    Value = k.KategoriId.ToString()
                })
                .ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YeniUrun(Urun p, IFormFile UrunGorsel)
        {
            var kontrol = AdminKontrol();
            if (kontrol != null) return kontrol;

            ModelState.Remove(nameof(Urun.Kategori));
            ModelState.Remove(nameof(Urun.SatisHarekets));
            ModelState.Remove(nameof(Urun.UrunGorsel));

            if (p.Stok < 0) p.Stok = 0;
            if (p.AlisFiyat < 0) p.AlisFiyat = 0;
            if (p.SatisFiyat < 0) p.SatisFiyat = 0;

            if (!ModelState.IsValid)
            {
                TempData["ModelErrors"] = "Lütfen gerekli alanları doldurun.";
                return View(p);
            }

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

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var kontrol = AdminKontrol();
            if (kontrol != null) return kontrol;

            var urun = _context.Uruns.Find(id);
            if (urun == null) return NotFound();

            ViewBag.Kategoriler = _context.Kategoris
                .Where(k => k.Durum == true)
                .Select(k => new SelectListItem
                {
                    Text = k.KategoriAd,
                    Value = k.KategoriId.ToString()
                })
                .ToList();

            return View(urun);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guncelle(Urun p, IFormFile YeniGorsel)
        {
            var kontrol = AdminKontrol();
            if (kontrol != null) return kontrol;

            ModelState.Remove(nameof(Urun.Kategori));
            ModelState.Remove(nameof(Urun.SatisHarekets));
            ModelState.Remove(nameof(Urun.UrunGorsel));

            if (p.Stok < 0) p.Stok = 0;
            if (p.AlisFiyat < 0) p.AlisFiyat = 0;
            if (p.SatisFiyat < 0) p.SatisFiyat = 0;

            var urun = _context.Uruns.Find(p.UrunId);
            if (urun == null)
            {
                TempData["DbError"] = "Ürün bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            urun.UrunAd = (p.UrunAd ?? "").Trim();
            urun.Marka = (p.Marka ?? "").Trim();
            urun.Stok = p.Stok;
            urun.AlisFiyat = p.AlisFiyat;
            urun.SatisFiyat = p.SatisFiyat;
            urun.Durum = p.Durum;
            urun.KategoriId = p.KategoriId;

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
            //TempData["Success"] = "Ürün bilgileri güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Sil(int id)
        {
            var kontrol = AdminKontrol();
            if (kontrol != null) return kontrol;

            var urun = _context.Uruns.Find(id);
            if (urun == null)
                return NotFound();

            urun.Durum = false;
            _context.SaveChanges();

            TempData["Success"] = "Ürün pasif hale getirildi.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UrunListesi()
        {


            var list = _context.Uruns
                .Include(x => x.Kategori)
                .Where(x => x.Durum == true)
                .ToList();

            return View(list);
        }
    }
}
