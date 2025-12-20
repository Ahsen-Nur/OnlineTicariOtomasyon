using System.ComponentModel.DataAnnotations;

namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class KategoriOzellik
    {
        [Key]
        public int KategoriOzellikId { get; set; }

        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }

        public int OzellikId { get; set; }
        public UrunOzellik Ozellik { get; set; }
    }
}
