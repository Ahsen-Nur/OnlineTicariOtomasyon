using System.ComponentModel.DataAnnotations;

namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class UrunAciklama
    {
        [Key]
        public int AciklamaId { get; set; }
        public int UrunId { get; set; }
        public string Metin { get; set; }

        public Urun Urun { get; set; }
    }
}
