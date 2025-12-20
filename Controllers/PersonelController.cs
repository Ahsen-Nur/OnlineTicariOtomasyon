using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.Linq;

namespace MVCTicariOtomasyonWeb.Controllers
{
    public class PersonelController : Controller
    {
        private readonly Context _context;

        public PersonelController(Context context)
        {
            _context = context;
        }

        // -----------------------------------------------------------
        // PERSONEL LİSTESİ
        // -----------------------------------------------------------
        public IActionResult Index()
        {
            var personeller = _context.Personels
                .Include(p => p.Departman)
                .Where(p => p.Durum == true)
                .ToList();

            return View(personeller);
        }

        // -----------------------------------------------------------
        // YENİ PERSONEL (GET)
        // -----------------------------------------------------------
        [HttpGet]
        public IActionResult YeniPersonel()
        {
            ViewBag.Departmanlar = _context.Departmans
                .Where(x => x.Durum == true)
                .Select(x => new SelectListItem
                {
                    Text = x.DepartmanAd,
                    Value = x.DepartmanId.ToString()
                })
                .ToList();

            return View();
        }

        // -----------------------------------------------------------
        // YENİ PERSONEL (POST)
        // -----------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YeniPersonel(Personel p)
        {
           

            Console.WriteLine(">>> MODESTATE: " + ModelState.IsValid);
            Console.WriteLine(">>> AD: " + p.PersonelAd);
            Console.WriteLine(">>> SOYAD: " + p.PersonelSoyad);
            Console.WriteLine(">>> DEPARTMAN: " + p.DepartmanId);

            p.Durum = true;

            _context.Personels.Add(p);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // -----------------------------------------------------------
        // PERSONEL GÜNCELLE (GET)
        // -----------------------------------------------------------
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var personel = _context.Personels.Find(id);
            if (personel == null)
                return NotFound();

            ViewBag.Departmanlar = _context.Departmans
                .Where(x => x.Durum == true)
                .Select(x => new SelectListItem
                {
                    Text = x.DepartmanAd,
                    Value = x.DepartmanId.ToString()
                })
                .ToList();

            return View(personel);
        }

        // -----------------------------------------------------------
        // PERSONEL GÜNCELLE (POST)
        // -----------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guncelle(Personel p)
        {
            var mevcut = _context.Personels.Find(p.PersonelId);
            if (mevcut == null)
                return NotFound();

            mevcut.PersonelAd = p.PersonelAd;
            mevcut.PersonelSoyad = p.PersonelSoyad;
            mevcut.PersonelGorsel = p.PersonelGorsel;
            mevcut.DepartmanId = p.DepartmanId;
            mevcut.Durum = true;
            
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // -----------------------------------------------------------
        // PERSONEL SİL (Soft Delete)
        // -----------------------------------------------------------
        public IActionResult Sil(int id)
        {
            var personel = _context.Personels.Find(id);
            if (personel == null)
                return NotFound();

            personel.Durum = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Satislar(int id)
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

