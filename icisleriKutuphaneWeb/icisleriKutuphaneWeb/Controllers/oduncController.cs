using System;
using System.Linq;
using System.Web.Mvc;
using icisleriKutuphaneWeb.Models.Entity;

namespace icisleriKutuphaneWeb.Controllers
{
    public class oduncController : Controller
    {
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: odunc
        public ActionResult IndexOdunc()
        {
            var degerler = db.TBHAREKET.Where(x => x.islemDurum == false).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult oduncVer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult oduncVer(TBHAREKET p)
        {
            // Kitap demirbaş numarası ile kitap kaydını bulma
            var kitap = db.TBKITAP.FirstOrDefault(k => k.demirbas == p.kitapDemirbas);
            if (kitap == null)
            {
                // Kitap bulunamazsa hata döndür
                ModelState.AddModelError("", "Geçerli bir kitap seçilmedi.");
                return View();
            }

            // Bulunan kitap demirbaş numarasını hareket tablosuna ekle
            p.kitapDemirbas = kitap.demirbas;
            p.islemDurum = false; // Ödünç verildiğinde false atanmalı

            db.TBHAREKET.Add(p);
            db.SaveChanges();

            return View();
        }

        public ActionResult odunciade(int id)
        {
            var odn = db.TBHAREKET.Find(id);
            if (odn == null)
            {
                return HttpNotFound();
            }
            return View("odunciade", odn);
        }

        [HttpPost]
        public ActionResult oduncGuncelle(TBHAREKET p)
        {
            // TBHAREKET tablosundan ilgili hareket kaydını bul
            var hrk = db.TBHAREKET.Find(p.ID);
            if (hrk == null)
            {
                ModelState.AddModelError("", "Geçerli bir hareket kaydı bulunamadı.");
                return View("odunciade", p);
            }

            // Kitap demirbaş numarasını kullanarak TBKITAP tablosundan ilgili kitabı bul
            var kitap = db.TBKITAP.FirstOrDefault(k => k.demirbas == hrk.kitapDemirbas);
            if (kitap == null)
            {
                ModelState.AddModelError("", "Geçerli bir kitap bulunamadı.");
                return View("odunciade", p);
            }

            // Hareket kaydını güncelle
            hrk.uyeGetirTarih = p.uyeGetirTarih;
            hrk.islemDurum = true; // İade edildiğinde true atanmalı

            // Kitap durumunu güncelle
            kitap.durum = true; // Kitap iade alındığında durum true olarak ayarlanmalı

            db.SaveChanges();

            return RedirectToAction("IndexOdunc");
        }

        public ActionResult uzatIadeTarihi(int id)
        {
            var hrk = db.TBHAREKET.Find(id);
            if (hrk == null)
            {
                return HttpNotFound();
            }

            // İade tarihini 15 gün uzat
            hrk.iadeTarih = hrk.iadeTarih.HasValue ? hrk.iadeTarih.Value.AddDays(15) : DateTime.Now.AddDays(15);

            db.SaveChanges();

            return RedirectToAction("IndexOdunc");
        }
    }
}
