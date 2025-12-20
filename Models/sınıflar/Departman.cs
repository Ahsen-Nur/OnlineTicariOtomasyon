using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace MVCTicariOtomasyonWeb.Models.sınıflar 
{
    public class Departman
    {
        [Key]
        public int DepartmanId { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string DepartmanAd { get; set; }

        public bool Durum { get; set; }


        //ilişkiler
        //Her departmanda birden fazla personel olabilir
        public ICollection<Personel> Personels { get; set; }
    }
}