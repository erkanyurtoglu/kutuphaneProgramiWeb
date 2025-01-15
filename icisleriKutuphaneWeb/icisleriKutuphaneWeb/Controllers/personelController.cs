using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using icisleriKutuphaneWeb.Models.Entity;

namespace icisleriKutuphaneWeb.Controllers
{
    public class personelController : Controller
    {
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: personel
        public ActionResult IndexPersonel(string search, int page = 1)
        {
            // Eğer arama terimi boş değilse, filtre uygula
            var personeller = from p in db.TBPERSONEL select p;

            if (!string.IsNullOrEmpty(search))
            {
                personeller = personeller.Where(p => p.personelAdSoyad.Contains(search)
                                                    || p.personelTcNumarasi.Contains(search)
                                                    || p.personelTelefon.Contains(search)
                                                    || p.personelEmail.Contains(search)
                                                    || p.personelUnvan.Contains(search));
            }

            // Arama terimi ve sayfalama bilgilerini ViewBag ile View'a gönder
            ViewBag.SearchTerm = search;

            // Filtrelenmiş listeyi sayfalama ile döndür
            var degerler = personeller.ToList();  // Sayfalama yapılacaksa ToPagedList kullanılabilir
            return View(degerler);
        }

        [HttpGet]
        public ActionResult personelEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult personelEkle(TBPERSONEL p) 
        {
            // Arka plandaki geçerliliği sağlayamadıysa personelEkle ye geri döndürdüm.
            if(!ModelState.IsValid)
            {
                return View("personelEkle");
            }

            db.TBPERSONEL.Add(p);
            db.SaveChanges();
            return View();
        }

        public ActionResult personelSil(string personelTcNumarasi) 
        {
            if (string.IsNullOrEmpty(personelTcNumarasi))
            {
                return RedirectToAction("IndexPersonel");
            }

            var person = db.TBPERSONEL.FirstOrDefault(p => p.personelTcNumarasi == personelTcNumarasi);
            if (person == null)
            {
                return HttpNotFound();
            }

            db.TBPERSONEL.Remove(person);
            db.SaveChanges();

            return RedirectToAction("IndexPersonel");
        }

        public ActionResult personelGetir(string personelTcNumarasi)
        {
            var prs = db.TBPERSONEL.Find(personelTcNumarasi);
            return View("personelGetir", prs);
        }

        public ActionResult personelGuncelle(TBPERSONEL p)
        {
            if (!ModelState.IsValid)
            {
                return View("personelGetir", p);
            }

            var prs = db.TBPERSONEL.Find(p.personelTcNumarasi);
            if (prs != null)
            {
                prs.personelAdSoyad = p.personelAdSoyad;
                prs.personelTelefon = p.personelTelefon;
                prs.personelEmail = p.personelEmail;
                prs.personelAdres = p.personelAdres;
                prs.personelUnvan = p.personelUnvan;

                db.SaveChanges();
            }

            return RedirectToAction("IndexPersonel");
        }
  
    }
}