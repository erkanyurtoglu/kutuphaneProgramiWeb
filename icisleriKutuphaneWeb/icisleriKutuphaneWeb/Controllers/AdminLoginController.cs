using icisleriKutuphaneWeb.Models.Entity;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace icisleriKutuphaneWeb.Controllers
{
    public class AdminLoginController : Controller
    {
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: AdminLogin
        public ActionResult IndexAdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IndexAdminLogin(TBPERSONEL p)
        {
            if (ModelState.IsValid)
            {
                var bilgiler = db.TBPERSONEL.FirstOrDefault(x => x.personelKullaniciAd == p.personelKullaniciAd &&
                    x.personelSifre == p.personelSifre);

                if (bilgiler != null)
                {
                    FormsAuthentication.SetAuthCookie(bilgiler.personelKullaniciAd, false);
                    Session["personelKullaniciAd"] = bilgiler.personelKullaniciAd.ToString();
                    Session["personelEmail"] = bilgiler.personelEmail; // E-posta adresini oturumda sakla
                    return RedirectToAction("IndexKitap", "kitap");
                }
                else
                {
                    ViewBag.HataMesaji = "Kullanıcı adı veya şifre yanlış.";
                    return View();
                }
            }

            ViewBag.HataMesaji = "Lütfen tüm alanları doldurun.";
            return View();
        }
    }
}
