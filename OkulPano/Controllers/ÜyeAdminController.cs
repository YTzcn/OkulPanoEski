using OkulPano.Models.Sınıflar;
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
            var Değişecek= context.Okuls.Find(okul.OkulId);
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
    }
}