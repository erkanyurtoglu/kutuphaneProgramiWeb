using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using icisleriKutuphaneWeb.Models.Entity;

namespace icisleriKutuphaneWeb.Controllers
{
    public class mesajController : Controller
    {
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: mesaj
        public ActionResult IndexMesaj()
        {
            // Oturumdaki kullanıcının email adresini al
            var email = Session["uyeEmail"]?.ToString();

            // Email adresine göre mesajları filtrele
            var mesajlar = db.TBMESAJ.Where(x => x.alici == email).ToList();

            // Mesajları görünüme gönder
            return View(mesajlar);
        }


        public ActionResult GidenMesaj()
        {
            var uyemail = (string)Session["uyeEmail"].ToString();
            var mesajlar = db.TBMESAJ.Where(x => x.gonderen == uyemail.ToString()).ToList();
            return View(mesajlar);
        }


        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(TBMESAJ t)
        {
            var uyemail = (string)Session["uyeEmail"].ToString();
            t.gonderen = uyemail.ToString();
            t.tarih = DateTime.Parse(DateTime.Now.ToString());
            db.TBMESAJ.Add(t);
            db.SaveChanges();
            return RedirectToAction("GidenMesaj", "mesaj");
        }

        public PartialViewResult Partial1()
        {
            var uyemail = (string)Session["uyeEmail"].ToString();
            var gelensayisi = db.TBMESAJ.Where(x => x.alici == uyemail).Count();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = db.TBMESAJ.Where(x => x.gonderen == uyemail).Count();
            ViewBag.d2 = gidensayisi;

            return PartialView();
        }
    }
}