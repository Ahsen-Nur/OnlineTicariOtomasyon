using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class KargoDetay
    {
        [Key]
        public int KargoDetayId { get; set; }

        // Kargo
        public int KargoId { get; set; }

        [ForeignKey("KargoId")]
        public virtual Kargo Kargo { get; set; }

        // Satış
        public int SatisId { get; set; }

        [ForeignKey("SatisId")]
        public virtual SatisHareket SatisHareket { get; set; }
    }
}
