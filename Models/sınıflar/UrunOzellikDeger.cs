using System.ComponentModel.DataAnnotations;


namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class UrunOzellikDeger
    {
        [Key]
        public int Id { get; set; }

        public int UrunId { get; set; }
        public Urun Urun { get; set; }

        public int OzellikId { get; set; }
        public UrunOzellik Ozellik { get; set; }

        public string Deger { get; set; }
    }
}
