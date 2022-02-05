using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OkulPano.Models.Sınıflar
{
    public class NöbertYer
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public int OkulId { get; set; }
        public ICollection<Öğretmen> Öğretmens { get; set; }


    }
}