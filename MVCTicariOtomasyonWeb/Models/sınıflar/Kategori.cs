using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }

        [Required(ErrorMessage = "Kategori adı boş bırakılamaz")]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")] // İstersen bu satırı tamamen silebilirsin.
        public string KategoriAd { get; set; } = string.Empty;
        public bool Durum { get; set; } = true;

        // Navigation
        public ICollection<Urun>? Uruns { get; set; }
    }
}
