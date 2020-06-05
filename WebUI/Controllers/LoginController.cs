using Model.Entities;
using Service.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        CategoryService cs = new CategoryService();
        SubCategoryService ss = new SubCategoryService();
        ThirdCategoryService ts = new ThirdCategoryService();
        ProductService ps = new ProductService();
        ImagePathService imgs = new ImagePathService();

        // GET: Login
        AppUserService aus = new AppUserService();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AppUser item)
        {
            ViewData["product"] = ps.GetAll();
            ViewData["category"] = cs.GetAll();
            ViewData["subcategory"] = ss.GetAll();
            ViewData["thirdcategory"] = ts.GetAll();
            ViewBag.ProductImage = imgs.GetAll();
            if (aus.Any(m => m.UserName == item.UserName && m.Password == item.Password))
            {
                AppUser gelen = aus.GetByDefault(m => m.UserName == item.UserName);
                ViewBag.Username = gelen.Name;
                if (gelen.IsPageAdmin == true || gelen.IsAdministrator == true)
                {
                    Session["admin"] = gelen;
                    Session.Timeout = 10;


                    return RedirectToAction("Index", "MyPage");

                }

                Session["oturum"] = gelen;
                Session.Timeout = 10;


                return RedirectToAction("Index", "MyPage");
            }
            else
            {
                ViewBag.Message = "Lütfen girmiş olduğunuz bilgileri kontrol ediniz";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }

}