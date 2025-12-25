using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.sınıflar;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class FaturaKalemController : BaseAdminController
    {
        private readonly Context _context;

        public FaturaKalemController(Context context)
        {
            _context = context;
        }

        // BELİRLİ FATURAYA AİT KALEMLERİ LİSTELE
        public IActionResult Index()
        {
            var kalemler = _context.FaturaKalems
                .OrderByDescending(k => k.FaturaKalemId)
                .ToList();

            return View(kalemler);
        }

        // YENİ KALEM GET
        [HttpGet]
        public IActionResult YeniKalem(int faturaId)
        {
            ViewBag.FaturaId = faturaId;
            return View();
        }

        // YENİ KALEM POST
        [HttpPost]
        public IActionResult YeniKalem(FaturaKalem k)
        {
            k.Tutar = k.Miktar * k.BirimFiyat;

            _context.FaturaKalems.Add(k);
            _context.SaveChanges();

            return RedirectToAction("Detay", "Fatura", new { id = k.FaturaId });
        }

        // KALEM GÜNCELLE GET
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var kalem = _context.FaturaKalems.Find(id);
            if (kalem == null) return NotFound();

            return View(kalem);
        }

        // KALEM GÜNCELLE POST
        [HttpPost]
        public IActionResult Guncelle(FaturaKalem k)
        {
            var eski = _context.FaturaKalems.Find(k.FaturaKalemId);
            if (eski == null) return NotFound();

            eski.Miktar = k.Miktar;
            eski.BirimFiyat = k.BirimFiyat;
            eski.Tutar = k.Miktar * k.BirimFiyat;
            eski.Aciklama = k.Aciklama;

            _context.SaveChanges();

            return RedirectToAction("Detay", "Fatura", new { id = eski.FaturaId });
        }

        [HttpPost]
        public IActionResult Sil(int id)
        {
            var kalem = _context.FaturaKalems.Find(id);
            if (kalem == null) 
                return NotFound();

            int faturaId = kalem.FaturaId; // silmeden önce alıyoruz

            _context.FaturaKalems.Remove(kalem);
            _context.SaveChanges();

            return RedirectToAction("Detay", "Fatura", new { id = faturaId });
        }

    }
}
