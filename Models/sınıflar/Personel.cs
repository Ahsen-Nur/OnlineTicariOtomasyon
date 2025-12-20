using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class Personel
    {
        [Key]
        public int PersonelId { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "Varchar(30)")]
        public string PersonelAd { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "Varchar(30)")]
        public string PersonelSoyad { get; set; }

        [StringLength(250)]
        [Column(TypeName = "Varchar(250)")]
        public string? PersonelGorsel { get; set; }


        public bool Durum { get; set; }

        [Required]
        public int DepartmanId { get; set; }

        [ForeignKey("DepartmanId")]
        public virtual Departman Departman { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}
