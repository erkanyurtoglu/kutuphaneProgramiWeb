using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using icisleriKutuphaneWeb.Models.Entity;

namespace icisleriKutuphaneWeb.Controllers
{
    public class islemController : Controller
    {
        icisleriBakanlıgıWebEntities db = new icisleriBakanlıgıWebEntities();

        // GET: islem
        public ActionResult Indexislem()
        {
            var degerler = db.TBHAREKET.Where(x => x.islemDurum == true).ToList();
            return View(degerler);
        }
    }
}