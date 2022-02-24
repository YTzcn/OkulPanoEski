﻿    using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OkulPano.Models.ViewModels;
using OkulPano.Models.Sınıflar;

namespace OkulPano.Controllers
{
    public class PanelController : Controller
    {
        // GET: Panel
        Context context = new Context();
        OkulViewModel ovm = new OkulViewModel();
        public ActionResult Index()
        {
            string KullanıcıMail = Session["Mail"].ToString();
            var Kullanıcı = context.Okuls.FirstOrDefault(x => x.Mail == KullanıcıMail);
            var KullanıcıR = context.Resims.FirstOrDefault(x => x.OkulId == Kullanıcı.OkulId);
            ViewBag.OkulAd = Kullanıcı.Ad;
            System.IO.FileInfo ff = new System.IO.FileInfo(KullanıcıR.ResimYol);
            string DosyaUzantisi = ff.Extension;
            ViewBag.Logo =  Kullanıcı.OkulId+DosyaUzantisi;
            var NöbetYerler = context.NöbertYers.Where(x => x.OkulId == Kullanıcı.OkulId && x.Aktiflik == true).ToList();
            
            
      

            return View(NöbetYerler);
        }
    }
}