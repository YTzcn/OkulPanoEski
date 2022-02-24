﻿using OkulPano.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OkulPano.Models.ViewModels
{
    public class OkulViewModel
    {
        public Okul Okul { get; set; }
        public Resim Resim { get; set; }
        public NöbertYer NöbertYer{ get; set; }
        public Öğretmen öğretmen { get; set; }
        public Context context{ get; set; }
        

        public string Guvenlik { get; set; }
    }
  
}