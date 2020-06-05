using Model.Entities;
using Service.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class MyPageController : Controller
    {
        // GET: MyPage

        WishListService ws = new WishListService();
        ProductService ps = new ProductService();
        AppUserService aus = new AppUserService();
        ImagePathService imgs = new ImagePathService();
        CategoryService cs = new CategoryService();
        SubCategoryService ss = new SubCategoryService();
        ThirdCategoryService ts = new ThirdCategoryService();
        ShoppingCartService scs = new ShoppingCartService();
        AddressService ads = new AddressService();
        OrderService os = new OrderService();
        OrderDetailService ods = new OrderDetailService();
        AppUser user = new AppUser();
        public ActionResult Index(string mesaj)
        {
            if (Session["oturum"] != null)
            {
                user = (AppUser)Session["oturum"];
            }
            else if (Session["admin"] != null)
            {
                user = (AppUser)Session["admin"];

            }
            ViewBag.Sifre = mesaj;
            ViewData["order"] = os.GetDefault(m => m.AppUserID == user.ID);
            ViewData["orderdatail"] = ods.GetDefault(m => m.Order.AppUserID == user.ID);
            return View(Tuple.Create<List<ShoppingCart>, AppUser>(scs.GetDefault(m => m.AppUserID == user.ID), aus.GetByID(user.ID)));
        }
        public ActionResult MyAddress()
        {
            if (Session["oturum"] != null)
            {
                user = (AppUser)Session["oturum"];
            }
            else if (Session["admin"] != null)
            {
                user = (AppUser)Session["admin"];
            }
            ViewBag.Username = user.Name;
            ViewBag.AppUserID = user.ID;

            return View(ads.GetDefault(m => m.AppUserID == user.ID));
        }
        [HttpPost]
        public ActionResult Update(AppUser Item2, Guid id, string ilk, string sifre, string sifreTekrar)
        {
            AppUser gelen = aus.GetByID(id);
            if (ilk == null)
            {
                gelen.Name = Item2.Name;
                gelen.SurName = Item2.SurName;
                gelen.UserName = Item2.UserName;
                gelen.EmailAddress = Item2.EmailAddress;
                gelen.BirthDate = Item2.BirthDate;
            }
            else
            {
                if (gelen.Password == ilk)
                {
                    if (sifre == sifreTekrar)
                    {
                        gelen.Password = sifre;
                    }
                    else
                    {
                        ViewBag.Sifre = "Şifreler Eşleşmemektedir";
                        return RedirectToAction("Index", new { id = gelen.ID, mesaj = ViewBag.Sifre });


                    }

                }
                else
                {
                    ViewBag.Sifre = "Kullanılan şifre yanlış girilmiştir";
                    return RedirectToAction("Index", new { id = gelen.ID, mesaj = ViewBag.Sifre });

                }
            }
            aus.Update(gelen);



            return RedirectToAction("Index", new { id = gelen.ID });
        }
        public ActionResult MyOrder()
        {
            if (Session["oturum"] != null)
            {
                user = (AppUser)Session["oturum"];
            }
            else if (Session["admin"] != null)
            {
                user = (AppUser)Session["admin"];

            }
            ViewBag.Username = user.Name;
            ViewBag.AppUserID = user.ID;

            return View(os.GetDefault(m => m.AppUserID == user.ID));
        }
        public ActionResult WishList()
        {
            if (Session["oturum"] != null)
            {
                user = (AppUser)Session["oturum"];
            }
            else if (Session["admin"] != null)
            {
                user = (AppUser)Session["admin"];

            }
            ViewBag.Username = user.Name;
            ViewBag.AppUserID = user.ID;
            return View(ws.GetDefault(m => m.AppUserID == user.ID && m.Status != Core.Entity.Enum.Status.Deleted));


        }
        public ActionResult AddWish(Guid? id)
        {
            if (Session["oturum"] != null)
            {
                user = (AppUser)Session["oturum"];
            }
            else if (Session["admin"] != null)
            {
                user = (AppUser)Session["admin"];

            }
            ViewBag.Username = user.Name;
            ViewBag.AppUserID = user.ID;
            if (Session["admin"] != null || Session["oturum"] != null)
            {
                WishList gelen = new WishList();
                Product gelenUrun = ps.GetByID((Guid)id);
                bool result = ws.Any(m => m.ProductID == gelenUrun.ID && m.AppUserID == user.ID);
                if (result)
                {
                    gelen = ws.GetByDefault(m => m.ProductID == gelenUrun.ID && m.AppUserID == user.ID);
                    gelenUrun.Point++;
                    ps.Update(gelenUrun);
                    bool guncelle = ws.Update(gelen);
                    return Json("", JsonRequestBehavior.AllowGet);


                }

                gelen.AppUserID = user.ID;
                gelen.ProductID = gelenUrun.ID;
                gelen.ProductName = gelenUrun.ProductName;
                gelen.Price = gelenUrun.UnitPrice;
                foreach (ImagePath resim in imgs.GetAll())
                {
                    if (resim.ProductID == gelenUrun.ID)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            gelen.ImagePath = resim.ProductImage;
                        }
                        break;
                    }
                }
                gelenUrun.Point = 1;
                ps.Update(gelenUrun);

                bool sonuc = ws.Add(gelen);
                if (sonuc)
                {
                    return Json("", JsonRequestBehavior.AllowGet);



                }
            }

            ViewBag.Message = "Ürünleri Favorilerinize Eklemek için Üye Olmalısınız";


            return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);

        }
        MessageService ms = new MessageService();
        public ActionResult Messages()
        {
            return View(ms.GetDefault(m => m.Status != Core.Entity.Enum.Status.Deleted));
        }
        public ActionResult DeleteWish(Guid id)
        {

            ws.Remove(id);
            return RedirectToAction("WishList");
        }
    }
}