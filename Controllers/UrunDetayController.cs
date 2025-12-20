using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.ViewModels;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class UrunDetayController : Controller
    {
        private readonly Context _context;

        public UrunDetayController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var model = new UrunDetayViewModel
            {
                Urun = _context.Uruns
                    .Include(x => x.Kategori)
                    .FirstOrDefault(x => x.UrunId == id),

                Ozellikler = _context.UrunOzellikDegerleri
                    .Include(x => x.Ozellik)
                    .Where(x => x.UrunId == id)
                    .ToList(),

                Aciklama = _context.UrunAciklamalar
                    .FirstOrDefault(x => x.UrunId == id),

                Yorumlar = _context.UrunYorumlar
                    .Where(x => x.UrunId == id)
                    .OrderByDescending(x => x.Tarih)
                    .ToList()
            };

            return View(model);
        }

    }
}
