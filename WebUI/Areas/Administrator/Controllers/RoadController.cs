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
    public class RoadController : Controller
    {
        RoadService rs = new RoadService();
        DistrictService ds = new DistrictService();
        public ActionResult Index(Guid? id)
        {
            District gelen = ds.GetByID((Guid)id);
            ViewBag.DistrictName = gelen.Name;
            ViewBag.DistrictID = gelen.ID;

            return View(rs.GetDefault(m => m.DistrictID == id).ToList());

        }
        public ActionResult Insert(Guid? id)
        {
            District gelen = ds.GetByID((Guid)id);
            ViewBag.DistrictName = gelen.Name;
            ViewBag.DistrictID = gelen.ID;
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Road item,Guid? id)
        {
            item.DistrictID = (Guid)id;
            bool result = rs.Add(item);
            if (result)
            {
                return RedirectToAction("Index", new { id = item.DistrictID });
            }
            ViewBag.Message = "Ekleme İşlemi Başarısız";
            return View();
        }
        public ActionResult Update(Guid id)
        {
            Road item = rs.GetByID(id);
            ViewBag.DistricID = new SelectList(ds.GetActive(), "ID", "Name", item.DistrictID);
            return View(rs.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Road item)
        {
            ViewBag.DistricID = new SelectList(ds.GetActive(), "ID", "Name", item.DistrictID);
            Road gelen = rs.GetByID(item.ID);
            gelen.Name = item.Name;
            gelen.DistrictID = item.DistrictID;
            bool result = rs.Update(gelen);
            if (result)
            {
                return RedirectToAction("Index", new { id = gelen.DistrictID });
            }
            ViewBag.Message = "Güncelleme İşlemi Başarısız";
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            Road item = rs.GetByID(id);
            rs.Remove(id);
            return RedirectToAction("Index", new { id = item.DistrictID });

        }
    }
}