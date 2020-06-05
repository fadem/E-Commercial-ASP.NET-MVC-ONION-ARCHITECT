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
    public class SizeToController : Controller
    {
        SizeToService ss = new SizeToService();
        public ActionResult Index()
        {
            return View(ss.GetAll());
        }
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(SizeTo item)
        {
            bool result = ss.Add(item);
            if (result)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Ekleme işlemi Başarısız";
            return View();
        }
        public ActionResult Update(Guid id)
        {
            return View(ss.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(SizeTo item)
        {
            SizeTo gelen = ss.GetByID(item.ID);
            gelen.ProductSize = item.ProductSize;
            bool result = ss.Update(gelen);
            if (result)
            {
                return RedirectToAction("Index");

            }
            ViewBag.Message = "Güncelleme işlemi Başarısız";


            return View();
        }
        public ActionResult Delete(Guid id)
        {
            ss.Remove(id);
            return RedirectToAction("Index");
        }
    }
}