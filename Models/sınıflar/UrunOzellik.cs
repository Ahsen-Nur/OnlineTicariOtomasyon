using System.ComponentModel.DataAnnotations;


namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class UrunOzellik
    {
        [Key]
        public int OzellikId { get; set; }
        public string OzellikAd { get; set; }
    }
}
