﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OkulPano.Models.Sınıflar
{
    public class Resim
    {
        [Key]
        public int ResimId { get; set; }
        public string ResimYol { get; set; }
        public int OkulId { get; set; }

    }
}