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
    public class ThirdCategoryController : Controller
    {
        // GET: Administrator/ThirdCategory
        ThirdCategoryService ts = new ThirdCategoryService();
        SubCategoryService ss = new SubCategoryService();
        public ActionResult Index(Guid id)
        {
            return View(ts.GetDefault(m=> m.SubCategoryID==id));
        }
        public ActionResult Insert()
        {
            ViewBag.SubCategoryID = new SelectList(ss.GetActive(), "ID", "SubCategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Insert(ThirdCategory item, HttpPostedFileBase resim)
        {
            ViewBag.SubCategoryID = new SelectList(ss.GetActive(), "ID", "SubCategoryName",item.SubCategoryID);
            if(ModelState.IsValid)
            {
                bool result;
                string fileResult = FxFunction.ImageUpload(resim, ImageFile.ThirdCategories, out result);
                if(result)
                {
                    item.ImagePath = fileResult;
                }
                else
                {
                    ViewBag.Message = fileResult;
                }
                bool sonuc = ts.Add(item);
                if(sonuc)
                {
                    ViewBag.Message = "Ekleme İşlemi Başarılı";
                    return RedirectToAction("Index", new {id=item.SubCategoryID });
                }
                else
                {
                    ViewBag.Message = "Ekleme İşlemi Başarısız";

                }
            }
            ViewBag.Message = "Lütfen Girdiğiniz Bilgilerin Doğru Formatta Olduğundan Emin Olunuz";


            return View();
        }
        public ActionResult Update(Guid id)
        {
            ThirdCategory thirdCategory = ts.GetByID(id);
            ViewBag.SubCategoryID = new SelectList(ss.GetActive(), "ID", "SubCategoryName", thirdCategory.SubCategoryID);

            return View(ts.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(ThirdCategory item, HttpPostedFileBase resim)
        {
            ViewBag.SubCategoryID = new SelectList(ss.GetActive(), "ID", "SubCategoryName", item.SubCategoryID);
            ThirdCategory gelen = ts.GetByID(item.ID);
            gelen.ThirdCategoryName = item.ThirdCategoryName;
            gelen.Description = item.Description;
            gelen.SubCategoryID = item.SubCategoryID;
            if(resim!=null)
            {
                bool result;
                string fileResult = FxFunction.ImageUpload(resim, ImageFile.ThirdCategories, out result);
                if (result)
                {
                    item.ImagePath = fileResult;
                }
                else
                {
                    ViewBag.Message = fileResult;
                }
            }
            bool sonuc = ts.Update(gelen);
            if(sonuc)
            {
                ViewBag.Message = "Güncelleme İşlemi Başarılı";

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Güncelleme İşlemi Başarısız";
            }

            return View();
        }
        public ActionResult Delete(Guid id)
        {
            ts.Remove(id);
            return RedirectToAction("Index");
        }
    }
}