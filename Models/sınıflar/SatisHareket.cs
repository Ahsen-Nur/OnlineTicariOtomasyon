using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class SatisHareket
    {
        [Key]
        public int SatisId { get; set; }

        public DateTime Tarih { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal ToplamTutar { get; set; }


        //İlişkiler
        
        public int UrunId { get; set; }
        [ForeignKey("UrunId")]
        public virtual Urun Urun { get; set; }

        
        public int CariId { get; set; }
        [ForeignKey("CariId")]
        public virtual Cariler Cariler { get; set; }


        public int PersonelId { get; set; }
        [ForeignKey("PersonelId")]
        public virtual Personel Personel { get; set; }
    }
}
