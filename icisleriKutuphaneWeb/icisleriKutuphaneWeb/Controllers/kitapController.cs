using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using icisleriKutuphaneWeb.Models.Entity;

namespace icisleriKutuphaneWeb.Controllers
{
    public class kitapController : Controller
    {
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: kitap
        public ActionResult IndexKitap(string search, int page = 1, string sortColumn = "eser", string sortOrder = "asc")
        {
            int pageSize = 7; // Her sayfada gösterilecek kitap sayısı
            var kitaplar = db.TBKITAP.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                kitaplar = kitaplar.Where(k => k.eser.Contains(search) ||
                                                k.yazar.Contains(search) ||
                                                k.demirbas.Contains(search) ||
                                                k.sorumlular.Contains(search) ||
                                                k.isbn.Contains(search) ||
                                                k.yayinlayan.Contains(search));
            }

            // Sıralama işlevselliği
            switch (sortColumn)
            {
                case "eser":
                    kitaplar = sortOrder == "asc" ? kitaplar.OrderBy(k => k.eser) : kitaplar.OrderByDescending(k => k.eser);
                    break;
                case "yazar":
                    kitaplar = sortOrder == "asc" ? kitaplar.OrderBy(k => k.yazar) : kitaplar.OrderByDescending(k => k.yazar);
                    break;
                case "demirbas":
                    kitaplar = sortOrder == "asc" ? kitaplar.OrderBy(k => k.demirbas) : kitaplar.OrderByDescending(k => k.demirbas);
                    break;
                case "isbn":
                    kitaplar = sortOrder == "asc" ? kitaplar.OrderBy(k => k.isbn) : kitaplar.OrderByDescending(k => k.isbn);
                    break;
                case "yayinYeri":
                    kitaplar = sortOrder == "asc" ? kitaplar.OrderBy(k => k.yayinYeri) : kitaplar.OrderByDescending(k => k.yayinYeri);
                    break;
                case "yayinlayan":
                    kitaplar = sortOrder == "asc" ? kitaplar.OrderBy(k => k.yayinlayan) : kitaplar.OrderByDescending(k => k.yayinlayan);
                    break;
                case "durum":
                    kitaplar = sortOrder == "asc" ? kitaplar.OrderBy(k => k.durum) : kitaplar.OrderByDescending(k => k.durum);
                    break;
            }

            // Toplam kitap sayısını al
            int totalBooks = kitaplar.Count();

            // Kitapları sayfalama için ayarla
            kitaplar = kitaplar
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize);

            // Sayfa numarası ve toplam sayfa sayısını ViewBag ile gönder
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalBooks / pageSize);

            // Arama terimini ve sıralama bilgilerini ViewBag ile göndermek
            ViewBag.SearchTerm = search;
            ViewBag.SortColumn = sortColumn;
            ViewBag.SortOrder = sortOrder;

            return View(kitaplar.ToList());
        }

        [HttpGet]
        public ActionResult kitapEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult kitapEkle(TBKITAP p)
        {
            if (ModelState.IsValid)
            {
                db.TBKITAP.Add(p);
                db.SaveChanges();
                return RedirectToAction("IndexKitap");
            }

            return View(p);
        }

        public ActionResult kitapSil(string demirbas)
        {
            if (string.IsNullOrEmpty(demirbas))
            {
                return RedirectToAction("IndexKitap");
            }

            var kitap = db.TBKITAP.FirstOrDefault(k => k.demirbas == demirbas);

            if (kitap == null)
            {
                return HttpNotFound();
            }

            db.TBKITAP.Remove(kitap);
            db.SaveChanges();

            return RedirectToAction("IndexKitap");
        }

        [HttpGet]
        public ActionResult kitapGetir(string demirbas)
        {
            var kitap = db.TBKITAP.Find(demirbas);
            if (kitap == null)
            {
                return HttpNotFound();
            }
            return View("kitapGetir", kitap);
        }

        [HttpPost]
        public ActionResult kitapGuncelle(TBKITAP p)
        {
            if (ModelState.IsValid)
            {
                var kitap = db.TBKITAP.FirstOrDefault(k => k.demirbas == p.demirbas);

                if (kitap != null)
                {
                    // Kitap bilgilerini güncelledim
                    kitap.eser = p.eser;
                    kitap.yazar = p.yazar;
                    kitap.sorumlular = p.sorumlular;
                    kitap.isbn = p.isbn;
                    kitap.yayinYeri = p.yayinYeri;
                    kitap.yayinlayan = p.yayinlayan;
                    kitap.matbaa = p.matbaa;
                    kitap.fizikselNiteleme = p.fizikselNiteleme;
                    kitap.altTur = p.altTur;
                    kitap.yerNumarasi = p.yerNumarasi;
                    kitap.kayitTarih = p.kayitTarih;
                    kitap.yayinTarih = p.yayinTarih;
                    kitap.kopya = p.kopya;
                    kitap.cilt = p.cilt;
                    kitap.dilBir = p.dilBir;
                    kitap.diliki = p.diliki;
                    kitap.temelGiris = p.temelGiris;
                    kitap.aramaGrubu = p.aramaGrubu;
                    kitap.saglama = p.saglama;
                    kitap.oda = p.oda;
                    kitap.kitapResim = p.kitapResim;
                    kitap.konuBasliklari = p.konuBasliklari;
                    kitap.degisiklikYapan = p.degisiklikYapan;
                    kitap.notlar = p.notlar;
                    kitap.durum = true;

                    db.SaveChanges();
                }

                return RedirectToAction("IndexKitap");
            }

            return View("kitapGetir", p);
        }
    }
}
