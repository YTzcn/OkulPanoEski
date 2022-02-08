using OkulPano.Models.Sınıflar;
using System.IO;
using System.Linq;
using System.Web.Mvc;
namespace OkulPano.Controllers
{
    public class ÜyeAdminController : Controller
    {
        // GET: ÜyeAdmin
        Context context = new Context();
        public ActionResult Index()
        {
            string KullanıcıMail = Session["Mail"].ToString();
            var Kullanıcı = context.Okuls.FirstOrDefault(x => x.Mail == KullanıcıMail);
            ViewBag.OkulAd = Kullanıcı.Ad;
            string Tarih = Kullanıcı.KayıtTarihi.ToString();
            Tarih = Tarih.Remove(9);
            ViewBag.Üyelik = Tarih;
            ViewBag.Adres = Kullanıcı.Adres;
            ViewBag.KayanYazı = Kullanıcı.KayarYazı;

            var VerileriÇek = context.Okuls.Find(Kullanıcı.OkulId);


            return View("Index", VerileriÇek);
        }
        [HttpGet]
        public ActionResult SifreGuncelle()
        {

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SifreGuncelle(string EskiS, string YeniS)
        {
            string KullanıcıMail = Session["Mail"].ToString();
            var Kullanıcı = context.Okuls.FirstOrDefault(x => x.Mail == KullanıcıMail);
            string Şifre = Kullanıcı.Şifre;
            if (EskiS == Şifre)
            {
                Kullanıcı.Şifre = YeniS;
                context.SaveChanges();
                ViewBag.Renk = "green";
                TempData["Hatalı"] = "Şifre Değişikliği Başarılı";

            }
            else
            {
                ViewBag.Renk = "red";
                TempData["Hatalı"] = "Eski şifre hatalı";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult BilgiGuncelle()
        {

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult BilgiGuncelle(Okul okul)
        {
            try
            {
                var Değişecek = context.Okuls.Find(okul.OkulId);
                Değişecek.Ad = okul.Ad;
                Değişecek.Adres = okul.Adres;
                Değişecek.Web = okul.Web;
                Değişecek.MüdürAd = okul.MüdürAd;
                Değişecek.MüdürSoyad = okul.MüdürSoyad;
                context.SaveChanges();
                TempData["Guncelle"] = "Güncelleme İşlemi Başarılı";
            }
            catch
            {
                TempData["Guncelle"] = "İşlem Başarısız";
                throw;
            }


            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult KayarYaziGuncelle(Okul p)
        {
            string KullanıcıMail = Session["Mail"].ToString();
            var Kullanıcı = context.Okuls.FirstOrDefault(x => x.Mail == KullanıcıMail);
            Kullanıcı.KayarYazı = p.KayarYazı;

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ResimEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResimEkle(Resim R)
        {

            string KullanıcıMail = Session["Mail"].ToString();
            var Kullanıcı = context.Okuls.FirstOrDefault(x => x.Mail == KullanıcıMail);
            if (Request.Files.Count > 0)
            {
                string DosyaAdı = Path.GetFileName(Request.Files[0].FileName);
                string yol = "~/Resimler/" + Kullanıcı.OkulId + "/" + DosyaAdı;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                R.OkulId = Kullanıcı.OkulId;
                R.ResimYol = yol.ToString();
            }
            context.Resims.Add(R);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ProfilResmiYükle(Resim R)
        {
            string KullanıcıMail = Session["Mail"].ToString();
            var Kullanıcı = context.Okuls.FirstOrDefault(x => x.Mail == KullanıcıMail);
            if (Request.Files.Count > 0)
            {
                string DosyaAdı = Path.GetFileName(Request.Files[0].FileName);

                string yol = "~/Resimler/Profil/" + DosyaAdı;
                R.OkulId = Kullanıcı.OkulId;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                R.ResimYol = yol.ToString();
            }
            context.Resims.Add(R);
            context.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult Öğretmen()
        {
            string KullanıcıMail = Session["Mail"].ToString();
            var Kullanıcı = context.Okuls.FirstOrDefault(x => x.Mail == KullanıcıMail);
            var Öğretmen = context.Öğretmens.Where(x => x.OkulId == Kullanıcı.OkulId).ToList();

            return View(Öğretmen);
        }
        [HttpGet]
        public ActionResult ÖğretmenEkle()
        {
            string KullanıcıMail = Session["Mail"].ToString();
            var Kullanıcı = context.Okuls.FirstOrDefault(x => x.Mail == KullanıcıMail);
            var Nöbet = context.NöbertYers.Where(x => x.OkulId == Kullanıcı.OkulId).ToList();
          
            return View(Nöbet);
        }
        [HttpPost]
        public ActionResult ÖğretmenEkle(Öğretmen öğretmen)
        {
            string KullanıcıMail = Session["Mail"].ToString();
            var Kullanıcı = context.Okuls.FirstOrDefault(x => x.Mail == KullanıcıMail);
            öğretmen.OkulId = Kullanıcı.OkulId;
            context.Öğretmens.Add(öğretmen);
            context.SaveChanges();
            return RedirectToAction("Öğretmen");
        }

        public ActionResult Nöbet() 
        {

            return View();
        }
        



    }
}