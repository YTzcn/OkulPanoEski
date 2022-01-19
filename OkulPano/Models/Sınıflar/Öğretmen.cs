using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OkulPano.Models.Sınıflar
{
    public class Öğretmen
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public int Soyad { get; set; }
        public string NöbetGün { get; set; }
        public string NöbetYer { get; set; }
    }
}