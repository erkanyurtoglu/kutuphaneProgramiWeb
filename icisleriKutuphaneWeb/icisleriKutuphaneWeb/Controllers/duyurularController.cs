using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using icisleriKutuphaneWeb.Models.Entity;

namespace icisleriKutuphaneWeb.Controllers
{
    public class duyurularController : Controller
    {
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: duyurular
        public ActionResult IndexDuyurular()
        {
            var degerler = db.TBDUYURULAR.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniDuyuru(TBDUYURULAR t)
        {
            db.TBDUYURULAR.Add(t);
            db.SaveChanges();
            return RedirectToAction("IndexDuyurular");
        }

        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.TBDUYURULAR.Find(id);
            db.TBDUYURULAR.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("IndexDuyurular");
        }

        [HttpGet]
        public ActionResult DuyuruDetay(int id)
        {
            var duyuru = db.TBDUYURULAR.Find(id);
            return View(duyuru);
        }

        [HttpPost]
        public ActionResult DuyuruGuncelle(TBDUYURULAR t)
        {
            var duyuru = db.TBDUYURULAR.Find(t.ID);
            if (duyuru != null)
            {
                duyuru.kategori = t.kategori;
                duyuru.icerik = t.icerik;
                duyuru.tarih = t.tarih;
                db.SaveChanges();
            }
            return RedirectToAction("IndexDuyurular");
        }
    }
}