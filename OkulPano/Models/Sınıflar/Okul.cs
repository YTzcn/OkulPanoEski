using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OkulPano.Models.Sınıflar
{
    public class Okul
    {
        [Key]
        public int OkulId { get; set; }
        public string Ad { get; set; }
        public string Adres { get; set; }
        public string Mail { get; set; }
        public string Web { get; set; }
        public string  MüdürAd { get; set; }
        public string  MüdürSoyad { get; set; }
        public string  Şifre { get; set; }
        public string  KayarYazı { get; set; }
        public string  AktivasyonKod { get; set; }
        //boolean aktiflik durumu yapılacak

    }
}