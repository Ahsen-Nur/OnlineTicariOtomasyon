using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class DepartmanController : BaseAdminController
    {
        private readonly Context _context;
        public DepartmanController(Context context) => _context = context;

        public IActionResult Index()
        {
            var list = _context.Departmans
                               .AsNoTracking()
                               .Where(d => d.Durum == true)
                               .OrderBy(d => d.DepartmanId)
                               .ToList();

       
                   
            return View(list);
        }

        [HttpGet]
        public IActionResult YeniDepartman() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YeniDepartman(Departman d)
        {
            if (string.IsNullOrWhiteSpace(d?.DepartmanAd))
            {
                TempData["DbError"] = "Departman adı boş olamaz.";
                return View(d);
            }

            d.DepartmanAd = d.DepartmanAd.Trim();
            d.Durum = true;
            _context.Departmans.Add(d);
            _context.SaveChanges();

            //TempData["Success"] = $"'{d.DepartmanAd}' eklendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var dep = _context.Departmans.Find(id);
            if (dep == null) return NotFound();
            return View(dep);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guncelle(Departman d)
        {
            var dep = _context.Departmans.FirstOrDefault(x => x.DepartmanId == d.DepartmanId);
            if (dep == null) return NotFound();

            if (string.IsNullOrWhiteSpace(d.DepartmanAd))
            {
                ModelState.AddModelError(nameof(d.DepartmanAd), "Departman adı zorunludur.");
                return View(d);
            }

            dep.DepartmanAd = d.DepartmanAd.Trim();
            _context.SaveChanges();

            //TempData["Success"] = "Departman güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sil(int id)
        {
            var dep = _context.Departmans.Find(id);
            if (dep == null) return NotFound();

            dep.Durum = false;               // soft delete
            _context.SaveChanges();

            //TempData["Success"] = $"'{dep.DepartmanAd}' silindi.";
            return RedirectToAction(nameof(Index));
        }

        // Detay: aynı tasarım, personeller listelenir
        public IActionResult Detay(int id)
        {
            var personeller = _context.Personels
                .Where(p => p.DepartmanId == id && p.Durum == true)
                .OrderBy(p => p.PersonelId)
                .ToList();

            ViewBag.DepartmanId = id;
            ViewBag.DepartmanAd = _context.Departmans
                .Where(d => d.DepartmanId == id)
                .Select(d => d.DepartmanAd)
                .FirstOrDefault();

            return View(personeller);
        }


        public IActionResult PersonelSatis(int id)
        {
            var satislar = _context.SatisHarekets
                .Where(x => x.PersonelId == id)
                .Include(x => x.Urun)
                .Include(x => x.Cariler)
                .Include(x => x.Personel)
                .OrderByDescending(x => x.SatisId)
                .ToList();

            ViewBag.PersonelAd = _context.Personels
                .Where(x => x.PersonelId == id)
                .Select(x => x.PersonelAd + " " + x.PersonelSoyad)
                .FirstOrDefault();

            return View(satislar);
        }

    }
}
