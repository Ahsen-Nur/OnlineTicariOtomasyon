using Microsoft.AspNetCore.Mvc;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

public class UrunYorumController : Controller
{
    private readonly Context _context;

    public UrunYorumController(Context context)
    {
        _context = context;
    }

    // YORUM EKLE
    [HttpPost]
    public IActionResult Ekle(UrunYorum y)
    {
        int? cariId = HttpContext.Session.GetInt32("CariId");

        // Giriş yoksa login'e at
        if (cariId == null)
        {
            return RedirectToAction("GirisYap", "Login");
        }

        var cari = _context.Carilers.Find(cariId.Value);
        if (cari == null)
        {
            return RedirectToAction("Index", "Kategori");
        }

        y.CariId = cariId.Value;
        y.KullaniciAd = cari.CariAd + " " + cari.CariSoyad;
        y.Tarih = DateTime.Now;

        _context.UrunYorumlar.Add(y);
        _context.SaveChanges();

        return RedirectToAction("Index", "UrunDetay", new { id = y.UrunId });
    }

    // YORUM SİL
    public IActionResult Sil(int id)
    {
        var yorum = _context.UrunYorumlar.FirstOrDefault(x => x.YorumId == id);
        if (yorum == null)
        {
            return NotFound();
        }

        int? aktifCariId = HttpContext.Session.GetInt32("CariId");
        int? adminId = HttpContext.Session.GetInt32("AdminId");

        // 🔐 YETKİ KONTROLÜ
        // Admin HER yorumu silebilir
        // Cari SADECE kendi yorumunu silebilir
        if (adminId == null && (aktifCariId == null || yorum.CariId != aktifCariId))
        {
            return Unauthorized(); // 401
        }

        _context.UrunYorumlar.Remove(yorum);
        _context.SaveChanges();

        return RedirectToAction("Index", "UrunDetay", new { id = yorum.UrunId });
    }

    // YORUM DÜZENLEME EKRANI
    [HttpGet]
    public IActionResult Duzenle(int id)
    {
        var yorum = _context.UrunYorumlar.FirstOrDefault(x => x.YorumId == id);
        if (yorum == null)
            return NotFound();

        int? aktifCariId = HttpContext.Session.GetInt32("CariId");
        int? adminId = HttpContext.Session.GetInt32("AdminId");

        // Yetki kontrolü
        if (adminId == null && (aktifCariId == null || yorum.CariId != aktifCariId))
            return Unauthorized();
        return View(yorum);
    }


// YORUM GÜNCELLE
    [HttpPost]
    public IActionResult Duzenle(UrunYorum y)
    {
        var yorum = _context.UrunYorumlar.FirstOrDefault(x => x.YorumId == y.YorumId);
        if (yorum == null)
            return NotFound();

        int? aktifCariId = HttpContext.Session.GetInt32("CariId");
        int? adminId = HttpContext.Session.GetInt32("AdminId");

        if (adminId == null && (aktifCariId == null || yorum.CariId != aktifCariId))
            return Unauthorized();  
            
        yorum.Yorum = y.Yorum;
        yorum.Puan = y.Puan;
        yorum.Tarih = DateTime.Now;

        _context.SaveChanges();
        return RedirectToAction("Index", "UrunDetay", new { id = yorum.UrunId });
}

}
