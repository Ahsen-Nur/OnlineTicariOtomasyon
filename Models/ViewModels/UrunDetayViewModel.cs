using MVCTicariOtomasyonWeb.Models.sınıflar;
using System.Collections.Generic;

namespace MVCTicariOtomasyonWeb.Models.ViewModels
{
    public class UrunDetayViewModel
    {
        public Urun Urun { get; set; }

        public List<UrunOzellikDeger> Ozellikler { get; set; }
        public UrunAciklama Aciklama { get; set; }
        public List<UrunYorum> Yorumlar { get; set; }
    }
}
