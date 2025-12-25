using Microsoft.AspNetCore.Mvc;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        // --------------------
        // GİRİŞ
        // --------------------
        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GirisYap(string Mail, string Sifre)
        {
            // ADMIN
            var admin = _context.Admins
                .FirstOrDefault(x => x.KullaniciAd == Mail && x.Sifre == Sifre);

            if (admin != null)
            {
                HttpContext.Session.SetInt32("AdminId", admin.AdminId);
                HttpContext.Session.SetString("Rol", "Admin");
                return RedirectToAction("Index", "Admin");
            }

            // CARİ
            var cari = _context.Carilers
                .FirstOrDefault(x => x.CariMail == Mail && x.Sifre == Sifre && x.Durum == true);

            if (cari != null)
            {
                HttpContext.Session.SetInt32("CariId", cari.CariId);
                HttpContext.Session.SetString("Rol", "Cari");
                return RedirectToAction("Index", "Hesabim");
            }

            ViewBag.Hata = "Mail veya şifre hatalı";
            return View();
        }

        // --------------------
        // CARİ ŞİFRE ALMA
        // --------------------
        [HttpGet]
        public IActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public IActionResult KayitOl(string Mail, string Sifre)
        {
            var cari = _context.Carilers.FirstOrDefault(x => x.CariMail == Mail);

            if (cari == null)
            {
                ViewBag.Hata = "Bu mail sistemde kayıtlı değil";
                return View();
            }

            if (cari.Sifre != null)
            {
                ViewBag.Hata = "Bu hesap zaten aktif";
                return View();
            }

            cari.Sifre = Sifre;
            cari.Durum = true;
            _context.SaveChanges();

            return RedirectToAction("GirisYap");
        }

        // --------------------
        // ÇIKIŞ
        // --------------------
        public IActionResult CikisYap()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("GirisYap");
        }
    }
}
