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
        GuvenlikResmi gr = new GuvenlikResmi(5, "Arial", 25F);
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
            if (Bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(Bilgiler.Mail, false);
                Session["Mail"] = Bilgiler.Mail.ToString();
                return RedirectToAction("Index");
            }
            else
                return View();

        }
        [HttpGet]
        public ActionResult Register()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Register(OkulViewModel ovm)
        {
#pragma warning disable CS0252 // İstenmeden yapılmış olabilecek başvuru karşılaştırması, sol taraf için atama gerekiyor
            if (Session["GuvenlikResmi"] == ovm.Guvenlik)
#pragma warning restore CS0252 // İstenmeden yapılmış olabilecek başvuru karşılaştırması, sol taraf için atama gerekiyor
            {
                var Bilgiler = context.Okuls.Add(ovm.Okul);
                Bilgiler.AktivasyonKod = Aktivasyon.ToString();
                context.SaveChanges();
            }
            else
            {
                return RedirectToAction("sfsdf");
            }





            return View();
        }

        public PartialViewResult GuvenlikResmi()
        {
            ResimGönder();
            return PartialView();
        }
        public Bitmap ResimGönder()
        {

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

