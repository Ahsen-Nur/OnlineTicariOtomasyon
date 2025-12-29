using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class Iade
    {
        [Key]
        public int IadeId { get; set; }

        public int SatisId { get; set; }

        [ForeignKey("SatisId")]
        public virtual SatisHareket SatisHareket { get; set; }

        public DateTime TalepTarihi { get; set; }

        public string Durum { get; set; } // Beklemede, Onaylandı, Reddedildi

        public string Aciklama { get; set; }
    }
}
