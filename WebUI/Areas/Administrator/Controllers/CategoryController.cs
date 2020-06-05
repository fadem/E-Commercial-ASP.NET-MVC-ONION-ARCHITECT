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
    public class CategoryController : Controller
    {
        // GET: Administrator/Category
        CategoryService cs = new CategoryService();
        public ActionResult Index()
        {
            return View(cs.GetAll());
        }
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Category item , HttpPostedFileBase resim)
        {
            if(ModelState.IsValid)
            {
                bool result;
                string fileResult = FxFunction.ImageUpload(resim, ImageFile.Categories, out result);
                if(result)
                {
                    item.ImagePath = fileResult;
                }
                else
                {
                    ViewBag.Message = fileResult;
                }
                bool sonuc = cs.Add(item);
                if(sonuc)
                {
                    return RedirectToAction("Index");
                }

            }
            ViewBag.Message = "Ekleme İşlemi Başarısız";

            return View();
        }
        public ActionResult Update(Guid id)
        {
            return View(cs.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Category item, HttpPostedFileBase resim)
        {
            Category gelen = cs.GetByID(item.ID);
            gelen.CategoryName = item.CategoryName;
            gelen.Description = item.Description;
            if(resim!=null)
            {
                bool result;
                string fileResult = FxFunction.ImageUpload(resim, ImageFile.Categories, out result);
                if(result)
                {
                    gelen.ImagePath = fileResult;
                }
            }
            bool updateResult = cs.Update(gelen);
            if(updateResult)
            {
                ViewBag.Message = "Kategori Güncelleme işlemi Başarılı";

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Kategori Güncelleme işlemi Başarısız";
            }
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            cs.Remove(id);
            return RedirectToAction("Index");
        }
    }
}