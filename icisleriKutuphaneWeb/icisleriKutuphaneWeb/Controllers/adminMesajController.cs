using icisleriKutuphaneWeb.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace icisleriKutuphaneWeb.Controllers
{
    public class adminMesajController : Controller
    {
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: adminMesaj
        public ActionResult IndexAdminMesaj()
        {
            // Oturumdaki kullanıcı adını al
            var kullaniciAd = Session["personelKullaniciAd"]?.ToString();

            // Kullanıcı adıyla e-posta adresini al
            var email = db.TBPERSONEL.FirstOrDefault(x => x.personelKullaniciAd == kullaniciAd)?.personelEmail;

            // Email adresine göre mesajları filtrele
            var mesajlar = db.TBMESAJ.Where(x => x.alici == email).ToList();

            // Mesajları görünüme gönder
            return View(mesajlar);
        }

        public ActionResult adminGidenMesaj()
        {
            // Oturumdaki kullanıcı adını al
            var kullaniciAd = Session["personelKullaniciAd"]?.ToString();

            // Kullanıcı adıyla e-posta adresini al
            var personelmail = db.TBPERSONEL.FirstOrDefault(x => x.personelKullaniciAd == kullaniciAd)?.personelEmail;

            var mesajlar = db.TBMESAJ.Where(x => x.gonderen == personelmail).ToList();
            return View(mesajlar);
        }

        [HttpGet]
        public ActionResult adminYeniMesaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult adminYeniMesaj(TBMESAJ t)
        {
            // Oturumdaki kullanıcı adını al
            var kullaniciAd = Session["personelKullaniciAd"]?.ToString();

            // Kullanıcı adıyla e-posta adresini al
            var personelmail = db.TBPERSONEL.FirstOrDefault(x => x.personelKullaniciAd == kullaniciAd)?.personelEmail;

            t.gonderen = personelmail;
            t.tarih = DateTime.Now;
            db.TBMESAJ.Add(t);
            db.SaveChanges();
            return RedirectToAction("adminGidenMesaj", "adminMesaj");
        }

    }
}