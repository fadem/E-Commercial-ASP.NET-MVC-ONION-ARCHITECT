using Model.Entities;
using Service.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Areas.Administrator.Controllers
{
    [AdminAuthentication]
    public class RegionToController : Controller
    {
        RegionToService rs = new RegionToService();
        ProvinceService ps = new ProvinceService();
        public ActionResult Index(Guid? id)
        {
            Province gelen = ps.GetByID((Guid)id);
            ViewBag.ProvinceName = gelen.Name;
            ViewBag.ProvinceID = gelen.ID;
            return View(rs.GetDefault(m => m.ProvinceID == id));
            
        }
        public ActionResult Insert(Guid? id)
        {
            Province gelen = ps.GetByID((Guid)id);
            ViewBag.ProvinceID = gelen.ID;
            ViewBag.ProvinceName = gelen.Name;
            return View();
        }
        [HttpPost]
        public ActionResult Insert(RegionTo item, Guid? id)
        {
            item.ProvinceID = (Guid)id;
            bool result = rs.Add(item);
            if(result)
            {
                return RedirectToAction("Index", new { id = item.ProvinceID });
            }
            ViewBag.Message = "Ekleme işlemi başarısız";

            return View();
        }
        public ActionResult Update(Guid id)
        {
            RegionTo item = rs.GetByID(id);
            ViewBag.ProvinceID = new SelectList(ps.GetActive(), "ID", "Name", item.ProvinceID);

            return View(rs.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(RegionTo item)
        {
            ViewBag.ProvinceID = new SelectList(ps.GetActive(), "ID", "Name", item.ProvinceID);
            RegionTo gelen = rs.GetByID(item.ID);
            gelen.Name = item.Name;
            gelen.ProvinceID = item.ProvinceID;
            bool result = rs.Update(gelen);
            if (result)
            {
                return RedirectToAction("Index", new { id = item.ProvinceID });
            }
            ViewBag.Message = "Güncelleme işlemi başarısız";
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            RegionTo item = rs.GetByID(id);
            rs.Remove(id);
            return RedirectToAction("Index", new { id = item.ProvinceID });

        }
    }
}