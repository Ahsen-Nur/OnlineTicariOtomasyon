using Microsoft.AspNetCore.Mvc;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class UrunOzellikController : BaseAdminController
    {
        private readonly Context _context;

        public UrunOzellikController(Context context)
        {
            _context = context;
        }

        // ÖZELLİK EKLEME FORMU
        [HttpGet]
        public IActionResult Ekle(int id)
        {
            var urun = _context.Uruns.Find(id);

            var ozellikler = _context.KategoriOzellikler
                .Where(x => x.KategoriId == urun.KategoriId)
                .Select(x => x.Ozellik)
                .ToList();

            ViewBag.UrunId = id;
            ViewBag.Ozellikler = ozellikler;
            return View();
        }

        // ÖZELLİK KAYDET
        [HttpPost]
        public IActionResult Ekle(UrunOzellikDeger d)
        {
            Console.WriteLine("DEGER => " + d.Deger);
            d.Id = 0;
            _context.UrunOzellikDegerleri.Add(d);
            _context.SaveChanges();

            return RedirectToAction(
                "Index",
                "UrunDetay",
                new { id = d.UrunId }
            );
        
        }

        public IActionResult Sil(int id)
        {
            var ozellikDeger = _context.UrunOzellikDegerleri
                .FirstOrDefault(x => x.Id == id);

            if (ozellikDeger == null)
            {
                return NotFound();
            }

            int urunId = ozellikDeger.UrunId;
            _context.UrunOzellikDegerleri.Remove(ozellikDeger);
            _context.SaveChanges();

            return RedirectToAction(
                "Index",
                "UrunDetay",
                new { id = urunId }
            );
        }

    }
}
