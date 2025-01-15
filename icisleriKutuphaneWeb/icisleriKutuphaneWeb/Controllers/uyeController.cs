using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using icisleriKutuphaneWeb.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace icisleriKutuphaneWeb.Controllers
{
    public class uyeController : Controller
    {
        // GET: uye
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();
        public ActionResult IndexUye(string search, int sayfa = 1)
        {
            var degerler = db.TBUYELER.AsQueryable();  // Sorguyu oluşturuyoruz ancak henüz çalıştırmıyoruz

            // Eğer arama terimi boş değilse üyeleri arama terimine göre filtreliyoruz
            if (!string.IsNullOrEmpty(search))
            {
                degerler = degerler.Where(u => u.uyeAdSoyad.Contains(search) ||
                                               u.uyeTcNumarasi.Contains(search) ||
                                               u.uyeTelefon.Contains(search) ||
                                               u.uyeEmail.Contains(search));
            }

            // Sorguyu çalıştırıyor ve sayfalama işlemi yapıyoruz
            var sonuc = degerler.OrderBy(u => u.uyeAdSoyad).ToPagedList(sayfa, 6);

            return View(sonuc);
        }

        [HttpGet]
        public ActionResult uyeEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uyeEkle(TBUYELER u)
        {
            // Arka plandaki geçerliliği sağlayamadıysa personelEkle ye geri döndürdüm.
            if (!ModelState.IsValid)
            {
                return View("uyeEkle");
            }
            db.TBUYELER.Add(u);
            db.SaveChanges();
            return View();
        }

        public ActionResult uyeSil(string uyeTcNumarasi)
        {
            if (string.IsNullOrEmpty(uyeTcNumarasi))
            {
                return RedirectToAction("IndexUye");
            }

            var uye = db.TBUYELER.FirstOrDefault(u => u.uyeTcNumarasi == uyeTcNumarasi);
            if (uye == null)
            {
                return HttpNotFound();
            }

            db.TBUYELER.Remove(uye);
            db.SaveChanges();

            return RedirectToAction("IndexUye");
        }

        public ActionResult uyeGetir(string uyeTcNumarasi)
        {
            var uye = db.TBUYELER.Find(uyeTcNumarasi);
            return View("uyeGetir", uye);
        }

        public ActionResult uyeGuncelle(TBUYELER u)
        {
            if (!ModelState.IsValid)
            {
                return View("uyeGetir", u);
            }

            var uye = db.TBUYELER.Find(u.uyeTcNumarasi);
            if (uye != null)
            {
                uye.uyeAdSoyad = u.uyeAdSoyad;
                uye.uyeTcNumarasi = u.uyeTcNumarasi;
                uye.uyeTelefon= u.uyeTelefon;
                uye.uyeEmail = u.uyeEmail;
                uye.uyeAdres = u.uyeAdres;
                uye.calistigiBirim = u.calistigiBirim;
                uye.unvan = u.unvan;
                uye.isTelefonu = u.isTelefonu;
                uye.oduncNot = u.oduncNot;
                uye.uyeKullaniciAd = u.uyeKullaniciAd;
                uye.uyeSifre = u.uyeSifre;

                db.SaveChanges();
            }

            return RedirectToAction("IndexUye");
        }

        public ActionResult uyeKitapGecmis(string uyeTcNumarasi)
        {
            var ktpgcms=db.TBHAREKET.Where(X=>X.uyeTcNumarasi==uyeTcNumarasi).ToList();
            return View(ktpgcms);
        }
    }
}