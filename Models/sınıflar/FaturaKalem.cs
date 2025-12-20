using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace MVCTicariOtomasyonWeb.Models.sınıflar 
{
    public class FaturaKalem
    {
        [Key]
        public int FaturaKalemId { get; set; }
        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal Tutar { get; set; }
        

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Aciklama { get; set; }


        //ilişkiler
        //Her kalem bir faturaya bağlıdır

        
        public int FaturaId { get; set; }
        [ForeignKey("FaturaId")]
        public virtual Faturalar Faturalar { get; set; }
    }
}