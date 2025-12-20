using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class FaturaController : Controller
    {
        private readonly Context _context;
        public FaturaController(Context context) => _context = context;

        // FATURALARI LİSTELE
        public IActionResult Index()
        {
            var faturalar = _context.Faturalars
                .Include(x => x.FaturaKalems)
                .OrderByDescending(f => f.FaturaId)
                .ToList();

            return View(faturalar);
        }

        // YENİ FATURA GET
        [HttpGet]
        public IActionResult YeniFatura()
        {
            return View();
        }

        // YENİ FATURA POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YeniFatura(Faturalar f)
        {

            _context.Faturalars.Add(f);
            _context.SaveChanges();

            
            return RedirectToAction(nameof(Index));
        }

        // FATURA GETİR (GÜNCELLEME SAYFASI)
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var fatura = _context.Faturalars.Find(id);
            if (fatura == null) return NotFound();

            return View(fatura);
        }

        // FATURA GÜNCELLE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guncelle(Faturalar f)
        {

            var eski = _context.Faturalars.Find(f.FaturaId);
            if (eski == null) return NotFound();

            eski.FaturaSeriNo = f.FaturaSeriNo;
            eski.FaturaSıraNo = f.FaturaSıraNo;
            eski.Tarih = f.Tarih;
            eski.Saat = f.Saat;
            eski.VergiDairesi = f.VergiDairesi;
            eski.TeslimEden = f.TeslimEden;
            eski.TeslimAlan = f.TeslimAlan;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // FATURA DETAY (KALEMLERİ GETİR)
        public IActionResult Detay(int id)
        {
            var kalemler = _context.FaturaKalems
                .Where(x => x.FaturaId == id)
                .ToList();

            ViewBag.FaturaId = id;
            return View(kalemler);
        }
    }
}
