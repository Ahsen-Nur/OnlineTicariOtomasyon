using Microsoft.AspNetCore.Mvc;
using MVCTicariOtomasyonWeb.Models.sınıflar;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class UrunAciklamaController : Controller
    {
        private readonly Context _context;

        public UrunAciklamaController(Context context)
        {
            _context = context;
        }

        // AÇIKLAMA EKLEME EKRANI
        [HttpGet]
        public IActionResult Ekle(int id)
        {
            ViewBag.UrunId = id;
            return View();
        }


        [HttpPost]
        public IActionResult Ekle(UrunAciklama a)
        {
            var mevcut = _context.UrunAciklamalar
                .FirstOrDefault(x => x.UrunId == a.UrunId);

            if (mevcut != null)
            {
                // GÜNCELLE
                mevcut.Metin = a.Metin;
            }
            else
            {
                // YENİ EKLE (boşsa bile ekleme)
                if (!string.IsNullOrWhiteSpace(a.Metin))
                {
                    _context.UrunAciklamalar.Add(a);
                }
            }

            _context.SaveChanges();

            return RedirectToAction(
                "Index",
                "UrunDetay",
                new { id = a.UrunId }
            );
        }

    }
}
