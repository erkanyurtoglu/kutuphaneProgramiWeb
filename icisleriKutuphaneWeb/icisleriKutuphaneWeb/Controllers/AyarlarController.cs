using icisleriKutuphaneWeb.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace icisleriKutuphaneWeb.Controllers
{
    public class AyarlarController : Controller
    {
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: Ayarlar
        public ActionResult IndexAyarlar()
        {
            var kullanicilar = db.TBPERSONEL.ToList();
            return View(kullanicilar);
        }
    }
}