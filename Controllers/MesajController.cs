using Microsoft.AspNetCore.Mvc;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class MesajController : Controller
    {
        private readonly Context _context;

        public MesajController(Context context)
        {
            _context = context;
        }

        private bool GirisVarMi()
        {
            return HttpContext.Session.GetString("Rol") != null;
        }   


        // 🔹 GELEN MESAJLAR
        public IActionResult Gelen()
        {
            if (!GirisVarMi())
                return RedirectToAction("Giris", "Login");

            string rol = HttpContext.Session.GetString("Rol");
            string mail = HttpContext.Session.GetString("CariMail");

            if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(mail))
                return RedirectToAction("GirisYap", "Login");

            var mesajlar = _context.Mesajs
                .Where(x => x.AliciMail == mail)
                .OrderByDescending(x => x.Tarih)
                .ToList();

            return View(mesajlar);
        }

        // 🔹 GÖNDERİLEN MESAJLAR
        public IActionResult Gonderilen()
        {
            if (!GirisVarMi())
                return RedirectToAction("Giris", "Login");


            string rol = HttpContext.Session.GetString("Rol");
            string mail = HttpContext.Session.GetString("CariMail");

            if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(mail))
                return RedirectToAction("GirisYap", "Login");

            var mesajlar = _context.Mesajs
                .Where(x => x.GonderenMail == mail)
                .OrderByDescending(x => x.Tarih)
                .ToList();

            return View(mesajlar);
        }

        // 🔹 MESAJ DETAY
        public IActionResult Detay(int id)
        {
            if (!GirisVarMi())
                return RedirectToAction("Giris", "Login");

            string rol = HttpContext.Session.GetString("Rol");
            string mail = HttpContext.Session.GetString("CariMail");

            if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(mail))
                return RedirectToAction("GirisYap", "Login");

            var mesaj = _context.Mesajs.Find(id);
            if (mesaj == null)
                return NotFound();

            // ❗️Güvenlik: başkasının mesajını açamasın
            if (mesaj.AliciMail != mail && mesaj.GonderenMail != mail)
                return Forbid();

            mesaj.Okundu = true;
            _context.SaveChanges();

            return View(mesaj);
        }

        // 🔹 YENİ MESAJ (GET)
        [HttpGet]
        public IActionResult Yeni()
        {
            if (!GirisVarMi())
                return RedirectToAction("Giris", "Login");

            string rol = HttpContext.Session.GetString("Rol");
            string mail = HttpContext.Session.GetString("CariMail");

            if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(mail))
                return RedirectToAction("GirisYap", "Login");

            return View();
        }

        // 🔹 YENİ MESAJ (POST)
        [HttpPost]
        public IActionResult Yeni(Mesaj m)
        {
            if (!GirisVarMi())
                return RedirectToAction("Giris", "Login");

            string rol = HttpContext.Session.GetString("Rol");
            string mail = HttpContext.Session.GetString("CariMail");

            if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(mail))
                return RedirectToAction("GirisYap", "Login");

            m.GonderenMail = mail;
            m.Tarih = DateTime.Now;
            m.Okundu = false;

            _context.Mesajs.Add(m);
            _context.SaveChanges();

            return RedirectToAction("Gonderilen");
        }
    }
}
