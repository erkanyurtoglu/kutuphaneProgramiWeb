using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using icisleriKutuphaneWeb.Models.Entity;

namespace icisleriKutuphaneWeb.Controllers
{
    public class PanelimController : Controller
    {
        private icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: Panelim
        [HttpGet]
        [Authorize]
        public ActionResult IndexPanelim()
        {
            var uyeKullaniciAd = User.Identity.Name;
            if (string.IsNullOrEmpty(uyeKullaniciAd))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Kullanıcı adı bulunamadı.");
            }

            var uye = db.TBUYELER.FirstOrDefault(z => z.uyeKullaniciAd == uyeKullaniciAd);
            if (uye == null)
            {
                return HttpNotFound();
            }

            // Kullanıcının e-posta adresini oturuma kaydedin
            Session["uyeEmail"] = uye.uyeEmail;

            var duyurular = db.TBDUYURULAR.ToList();
            ViewBag.Duyurular = duyurular;

            var uyeID = uye.uyeTcNumarasi;
            var okunanKitapSayisi = db.TBHAREKET.Count(x => x.uyeTcNumarasi == uyeID);
            ViewBag.OkunanKitapSayisi = okunanKitapSayisi;

            return View(uye);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndexPanelim(TBUYELER p)
        {
            if (ModelState.IsValid)
            {
                var kullaniciAd = (string)Session["kullaniciAd"];
                var uye = db.TBUYELER.FirstOrDefault(x => x.uyeKullaniciAd == kullaniciAd);

                if (uye != null)
                {
                    // Sadece güncellenebilir alanları ayarlayın
                    uye.uyeAdSoyad = p.uyeAdSoyad;
                    uye.uyeTelefon = p.uyeTelefon;
                    uye.uyeEmail = p.uyeEmail;
                    uye.uyeAdres = p.uyeAdres;
                    uye.calistigiBirim = p.calistigiBirim;
                    uye.unvan = p.unvan;
                    uye.isTelefonu = p.isTelefonu;
                    uye.fotograf = p.fotograf;
                    // Şifreyi yalnızca kullanıcı isterse güncelle
                    if (!string.IsNullOrWhiteSpace(p.uyeSifre))
                    {
                        uye.uyeSifre = p.uyeSifre;
                    }

                    db.SaveChanges();
                }
                else
                {
                    // Kullanıcı bulunamadı, hata mesajı göster
                    ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                }
            }

            return RedirectToAction("IndexPanelim");
        }

        public ActionResult Duyurular()
        {
            var duyuruListesi = db.TBDUYURULAR.ToList();
            return View(duyuruListesi);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }

        public PartialViewResult Partial1()
        {
            var duyurular = db.TBDUYURULAR.ToList();
            return PartialView("Partial1", duyurular);
        }

        public PartialViewResult Partial2()
        {
            var uyeler = db.TBUYELER.ToList();
            return PartialView("Partial2", uyeler);
        }

        public ActionResult Kitaplarim()
        {
            var uyeKullaniciAd = User.Identity.Name;
            if (string.IsNullOrEmpty(uyeKullaniciAd))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Kullanıcı adı bulunamadı.");
            }

            var uye = db.TBUYELER.FirstOrDefault(z => z.uyeKullaniciAd == uyeKullaniciAd);
            if (uye == null)
            {
                return HttpNotFound();
            }

            var kitaplar = db.TBHAREKET
                .Where(x => x.uyeTcNumarasi == uye.uyeTcNumarasi)
                .ToList();

            return View(kitaplar);
        }
    }
}
