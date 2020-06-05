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
    public class SubCategoryController : Controller
    {
        // GET: Administrator/SubCategory
        SubCategoryService ss = new SubCategoryService();
        CategoryService cs = new CategoryService();
        public ActionResult Index(Guid id)
        {
            return View(ss.GetDefault(m => m.CategoryID == id));
        }
        public ActionResult Insert()
        {
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Insert(SubCategory item, HttpPostedFileBase resim)
        {
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", item.CategoryID);
            if (ModelState.IsValid)
            {
                bool result;
                string fileResult = FxFunction.ImageUpload(resim, ImageFile.SubCategories, out result);
                if (result)
                {
                    item.ImagePath = fileResult;
                }
                else
                {
                    ViewBag.Message = fileResult;
                }
                bool sonuc = ss.Add(item);

                if (sonuc)
                {
                    ViewBag.Message = "Ekleme İşlemi Başarılı";
                    return RedirectToAction("Index", new { id = item.CategoryID });
                }
                else
                {
                    ViewBag.Message = "Ekleme İşlemi Başarısız";

                }
            }
            return View();
        }
        public ActionResult Update(Guid id)
        {
            SubCategory subcategory = ss.GetByID(id);
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", subcategory.CategoryID);
            return View(ss.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(SubCategory item, HttpPostedFileBase resim)
        {
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", item.CategoryID);
            SubCategory gelen = ss.GetByID(item.ID);
            gelen.SubCategoryName = item.SubCategoryName;
            gelen.CategoryID = item.CategoryID;
            gelen.Description = item.Description;
            if (resim != null)
            {
                bool result;
                string fileResult = FxFunction.ImageUpload(resim, ImageFile.SubCategories, out result);
                if (result)
                {
                    gelen.ImagePath = fileResult;
                }
                else
                {
                    ViewBag.Message = fileResult;
                }

            }
            bool sonuc = ss.Update(gelen);
            if (sonuc)
            {
                ViewBag.Message = "Güncelleme Başarılı";
                return RedirectToAction("Index", new { id = item.CategoryID });
            }
            else
            {
                ViewBag.Message = "Güncelleme Başarısız";

            }
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            SubCategory gelen = ss.GetByID(id);
            ss.Remove(id);
            return RedirectToAction("Index", new { id = gelen.CategoryID });
        }
    }
}