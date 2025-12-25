using Microsoft.AspNetCore.Mvc;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class AdminController : BaseAdminController
    {
        private readonly Context _context;

        public AdminController(Context context)
        {
            _context = context;
        }

        // ============================
        // ADMIN DASHBOARD
        // ============================
        public IActionResult Index(string sehir = "", string marka = "")
        {
            // 🔒 Admin değilse sokma
            if (HttpContext.Session.GetString("Rol") != "Admin")
            {
                return RedirectToAction("Index", "Kategori");
            }

            // ----------------------------
            // GENEL SAYILAR (NET TİP)
            // ----------------------------
            int toplamCari = _context.Carilers.Count();
            int toplamUrun = _context.Uruns.Count();
            int toplamSatis = _context.SatisHarekets.Count();
            int personelSayisi = _context.Personels.Count();

            ViewBag.ToplamCari = toplamCari;
            ViewBag.ToplamUrun = toplamUrun;
            ViewBag.ToplamSatis = toplamSatis;
            ViewBag.PersonelSayisi = personelSayisi;

            // ----------------------------
            // CARİ → ŞEHİR DAĞILIMI
            // ----------------------------
            ViewBag.CariSehirDagilim = _context.Carilers
                .GroupBy(x => x.CariSehir)
                .Select(g => new
                {
                    Sehir = g.Key,
                    Sayi = g.Count(),
                    Oran = toplamCari == 0
                        ? 0
                        : (int)((double)g.Count() * 100 / toplamCari)
                })
                .OrderByDescending(x => x.Sayi)
                .ToList();

            // ----------------------------
            // ÜRÜN → MARKA DAĞILIMI
            // ----------------------------
            ViewBag.MarkaDagilim = _context.Uruns
                .GroupBy(x => x.Marka)
                .Select(g => new
                {
                    Marka = g.Key,
                    Sayi = g.Count(),
                    Oran = toplamUrun == 0
                        ? 0
                        : (int)((double)g.Count() * 100 / toplamUrun)
                })
                .OrderByDescending(x => x.Sayi)
                .ToList();

            // ----------------------------
            // PERSONEL → DEPARTMAN DAĞILIMI
            // ----------------------------
            ViewBag.DepartmanDagilim = _context.Personels
                .GroupBy(x => x.Departman.DepartmanAd)
                .Select(g => new
                {
                    Departman = g.Key,
                    Sayi = g.Count()
                })
                .OrderByDescending(x => x.Sayi)
                .ToList();


            return View();
        }

        // ============================
        // 404
        // ============================
        public IActionResult Error404()
        {
            return View();
        }

        // ============================
        // 500
        // ============================
        public IActionResult Error500()
        {
            return View();
        }
    }
}
