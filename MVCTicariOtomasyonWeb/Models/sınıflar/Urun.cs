using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace MVCTicariOtomasyonWeb.Models.sınıflar 
{
    public class Urun
    {
        [Key]
        public int UrunId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string UrunAd { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Marka { get; set; }

        public short Stok { get; set; }
        public decimal AlisFiyat { get; set; }
        public decimal SatisFiyat { get; set; }
        public bool Durum { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string UrunGorsel { get; set; }
        

        //İlişki: her ürün bir kategoriye ait
        public int KategoriId { get; set; }
        [ForeignKey("KategoriId")]
        public Kategori Kategori { get; set; }

        //Bir ürün birçok satışta yer alabilir
        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}