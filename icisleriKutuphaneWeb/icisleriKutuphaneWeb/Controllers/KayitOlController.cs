using System;
using System.Web.Mvc;
using icisleriKutuphaneWeb.Models.Entity;

namespace icisleriKutuphaneWeb.Controllers
{
    public class KayitOlController : Controller
    {
        private readonly icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: KayitOl
        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }

        // POST: KayitOl
        [HttpPost]
        public ActionResult Kayit(TBUYELER p)
        {
            if (!ModelState.IsValid)
            {
                return View("Kayit");
            }

            try
            {
                db.TBUYELER.Add(p);
                db.SaveChanges();
                return RedirectToAction("Kayit"); // Başarılı bir kayıt sonrası yönlendirme
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Veri eklenirken bir hata oluştu: " + ex.Message);
                return View("Kayit");
            }
        }
    }
}
