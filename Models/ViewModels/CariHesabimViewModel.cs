using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.Collections.Generic;

namespace MVCTicariOtomasyonWeb.Models.ViewModels
{
    public class CariHesabimViewModel
    {
        // Profil
        public Cariler Cari { get; set; }

        // Satın Alımlar
        public List<SatisHareket> SatinAlimlar { get; set; }

        // Yorumlar
        public List<UrunYorum> Yorumlar { get; set; }

        // Dashboard
        public int ToplamSiparis { get; set; }
        public decimal ToplamHarcama { get; set; }
        public System.DateTime? SonAlisTarihi { get; set; }
    }
}
