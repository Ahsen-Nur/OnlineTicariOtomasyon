using System;
using System.ComponentModel.DataAnnotations;

namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class Mesaj
    {
        [Key]
        public int MesajId { get; set; }

        public string GonderenMail { get; set; }
        public string AliciMail { get; set; }

        public string Konu { get; set; }
        public string Icerik { get; set; }

        public DateTime Tarih { get; set; }
        public bool Okundu { get; set; }
    }
}
