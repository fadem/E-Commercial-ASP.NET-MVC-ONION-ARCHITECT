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
    public class ShipController : Controller
    {
        ShipService ss = new ShipService();
        public ActionResult Index()
        {
            return View(ss.GetAll());
        }
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Ship item)
        {
            bool result = ss.Add(item);
            if (result)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Ekleme işlemi başarısız";
            return View();
        }
        public ActionResult Update(Guid id)
        {

            return View(ss.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Ship item)
        {
            Ship gelen = ss.GetByID(item.ID);
            gelen.CompanyName = item.CompanyName;
            gelen.Phone = item.Phone;
            bool result = ss.Update(gelen);
            if (result)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Güncelleme işlemi başarısız";

            return View();
        }
        public ActionResult Delete(Guid id)
        {
            ss.Remove(id);
            return RedirectToAction("Index");

        }
    }
}