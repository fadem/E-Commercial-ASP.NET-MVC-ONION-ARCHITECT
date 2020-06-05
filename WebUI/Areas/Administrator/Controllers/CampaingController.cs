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
    public class CampaingController : Controller
    {
        // GET: Administrator/Campaing
        CampaingService cs = new CampaingService();
        ProductService ps = new ProductService();
        public ActionResult Index(Guid id)
        {
            Product gelen = ps.GetByID(id);
            ViewBag.ProductName = gelen.ProductName;
            ViewBag.ProductID = gelen.ID;
            TempData["appuser"] = gelen.AppUserID;

            return View(cs.GetDefault(m => m.ProductID == id));
        }
        public ActionResult Insert(Guid id)
        {
            Product gelen = ps.GetByID(id);
            ViewBag.ProductName = gelen.ProductName;
            ViewBag.ProductID = gelen.ID;
            ViewBag.AppUserID = gelen.AppUserID;

            return View();
        }
        [HttpPost]
        public ActionResult Insert(Campaing item, Guid id)
        {
            item.ProductID = id;
            bool result = cs.Add(item);
            if (result)
            {
                return RedirectToAction("Index", new { id = item.ProductID });
            }
            ViewBag.Message = "Ekleme işlemi başarısız";
            return View();
        }
        public ActionResult Update(Guid id)
        {
            Campaing gelen = cs.GetByID(id);
            ViewBag.ProductName = gelen.Product.ProductName;
            return View(cs.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Campaing item)
        {
            Campaing gelen = cs.GetByID(item.ID);
            gelen.Discount = item.Discount;
            gelen.Description = item.Description;
            bool result = cs.Update(gelen);
            if (result)
            {
                return RedirectToAction("Index", new { id = gelen.ProductID });
            }
            ViewBag.Message = "Güncelleme İşlemi Başarısız";
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            Campaing gelen = cs.GetByID(id);

            cs.Remove(id);
            return RedirectToAction("Index", new { id = gelen.ProductID });
        }
    }
}