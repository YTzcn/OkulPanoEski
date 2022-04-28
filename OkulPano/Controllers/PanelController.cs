    using System;
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
            var öğretmen = context.Öğretmens.Where(x => x.OkulId == Kullanıcı.OkulId).ToList();
            ViewBag.öğretmendeger = öğretmen;
            ViewBag.Kayaryazı = context.Okuls.Where(x => x.OkulId == Kullanıcı.OkulId).Select(x=>x.KayarYazı).FirstOrDefault();
            ViewBag.Tarih = DateTime.Now.ToString();
            Queue queue = new Queue();

            ViewBag.que = queue;

            
            var KayanResimList = context.KayanResims.Where(x => x.OkulId == Kullanıcı.OkulId).Select(x=>x.ResimYol).ToList();
            ViewBag.KayanList = KayanResimList;
            string Gün = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)DateTime.Now.DayOfWeek];
            Dictionary<NöbertYer, List<Öğretmen>> model = new Dictionary<NöbertYer, List<Öğretmen>>();
            
            List<NöbertYer> GünlükNöbetYerleri = new List<NöbertYer>();
            GünlükNöbetYerleri.AddRange(context.NöbertYers.Where(x => x.OkulId == Kullanıcı.OkulId));
            foreach (NöbertYer nöbetYer in GünlükNöbetYerleri)
            {
                List<Öğretmen> OGünküNöbetçiler= new List<Öğretmen>();
                OGünküNöbetçiler.AddRange(context.Öğretmens.Where(x => x.NöbetGün== Gün && x.NoöetYerId == nöbetYer.Id));
                model.Add(nöbetYer, OGünküNöbetçiler);
            }

            ViewBag.Modelk = model.Keys;
            ViewBag.Model = model;



            return View();
    
        
        }
    
    
    }
}