using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class UrunYorum
    {
        [Key]
        public int YorumId { get; set; }

        public int UrunId { get; set; }
        [ForeignKey("UrunId")]
        public Urun Urun { get; set; }

        // 🔥 EKLENMESİ GEREKEN ALAN
        public int CariId { get; set; }
        [ForeignKey("CariId")]
        public Cariler Cariler { get; set; }

        public string KullaniciAd { get; set; }
        public string Yorum { get; set; }
        public int Puan { get; set; }
        public DateTime Tarih { get; set; }
    }
}
