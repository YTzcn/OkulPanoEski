using OkulPano.Models.Sınıflar;
using OkulPano.Models.ViewModels;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace OkulPano.Controllers
{
    public class GirişController : Controller
    {
        // GET: Giriş
        Context context = new Context();

        Guid Aktivasyon = Guid.NewGuid();


        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(Okul okul)
        {
            var Bilgiler = context.Okuls.FirstOrDefault(x => x.Mail == okul.Mail && x.Şifre == okul.Şifre); 
            TempData["GirişHata"] = "Kullanıcı Adı Veya Şifre Hatalı!";
            if (Bilgiler != null)
            {
                if (Bilgiler.Aktif == true)
                {
                FormsAuthentication.SetAuthCookie(Bilgiler.Mail, false);
                Session["Mail"] = Bilgiler.Mail.ToString();
                return RedirectToAction("Index", "ÜyeAdmin");
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
            }
            else
               
                return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Register()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Register(OkulViewModel ovm)
        {
            string KoddanGelen = Session["GuvenlikResmi"].ToString();
            if (ovm.Guvenlik == KoddanGelen)
            {
                var Bilgiler = context.Okuls.Add(ovm.Okul);
                Bilgiler.AktivasyonKod = Aktivasyon.ToString();
                Bilgiler.KayıtTarihi = DateTime.Today;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["GüvenlikKoduHata"] = "Güvenlik Kodu Yanlış!";
                return RedirectToAction("Index");
            }
        }

        public Bitmap ResimGonder()
        {
            GuvenlikResmi gr = new GuvenlikResmi(5, "Arial", 25F);
            Bitmap bmp = gr.GuvenlikResmiGonder();
            if (Session["GuvenlikResmi"] == null)
            {
                Session.Add("GuvenlikResmi", gr.Sayi);
            }
            else
            {
                Session["GuvenlikResmi"] = gr.Sayi;
            }
            Response.ContentType = "image/jpeg";
            bmp.Save(Response.OutputStream, ImageFormat.Jpeg);


            return bmp;
        }

    }
}

