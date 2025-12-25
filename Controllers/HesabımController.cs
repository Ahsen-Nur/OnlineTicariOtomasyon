using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.ViewModels;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.Linq;

public class HesabimController : Controller
{
    private readonly Context _context;

    public HesabimController(Context context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        int? cariId = HttpContext.Session.GetInt32("CariId");
        if (cariId == null)
            return RedirectToAction("GirisYap", "Login");

        var cari = _context.Carilers.Find(cariId.Value);

        var satinAlimlar = _context.SatisHarekets
            .Where(x => x.CariId == cariId)
            .Include(x => x.Urun)
            .OrderByDescending(x => x.Tarih)
            .ToList();

        var yorumlar = _context.UrunYorumlar
            .Where(x => x.CariId == cariId)
            .Include(x => x.Urun)
            .OrderByDescending(x => x.Tarih)
            .ToList();

        var model = new CariHesabimViewModel
        {
            Cari = cari,
            SatinAlimlar = satinAlimlar,
            Yorumlar = yorumlar,
            ToplamSiparis = satinAlimlar.Count,
            ToplamHarcama = satinAlimlar.Sum(x => x.ToplamTutar),
            SonAlisTarihi = satinAlimlar.FirstOrDefault()?.Tarih
        };

        return View(model);
    }



    [HttpPost]
    public IActionResult SifreDegistir(string EskiSifre, string YeniSifre)
    {
        int? cariId = HttpContext.Session.GetInt32("CariId");
        if (cariId == null)
            return RedirectToAction("GirisYap", "Login");
        var cari = _context.Carilers.Find(cariId.Value);
        if (cari == null)
            return RedirectToAction("Index");

        if (cari.Sifre != EskiSifre)
        {
            TempData["Hata"] = "Eski şifre yanlış.";
            return RedirectToAction("Index");
        }
        cari.Sifre = YeniSifre;
        _context.SaveChanges();

        TempData["Basarili"] = "Şifreniz güncellendi.";
        return RedirectToAction("Index");
}

}
