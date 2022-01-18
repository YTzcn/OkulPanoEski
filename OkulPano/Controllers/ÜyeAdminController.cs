using OkulPano.Models.Sınıflar;
using System.Linq;
using System.Web.Mvc;
namespace OkulPano.Controllers
{
    public class ÜyeAdminController : Controller
    {
        // GET: ÜyeAdmin
        //View Olarak Layout Kullanma.
        Context context = new Context();
        public ActionResult Index()
        {
            string KullanıcıMail = Session["Mail"].ToString();
            var Kullanıcı = context.Okuls.FirstOrDefault(x => x.Mail == KullanıcıMail);
            ViewBag.OkulAd= Kullanıcı.Ad;

            
            return View();
        }
    }
}