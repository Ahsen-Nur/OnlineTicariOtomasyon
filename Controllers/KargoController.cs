using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class KargoController : BaseAdminController
    {
        private readonly Context _context;

        public KargoController(Context context)
        {
            _context = context;
        }

        // 🔹 ADMIN – KARGO LİSTESİ (detaylı)
        public IActionResult Index()
        {
            var kargolar = _context.Kargos
                .Include(k => k.KargoDetaylar)
                    .ThenInclude(kd => kd.SatisHareket)
                        .ThenInclude(s => s.Cariler)
                .OrderByDescending(k => k.OlusturmaTarihi)
                .ToList();

            return View(kargolar);
        }

        // 🔹 ADMIN – YENİ KARGO (GET)
        [HttpGet]
        public IActionResult Yeni(int satisId)
        {
            ViewBag.SatisId = satisId;
            ViewBag.TakipKodu = TakipKoduUret();
            return View();
        }

        // 🔹 ADMIN – YENİ KARGO (POST)
        [HttpPost]
        public IActionResult Yeni(int satisId, string kargoFirmasi)
        {
            var satis = _context.SatisHarekets.Find(satisId);
            if (satis == null)
                return NotFound();

            // 1️⃣ Kargo oluştur
            var kargo = new Kargo
            {
                KargoFirmasi = kargoFirmasi,
                TakipKodu = TakipKoduUret(),
                OlusturmaTarihi = DateTime.Now,
                Durum = "Kargoda"
            };

            _context.Kargos.Add(kargo);
            _context.SaveChanges();

            // 2️⃣ KargoDetay ile bağla
            var detay = new KargoDetay
            {
                KargoId = kargo.KargoId,
                SatisId = satis.SatisId
            };

            _context.KargoDetays.Add(detay);

            // 3️⃣ Sipariş durumunu güncelle
            satis.SiparisDurum = "Kargoda";
            satis.KargoyaVerilmeTarihi = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // 🔹 ADMIN – KARGO TESLİM
        public IActionResult TeslimEt(int id)
        {
            var kargo = _context.Kargos
                .Include(k => k.KargoDetaylar)
                    .ThenInclude(kd => kd.SatisHareket)
                .FirstOrDefault(k => k.KargoId == id);

            if (kargo == null)
                return NotFound();

            kargo.Durum = "Teslim Edildi";
            kargo.TeslimTarihi = DateTime.Now;

            foreach (var kd in kargo.KargoDetaylar)
            {
                kd.SatisHareket.SiparisDurum = "Teslim Edildi";
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // 🔹 ADMIN – KARGO İŞLEMLERİ (UI GÜZEL SAYFA)
        public IActionResult KargoIslemleri()
        {
            var kargolar = _context.Kargos
                .Include(k => k.KargoDetaylar)
                    .ThenInclude(kd => kd.SatisHareket)
                        .ThenInclude(s => s.Cariler)
                .OrderByDescending(k => k.KargoId)
                .ToList();

            return View(kargolar);
        }

        private string TakipKoduUret()
        {
            return "KR" + Guid.NewGuid().ToString("N")
                .Substring(0, 10)
                .ToUpper();
        }
    }
}
