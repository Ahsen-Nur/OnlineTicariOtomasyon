using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace MVCTicariOtomasyonWeb.Models.sınıflar 
{
    public class Personel
    {
        [Key]
        public int PersonelId { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string PersonelAd { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string PersonelSoyad { get; set; }
        

        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string PersonelGorsel { get; set; }


        //ilişkiler
        //her personel bir departmana ait
        
        public int DepartmanId { get; set; }
        [ForeignKey("DepartmanId")]
        public virtual Departman Departman { get; set; }

        //Bir personelin birden fazla satış kaydı olabilir
        public ICollection<SatisHareket> SatisHarekets { get; set; }
       
    }
}