using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class Kargo
    {
        [Key]
        public int KargoId { get; set; }

        public string KargoFirmasi { get; set; }
        public string TakipKodu { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime? TeslimTarihi { get; set; }

        public string Durum { get; set; } // Kargoda, Teslim Edildi

        public ICollection<KargoDetay> KargoDetaylar { get; set; }


    }

}
