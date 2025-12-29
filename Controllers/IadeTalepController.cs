using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.sınıflar;
using Microsoft.AspNetCore.Http;
using System;

public class IadeTalepController : Controller
{
    private readonly Context _context;

    public IadeTalepController(Context context)
    {
        _context = context;
    }

    public IActionResult TalepEt(int satisId)
    {
        int? cariId = HttpContext.Session.GetInt32("CariId");
        if (cariId == null)
            return RedirectToAction("GirisYap", "Login");

        var satis = _context.SatisHarekets
            .Include(s => s.Urun)
            .FirstOrDefault(s => s.SatisId == satisId && s.CariId == cariId);

        if (satis == null || satis.SiparisDurum != "Teslim Edildi")
            return RedirectToAction("Index", "Hesabim");

        var iade = new Iade
        {
            SatisId = satisId,
            TalepTarihi = DateTime.Now,
            Durum = "Beklemede",
            Aciklama = "Cari tarafından iade talebi oluşturuldu."

        };

        _context.Iades.Add(iade);

        satis.SiparisDurum = "İade Beklemede";

        _context.SaveChanges();

        return RedirectToAction("Index", "Hesabim");
    }
}
