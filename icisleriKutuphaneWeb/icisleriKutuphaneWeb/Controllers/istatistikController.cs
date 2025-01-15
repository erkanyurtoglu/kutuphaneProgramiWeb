using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using icisleriKutuphaneWeb.Models.Entity;

namespace icisleriKutuphaneWeb.Controllers
{
    public class istatistikController : Controller
    {   
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: istatistik
        public ActionResult Indexistatistik()
        {
            var deger1 = db.TBUYELER.Count();
            var deger2 = db.TBKITAP.Count();
            var deger3 = db.TBKITAP.Where(x => x.durum == false).Count();

            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            return View();
        }

        public ActionResult Galeri()
        {
            return View();
        }
    }
}