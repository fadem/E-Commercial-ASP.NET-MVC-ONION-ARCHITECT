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
    public class StreetController : Controller
    {

        StreetService ss = new StreetService();
        RoadService rs = new RoadService();
        public ActionResult Index(Guid? id)
        {
            Road gelen = rs.GetByID((Guid)id);
            ViewBag.RoadName = gelen.Name;
            ViewBag.RoadID = gelen.ID;
            return View(ss.GetDefault(m => m.RoadID == id));

        }
        public ActionResult Insert(Guid? id)
        {
            Road gelen = rs.GetByID((Guid)id);
            ViewBag.RoadName = gelen.Name;
            ViewBag.RoadID = gelen.ID;
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Street item,Guid? id)
        {
            item.RoadID = (Guid)id;
            bool result = ss.Add(item);
            if (result)
            {
                return RedirectToAction("Index", new { id = item.RoadID });
            }
            ViewBag.Message = "Ekleme işlemi başarısız";
            return View();
        }
        public ActionResult Update(Guid id)
        {
            Street item = ss.GetByID(id);
            ViewBag.RoadID = new SelectList(rs.GetActive(), "ID", "Name", item.RoadID);
            return View(ss.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Street item)
        {
            ViewBag.RoadID = new SelectList(rs.GetActive(), "ID", "Name", item.RoadID);
            Street gelen = ss.GetByID(item.ID);
            gelen.Name = item.Name;
            gelen.RoadID = item.RoadID;
            bool result = ss.Update(gelen);
            if (result)
            {
                return RedirectToAction("Index", new { id = gelen.RoadID });
            }
            ViewBag.Message = "Güncelleme işlemi başarısız";
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            Street item = ss.GetByID(id);
            ss.Remove(id);
            return RedirectToAction("Index", new { id = item.RoadID });

        }
    }
}