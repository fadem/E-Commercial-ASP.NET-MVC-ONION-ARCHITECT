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
    public class BrandController : Controller
    {
        // GET: Administrator/Brand
        BrandService bs = new BrandService();
        public ActionResult Index()
        {
            return View(bs.GetAll());
        }
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Brand item, HttpPostedFileBase resim)
        {

            bool result;
            string fileResult = FxFunction.ImageUpload(resim, ImageFile.Brands, out result);
            if (result)
            {
                item.ImagePath = fileResult;

            }
            else
            {
                ViewBag.Message = fileResult;
            }
            bool sonuc = bs.Add(item);
            if (sonuc)
            {
                ViewBag.Message = "Ekleme İşlemi Başarılı!";
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Message = "Marka ekleme işlemi esnasında bir problem ile karşılaşıldı lütfen sistem yöneticinize başvurun";
            }



            return View();
        }
        public ActionResult Update(Guid id)
        {
            return View(bs.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Brand item, HttpPostedFileBase resim)
        {
            Brand gelen = bs.GetByID(item.ID);
            gelen.Name = item.Name;
            gelen.Description = item.Description;
            if (resim != null)
            {
                bool result;
                string fileResult = FxFunction.ImageUpload(resim, ImageFile.Brands, out result);
                if (result)
                {
                    gelen.ImagePath = fileResult;
                }
            }
            bool updateResult = bs.Update(gelen);
            if(updateResult)
            {
                ViewBag.Message = "Marka Güncelleme Başarılı";

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Marka Güncelleme Başarısız";
            }

            return View();
        }
        public ActionResult Delete(Guid id)
        {
            bs.Remove(id);
            return RedirectToAction("Index");
        }
    }
}