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
    public class ImagePathController : Controller
    {
        // GET: Administrator/ImagePath
        ImagePathService ips = new ImagePathService();
        ProductService ps = new ProductService();
        public ActionResult Index(Guid id)
        {
            Product gelen = ps.GetByID(id);
            ViewBag.ProductName = gelen.ProductName;
            ViewBag.ProductID = id;
            TempData["product"] = id;
            return View(ips.GetDefault(m => m.ProductID == id).ToList());
        }
        public ActionResult Insert(Guid id)
        {
            Product gelen = ps.GetByID(id);
            ViewBag.ProductID = id;
            ImagePath gelenresim = ips.GetByID(id);
            if (gelen != null && gelenresim == null)
            {
                ViewBag.ProductName = gelen.ProductName;
            }
            else
            {
                ViewBag.ProductName = gelenresim.Product.ProductName;

            }
            return View();
        }
        [HttpPost]
        public ActionResult Insert(ImagePath item, Guid id, HttpPostedFileBase resim)
        {
            bool result;
            string fileResult = FxFunction.ImageUpload(resim, ImageFile.ImagePath, out result);
            if (result)
            {
                item.ProductImage = fileResult;
            }
            else
            {
                ViewBag.Message = fileResult;
            }
            item.ProductID = id;
            bool sonuc = ips.Add(item);
            if (sonuc)
            {
                return RedirectToAction("Index", new { id = item.ProductID });
            }
            ViewBag.Message = "Resim ekleme işlemi başarısız ";
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            ips.Remove(id);
            Guid giden = (Guid)TempData["product"];
            return RedirectToAction("Index", new { id = giden });
        }
        public ActionResult Update(Guid id)
        {
            return View(ips.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(ImagePath item, HttpPostedFileBase resim)
        {
            ImagePath gelen = ips.GetByID(item.ID);
          
            if (resim != null)
            {
                bool result;
                string fileResult = FxFunction.ImageUpload(resim, ImageFile.ImagePath, out result);
                if (result)
                {
                    gelen.ProductImage = fileResult;
                }
                else
                {
                    ViewBag.Message = fileResult;
                }
            }
            bool sonuc = ips.Update(gelen);
            if (sonuc)
            {
                return RedirectToAction("Index", new { id = gelen.ProductID });
            }
            ViewBag.Message = "Güncelleme İşlemi Başarısız";
            return View();
        }
    }
}