using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class IadeController : BaseAdminController
    {
        private readonly Context _context;

        public IadeController(Context context)
        {
            _context = context;
        }

        // 🔹 ADMIN – İade Listesi
        public IActionResult Index()
        {
            var iadeler = _context.Iades
                .Include(i => i.SatisHareket)
                    .ThenInclude(s => s.Urun)
                .Include(i => i.SatisHareket)
                    .ThenInclude(s => s.Cariler)
                .OrderByDescending(i => i.TalepTarihi)
                .ToList();

            return View(iadeler);
        }

        // 🔹 CARİ – İade Talebi
        [HttpGet]
        public IActionResult TalepEt(int satisId)
        {
            var satis = _context.SatisHarekets
                .Include(s => s.Urun)
                .FirstOrDefault(s => s.SatisId == satisId);

            if (satis == null || satis.SiparisDurum != "Teslim Edildi")
                return RedirectToAction("Index", "Hesabim");

            var iade = new Iade
            {
                SatisId = satisId,
                TalepTarihi = DateTime.Now,
                Durum = "Beklemede"
            };

            _context.Iades.Add(iade);

            satis.SiparisDurum = "İade Beklemede";

            _context.SaveChanges();

            return RedirectToAction("Index", "Hesabim");
        }

        // 🔹 ADMIN – İade Onayla
        public IActionResult Onayla(int id)
        {
            var iade = _context.Iades
                .Include(i => i.SatisHareket)
                    .ThenInclude(s => s.Urun)
                .FirstOrDefault(i => i.IadeId == id);

            if (iade == null)
                return NotFound();

            var satis = iade.SatisHareket;
            var urun = satis.Urun;

            iade.Durum = "Onaylandı";
            satis.SiparisDurum = "İade Edildi";

            // 🔴 STOK GERİ EKLENİR
            urun.Stok += (short)satis.Adet;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // 🔹 ADMIN – İade Reddet
        public IActionResult Reddet(int id)
        {
            var iade = _context.Iades
                .Include(i => i.SatisHareket)
                .FirstOrDefault(i => i.IadeId == id);

            if (iade == null)
                return NotFound();

            iade.Durum = "Reddedildi";
            iade.SatisHareket.SiparisDurum = "Teslim Edildi";

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
