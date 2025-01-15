using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using icisleriKutuphaneWeb.Models.Entity;

namespace icisleriKutuphaneWeb.Controllers
{
    public class vitrinController : Controller
    {
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: vitrin
        [HttpGet]
        public ActionResult IndexVitrin()
        {
            var degerler = db.TBKITAP.ToList();
            return View(degerler);
        }

        [HttpPost]
        public ActionResult IndexVitrin(TBILETISIM t)
        {
            db.TBILETISIM.Add(t);
            db.SaveChanges();
            return RedirectToAction("IndexVitrin");
        }

    }
}