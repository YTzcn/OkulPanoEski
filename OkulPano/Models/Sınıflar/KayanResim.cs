using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OkulPano.Models.Sınıflar;
namespace OkulPano.Models.Sınıflar
{
    public class KayanResim
    {
        [Key]
        public int Id { get; set; }
        public int OkulId { get; set; }
        public string ResimYol { get; set; }
    }
}