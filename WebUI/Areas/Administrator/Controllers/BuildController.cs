using Model.Entities;
using Service.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Areas.Administrator.Controllers
{
    [AdminAuthentication]
    public class BuildController : Controller
    {
        BuildService bs = new BuildService();
        StreetService ss = new StreetService();
        public ActionResult Index(Guid? id)
        {
            Street gelen = ss.GetByID((Guid)id);
            ViewBag.StreetName = gelen.Name;
            ViewBag.StreetID = gelen.ID;
            return View(bs.GetDefault(m => m.StreetID == id));
        }
        public ActionResult Insert(Guid? id)
        {
            Street gelen = ss.GetByID((Guid)id);
            ViewBag.StreetName = gelen.Name;
            ViewBag.StreetID = gelen.ID;
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Build item, Guid? id)
        {
            item.StreetID = (Guid)id;
            bool result = bs.Add(item);
            if (result)
            {
                return RedirectToAction("Index", new { id = item.StreetID });
            }
            ViewBag.Message = "Ekleme işlemi başarısız";

            return View();
        }
        public ActionResult Update(Guid id)
        {
            Build item = bs.GetByID(id);
            ViewBag.StreetID = new SelectList(ss.GetActive(), "ID", "Name", item.StreetID);
            return View(bs.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Build item)
        {
            ViewBag.StreetID = new SelectList(ss.GetActive(), "ID", "Name", item.StreetID);
            Build gelen = bs.GetByID(item.ID);
            gelen.Name = item.Name;
            gelen.StreetID = item.StreetID;
            bool result = bs.Update(gelen);
            if (result)
            {
                return RedirectToAction("Index", new { id = gelen.StreetID });
            }
            ViewBag.Message = "Güncelleme işlemi başarısız";
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            Build item = bs.GetByID(id);
            bs.Remove(id);
            return RedirectToAction("Index", new { id = item.StreetID });

        }
    }
}