using System;
using System.ComponentModel.DataAnnotations;


namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class UrunYorum
    {
        [Key]
        public int YorumId { get; set; }
        public int UrunId { get; set; }
        public string KullaniciAd { get; set; }
        public string Yorum { get; set; }
        public DateTime Tarih { get; set; }

        public Urun Urun { get; set; }
    }
}
