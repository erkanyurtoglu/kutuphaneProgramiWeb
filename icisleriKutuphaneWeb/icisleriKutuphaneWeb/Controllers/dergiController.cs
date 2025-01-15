using icisleriKutuphaneWeb.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace icisleriKutuphaneWeb.Controllers
{
    public class dergiController : Controller
    {
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: dergi
        public ActionResult IndexDergi(string search, int page = 1, string sortColumn = "eser", string sortOrder = "asc")
        {
            int pageSize = 7; // Her sayfada gösterilecek kitap sayısı
            var dergi = db.TBDERGI.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                dergi = dergi.Where(d => d.eser.Contains(search) ||
                                                d.Sorumlular.Contains(search) ||
                                                d.demirbas.Contains(search) ||
                                                d.issn.Contains(search) ||
                                                d.yayinlayan.Contains(search));
            }

            // Sıralama işlevselliği
            switch (sortColumn)
            {
                case "eser":
                    dergi = sortOrder == "asc" ? dergi.OrderBy(d => d.eser) : dergi.OrderByDescending(d => d.eser);
                    break;
                case "Sorumlular":
                    dergi = sortOrder == "asc" ? dergi.OrderBy(d => d.Sorumlular) : dergi.OrderByDescending(d => d.Sorumlular);
                    break;
                case "demirbas":
                    dergi = sortOrder == "asc" ? dergi.OrderBy(d => d.demirbas) : dergi.OrderByDescending(d => d.demirbas);
                    break;
                case "issn":
                    dergi = sortOrder == "asc" ? dergi.OrderBy(d => d.issn) : dergi.OrderByDescending(d => d.issn);
                    break;
                case "yayinlayan":
                    dergi = sortOrder == "asc" ? dergi.OrderBy(d => d.yayinlayan) : dergi.OrderByDescending(d => d.yayinlayan);
                    break;

            }

            // Toplam kitap sayısını al
            int totalBooks = dergi.Count();

            // Kitapları sayfalama için ayarla
            dergi = dergi
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize);

            // Sayfa numarası ve toplam sayfa sayısını ViewBag ile gönder
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalBooks / pageSize);

            // Arama terimini ve sıralama bilgilerini ViewBag ile göndermek
            ViewBag.SearchTerm = search;
            ViewBag.SortColumn = sortColumn;
            ViewBag.SortOrder = sortOrder;

            return View(dergi.ToList());
        }

        [HttpGet]
        public ActionResult dergiEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult dergiEkle(TBDERGI p)
        {
            if (ModelState.IsValid)
            {
                db.TBDERGI.Add(p);
                db.SaveChanges();
                return RedirectToAction("IndexDergi");
            }

            return View(p);
        }

        public ActionResult dergiSil(string demirbas)
        {
            if (string.IsNullOrEmpty(demirbas))
            {
                return RedirectToAction("IndexDergi");
            }

            var dergi = db.TBDERGI.FirstOrDefault(d => d.demirbas == demirbas);

            if (dergi == null)
            {
                return HttpNotFound();
            }

            db.TBDERGI.Remove(dergi);
            db.SaveChanges();

            return RedirectToAction("IndexDergi");
        }

        [HttpGet]
        public ActionResult dergiGetir(string demirbas)
        {
            var dergi = db.TBDERGI.Find(demirbas);
            if (dergi == null)
            {
                return HttpNotFound();
            }
            return View("dergiGetir", dergi);
        }

        [HttpPost]
        public ActionResult dergiGuncelle(TBDERGI p)
        {
            if (ModelState.IsValid)
            {
                var dergi = db.TBDERGI.FirstOrDefault(d => d.demirbas == p.demirbas);

                if (dergi != null)
                {
                    // Kitap bilgilerini güncelliyorum
                    dergi.eser = p.eser;
                    dergi.Sorumlular = p.Sorumlular;
                    dergi.issn = p.issn;  
                    dergi.yayinYeri = p.yayinYeri;
                    dergi.yayinlayan = p.yayinlayan;
                    dergi.matbaa = p.matbaa;
                    dergi.fizikselNiteleme = p.fizikselNiteleme;
                    dergi.altTur = p.altTur;
                    dergi.kayitTarih = p.kayitTarih;
                    dergi.yayinTarih = p.yayinTarih;
                    dergi.kopya = p.kopya;
                    dergi.dilBir = p.dilBir;
                    dergi.diliki = p.diliki;
                    dergi.temelGiris = p.temelGiris;
                    dergi.aramaGrubu = p.aramaGrubu;
                    dergi.oda = p.oda;
                    dergi.durum = p.durum;
                    dergi.sekil = p.sekil;
                    dergi.baslik = p.baslik;
                    dergi.sureliAyrinti = p.sureliAyrinti;
                    dergi.URLadres = p.URLadres;
                    dergi.MarcEkGiris = p.MarcEkGiris;

                    db.SaveChanges();
                }

                return RedirectToAction("IndexDergi");
            }

            return View("dergiGetir", p);
        }


    }
}