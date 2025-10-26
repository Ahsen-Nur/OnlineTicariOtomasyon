using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace MVCTicariOtomasyonWeb.Models.sınıflar 
{
    public class Cariler
    {
        [Key]
        public int CariId { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string CariAd { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]  
        public string CariSoyad { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(13)]
        public string CariSehir { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CariMail { get; set; }


        //ilişkiler
        
        //Bir cariye ait birden fazla satış olabilir
        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}