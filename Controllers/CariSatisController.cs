using Microsoft.AspNetCore.Mvc;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using Microsoft.AspNetCore.Http;
using System;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class CariSatisController : Controller
    {
        private readonly Context _context;

        public CariSatisController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SatinAl(int urunId, int adet)
        {
            int? cariId = HttpContext.Session.GetInt32("CariId");

            if (cariId == null)
                return RedirectToAction("GirisYap", "Login");

            var urun = _context.Uruns.Find(urunId);
            if (urun == null || urun.Stok < adet)
                return RedirectToAction("Index", "Urun");

            var satis = new SatisHareket
            {
                UrunId = urunId,
                CariId = cariId.Value,
                Adet = adet,
                Tarih = DateTime.Now,
                ToplamTutar = adet * urun.SatisFiyat,
                Durum = true
            };

            urun.Stok -= (short)adet;

            _context.SatisHarekets.Add(satis);
            _context.SaveChanges();

            return RedirectToAction("Index", "Hesabim");
        }
    }
}
