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
        // GİRİŞ (GET)
        // --------------------
        [HttpGet]
        public IActionResult GirisYap(string rol)
        {
           
            ViewBag.Rol = rol;
            return View();
        }

        // --------------------
        // GİRİŞ (POST)
        // --------------------

        [HttpPost]
        public IActionResult GirisYap(string Mail, string Sifre, string Rol)
        {
    // ADMIN
            if (Rol == "admin")
            {
                var admin = _context.Admins
                    .FirstOrDefault(x => x.KullaniciAd == Mail && x.Sifre == Sifre);

                if (admin != null)
                {
                    HttpContext.Session.SetInt32("AdminId", admin.AdminId);
                    HttpContext.Session.SetString("Rol", "Admin");
                    HttpContext.Session.SetString("CariMail", admin.KullaniciAd);
                    return RedirectToAction("Index", "Admin");
                }
            }

    // CARİ
            if (Rol == "cari")
            {
                var cari = _context.Carilers
                    .FirstOrDefault(x => x.CariMail == Mail && x.Sifre == Sifre && x.Durum);

                if (cari != null)
                {
                    HttpContext.Session.SetInt32("CariId", cari.CariId);
                    HttpContext.Session.SetString("Rol", "Cari");
                    HttpContext.Session.SetString("CariMail", cari.CariMail);
                    return RedirectToAction("Index", "Hesabim");
                }
            }

            ViewBag.Rol = Rol;
            ViewBag.Hata = "Mail / kullanıcı adı veya şifre hatalı";
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

            if (!string.IsNullOrEmpty(cari.Sifre))
            {
                ViewBag.Hata = "Bu hesap zaten aktif";
                return View();
            }

            cari.Sifre = Sifre;
            cari.Durum = true;
            _context.SaveChanges();

            return RedirectToAction("GirisYap", new { rol = "cari" });
        }

        // --------------------
        // ÇIKIŞ
        // --------------------
        public IActionResult CikisYap()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
