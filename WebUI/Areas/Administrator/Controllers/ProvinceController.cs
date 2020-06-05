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
    public class ProvinceController : Controller
    {
        ProvinceService ps = new ProvinceService();
        public ActionResult Index()
        {
            return View(ps.GetAll());
        }
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Province item)
        {
            bool result = ps.Add(item);
            if (result)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Ekleme işlemi başarız";
            return View();
        }
        public ActionResult Update(Guid id)
        {
            return View(ps.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Province item)
        {
            Province gelen = ps.GetByID(item.ID);
            gelen.Name = item.Name;
            bool result = ps.Update(gelen);
            if (result)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Güncelleme işlemi başarız";
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            ps.Remove(id);
            return RedirectToAction("Index");

        }
    }
}