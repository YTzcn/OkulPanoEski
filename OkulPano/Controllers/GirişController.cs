using OkulPano.Models.Sınıflar;
using OkulPano.Models.ViewModels;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Mail;
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

                string mail = ovm.Okul.Mail;
                MailMessage mesajım = new MailMessage();//mesaj oluşturma
                SmtpClient istemci = new SmtpClient();//istemci oluştuırma
                istemci.Credentials = new System.Net.NetworkCredential("uygulamachatapp@gmail.com", "yahya2005");//gönderen mail adresi şifresi
                istemci.Port = 587;//istemci portu
                istemci.Host = "smtp.gmail.com";//istemci adresi
                istemci.EnableSsl = true;//ssl etkinleştime 
                mesajım.To.Add(mail);//kullanıcı mail adresi
                mesajım.From = new MailAddress("uygulamachatapp@gmail.com");//gönderen mail adresi
                mesajım.Subject = "a";//başlık 
                mesajım.Body = "https://localhost:44382/Giriş/HesapOnay?kod=" + Aktivasyon;//ana konu
                istemci.Send(mesajım);//mail gönderme komutu 

                return RedirectToAction("Index");
            }
            else
            {
                TempData["GüvenlikKoduHata"] = "Güvenlik Kodu Yanlış!";
                return RedirectToAction("Index");
            }
        }
   
        public ActionResult HesapOnay(string kod) 
        {
            var aktif = context.Okuls.FirstOrDefault(x => x.AktivasyonKod == kod);
            aktif.Aktif = true;
            context.SaveChanges();

            return RedirectToAction("Index");
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

