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
    public class DistrictController : Controller
    {
        DistrictService ds = new DistrictService();
        RegionToService rs = new RegionToService();
        public ActionResult Index(Guid? id)
        {
            RegionTo gelen = rs.GetByID((Guid)id);
            ViewBag.RegionToName = gelen.Name;
            ViewBag.RegionToID = gelen.ID;
            return View(ds.GetDefault(m=> m.RegionToID==id));
        }
        public ActionResult Insert(Guid? id)
        {
            RegionTo gelen = rs.GetByID((Guid)id);
            ViewBag.RegionToName = gelen.Name;
            ViewBag.RegionToID = gelen.ID;
            return View();
        }
        [HttpPost]
        public ActionResult Insert(District item,Guid? id)
        {
            item.RegionToID = (Guid)id;
        
            bool result = ds.Add(item);
            if(result)
            {
            return RedirectToAction("Index",new {id=item.RegionToID});

            }
            ViewBag.Message = "Ekleme İşlemi Başarısız";
            return View();
        }
        public ActionResult Update(Guid id)
        {
            District item = ds.GetByID(id);
            ViewBag.RegionID = new SelectList(rs.GetActive(), "ID", "Name", item.RegionToID);

            return View(ds.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(District item)
        {
            ViewBag.RegionID = new SelectList(rs.GetActive(), "ID", "Name", item.RegionToID);
            District gelen = ds.GetByID(item.ID);
            gelen.Name = item.Name;
            gelen.PostalCode = item.PostalCode;

            bool result = ds.Update(gelen);
            if (result)
            {
                return RedirectToAction("Index", new { id = gelen.RegionToID });

            }
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            District gelen = ds.GetByID(id);
            ds.Remove(id);
            return RedirectToAction("Index", new { id = gelen.RegionToID });
        }
    }
}