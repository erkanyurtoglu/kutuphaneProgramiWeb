using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using icisleriKutuphaneWeb.Models.Entity;
using System.Web.Security;

namespace icisleriKutuphaneWeb.Controllers
{
    public class LoginController : Controller
    {
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();
        // GET: Login
        public ActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(TBUYELER p)
        {
            var bilgiler = db.TBUYELER.FirstOrDefault(x => x.uyeKullaniciAd == p.uyeKullaniciAd && x.uyeSifre == p.uyeSifre);
            
            if (bilgiler != null) 
            {
                FormsAuthentication.SetAuthCookie(bilgiler.uyeKullaniciAd, false);
                Session["kullaniciAd"] = bilgiler.uyeKullaniciAd.ToString();
                return RedirectToAction("IndexPanelim", "Panelim");
            }
            else
            {
                return View();
            }
        }
    }
}