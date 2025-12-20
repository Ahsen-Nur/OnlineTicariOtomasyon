using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace MVCTicariOtomasyonWeb.Models.sınıflar 
{
    public class Faturalar
    {
        [Key]
        public int FaturaId { get; set; }


        [Column(TypeName = "char")]
        [StringLength(1)]
        public string FaturaSeriNo { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(6)]
        public string FaturaSıraNo { get; set; }


        public DateTime Tarih { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(60)]
        public string VergiDairesi { get; set; }


        public string Saat { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string TeslimEden { get; set; }
        

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string TeslimAlan { get; set; }


        //İlişki
        public ICollection<FaturaKalem> FaturaKalems { get; set; }
    }

}