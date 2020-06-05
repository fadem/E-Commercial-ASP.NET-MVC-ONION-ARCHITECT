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
    public class AppUserController : Controller
    {
        // GET: Administrator/AppUser
        AppUserService aus = new AppUserService();
        CategoryService cs = new CategoryService();
        SubCategoryService scs = new SubCategoryService();
        ThirdCategoryService tcs = new ThirdCategoryService();
        [AdminAuthentication]
        public ActionResult Index()
        {


            return View(aus.GetAll());
        }
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(AppUser item)
        {
            if (ModelState.IsValid)
            {

                bool sonuc = aus.Add(item);
                if (sonuc)
                {
                    ViewBag.Message = "Kayıt işlemi Başarılı";
                    return RedirectToAction("Login", "Login", new { area = "" });
                }
                else
                {
                    ViewBag.Message = "Kayıt işlemi Başarısız";

                }
            }
            ViewBag.Message = "Girdiğiniz Bilgilerin formatını ve doğruluğunu kontrol ediniz";
            return RedirectToAction("Login", "Login", new { area = "" });

        }
        public ActionResult Update(Guid id)
        {
            return View(aus.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(AppUser item)
        {
            AppUser gelen = aus.GetByID(item.ID);
            gelen.Name = item.Name;
            gelen.SurName = item.SurName;
            gelen.UserName = item.UserName;
            gelen.EmailAddress = item.EmailAddress;
            gelen.Phone = item.Phone;
            gelen.IsAdministrator = item.IsAdministrator;
            gelen.IsPageAdmin = item.IsPageAdmin;
            bool sonuc = aus.Update(gelen);
            if (sonuc)
            {
                ViewBag.Message = "Güncelleme Başarılı";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Güncelleme Başarısız";

            }
            return View();
        }
        [AdminAuthentication]
        public ActionResult Delete(Guid id)
        {
            aus.Remove(id);
            return RedirectToAction("Index");
        }
    }
}