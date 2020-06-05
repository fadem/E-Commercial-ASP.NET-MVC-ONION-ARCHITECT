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
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        List<UrunSepeti> sepetim;
        ShoppingCartService scs = new ShoppingCartService();
        ProductService ps = new ProductService();
        AppUserService aus = new AppUserService();
        CategoryService cs = new CategoryService();
        SubCategoryService ss = new SubCategoryService();
        ThirdCategoryService ts = new ThirdCategoryService();
        ImagePathService imgs = new ImagePathService();
        ProductDetailService pds = new ProductDetailService();

        public ActionResult Index()
        {
            ViewData["product"] = ps.GetAll();
            ViewData["category"] = cs.GetAll();
            ViewData["subcategory"] = ss.GetAll();
            ViewData["subcategoryE"] = ss.GetAll();
            ViewData["thirdcategory"] = ts.GetAll();
            ViewData["ProductImage"] = imgs.GetAll();

            if (Session["oturum"] != null)
            {
                AppUser gelen = (AppUser)Session["oturum"];
                ViewBag.AppUserID = gelen.ID;
                TempData["appuserID"] = gelen.ID;
                return View(Tuple.Create<List<UrunSepeti>, List<ShoppingCart>>((List<UrunSepeti>)Session["sepet"], scs.GetDefault(m => m.AppUserID == gelen.ID && m.Status != Core.Entity.Enum.Status.Deleted)));

            }
            else if (Session["admin"] != null)
            {
                AppUser gelen = (AppUser)Session["admin"];
                ViewBag.AppUserID = gelen.ID;
                TempData["appuserID"] = gelen.ID;

                return View(Tuple.Create<List<UrunSepeti>, List<ShoppingCart>>((List<UrunSepeti>)Session["sepet"], scs.GetDefault(m => m.AppUserID == gelen.ID && m.Status != Core.Entity.Enum.Status.Deleted)));

            }
            return View(Tuple.Create<List<UrunSepeti>, List<ShoppingCart>>((List<UrunSepeti>)Session["sepet"], scs.GetDefault(m => m.Status == Core.Entity.Enum.Status.None)));
        }
        bool result;
        AppUser kullanici;
        Product gelen;
        public JsonResult Insert(Guid id, Guid? productds, string durum)
        {
            if (productds != null)
            {
                durum = "detay";

            }
            gelen = ps.GetByID(id);
            if (Session["oturum"] != null)
            {
                kullanici = (AppUser)Session["oturum"];
            }
            else if (Session["admin"] != null)
            {
                kullanici = (AppUser)Session["admin"];

            }
            if (kullanici != null)
            {
                if (scs.Any(m => m.AppUserID == kullanici.ID && m.ProductID == id && m.Status != Core.Entity.Enum.Status.Deleted))
                {
                    ShoppingCart guncel = scs.GetByDefault(m => m.AppUserID == kullanici.ID && m.ProductID == id);
                    if (durum == "detay")
                    {
                        guncel.ProductDetailID = (Guid)productds;
                        result = scs.Update(guncel);

                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else if (durum == "eksi")
                    {
                        if (guncel.Quantity <= 1)
                        {
                            foreach (ShoppingCart item in scs.GetDefault(m => m.AppUserID == kullanici.ID))
                            {
                                if (item.ProductID == id)
                                {
                                    scs.Remove(item.ID);
                                    return Json(result, JsonRequestBehavior.AllowGet);

                                }
                            }

                        }
                        guncel.Quantity--;
                        result = scs.Update(guncel);

                        return Json(result, JsonRequestBehavior.AllowGet);


                    }
                    else
                    {

                        guncel.Quantity++;
                        result = scs.Update(guncel);
                        return Json(result, JsonRequestBehavior.AllowGet);


                    }


                }
                else if (scs.Any(m => m.AppUserID == kullanici.ID && m.ProductID == id && m.Status == Core.Entity.Enum.Status.Deleted))
                {
                    ShoppingCart guncel = scs.GetByDefault(m => m.AppUserID == kullanici.ID && m.ProductID == id);
                    guncel.Quantity = 1;
                    result = scs.Update(guncel);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ShoppingCart yeni = new ShoppingCart();
                    if (pds != null)
                    {
                        yeni.ProductDetailID = productds;

                    }
                    yeni.AppUserID = kullanici.ID;
                    yeni.ProductID = gelen.ID;
                    yeni.Name = gelen.ProductName;
                    yeni.Price = gelen.UnitPrice;
                    yeni.Quantity = 1;
                    foreach (ImagePath item in imgs.GetAll())
                    {
                        if (item.ProductID == gelen.ID)
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                yeni.PicImage = item.ProductImage;
                            }

                        }
                    }
                    result = scs.Add(yeni);
                    return Json(result, JsonRequestBehavior.AllowGet);


                }



            }
            if (Session["sepet"] == null)
            {
                sepetim = new List<UrunSepeti>();
            }
            else
            {
                sepetim = (List<UrunSepeti>)Session["sepet"];
            }
            if (sepetim.Count < 1 || sepetim.FirstOrDefault(m => m.ID == gelen.ID) == null)
            {

                UrunSepeti urun = new UrunSepeti();
                if (productds != null)
                {
                    urun.ProductDetailID = (Guid)productds;
                    ProductDetail pdsurun = pds.GetByID(urun.ProductDetailID);
                    urun.ProductColor = pdsurun.Colour.ProductColour;
                    urun.ProductSize = pdsurun.SizeTo.ProductSize;

                }

                urun.ID = gelen.ID;
                urun.ProductName = gelen.ProductName;
                urun.ProductPrice = gelen.UnitPrice;
                foreach (ImagePath item in imgs.GetAll())
                {
                    if (item.ProductID == gelen.ID)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            urun.PicImage = item.ProductImage;
                        }

                    }
                }
                //urun.ProductColor = gelendetay.Colour.ProductColour;
                //urun.ProductSize = gelendetay.SizeTo.ProductSize;

                urun.Quantity = 1;
                sepetim.Add(urun);
                result = true;

            }
            else
            {
                UrunSepeti guncelle = sepetim.FirstOrDefault(m => m.ID == gelen.ID);
                if (durum == "detay")
                {
                    guncelle.ProductDetailID = (Guid)productds;
                    ProductDetail pdsurun = pds.GetByID(guncelle.ProductDetailID);
                    guncelle.ProductColor = pdsurun.Colour.ProductColour;
                    guncelle.ProductSize = pdsurun.SizeTo.ProductSize;

                }
                else if (durum == "eksi")
                {
                    if (guncelle.Quantity <= 1)
                    {
                        sepetim = (List<UrunSepeti>)Session["sepet"];
                        UrunSepeti sil = sepetim.FirstOrDefault(m => m.ID == id);
                        sepetim.Remove(sil);
                    }
                    else
                    {
                        guncelle.Quantity--;

                    }

                }
                else
                {
                    guncelle.Quantity++;

                }

                result = true;

            }
            Session["sepet"] = sepetim;
            Session.Timeout = 80;



            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Delete(Guid? id)
        {
            if (Session["oturum"] != null)
            {

                kullanici = (AppUser)Session["oturum"];
            }
            else if (Session["admin"] != null)
            {

                kullanici = (AppUser)Session["admin"];

            }
            if (id != null)
            {

                if (kullanici != null)
                {


                    foreach (ShoppingCart item in scs.GetDefault(m => m.AppUserID == kullanici.ID))
                    {
                        if (item.ID == id)
                        {
                            item.ProductDetailID = null;
                            scs.Update(item);
                            scs.Remove(item.ID);
                        }
                    }

                    return RedirectToAction("Index");

                }
                sepetim = (List<UrunSepeti>)Session["sepet"];
                UrunSepeti sil = sepetim.FirstOrDefault(m => m.ID == id);
                sepetim.Remove(sil);
                if (sepetim.Count < 1)
                {
                    Session.Remove("sepet");
                }

                return RedirectToAction("Index");
            }
            else
            {
                if (kullanici != null)
                {


                    foreach (ShoppingCart item in scs.GetDefault(m => m.AppUserID == kullanici.ID))
                    {
                        item.ProductDetailID = null;
                        scs.Update(item);
                        scs.Remove(item.ID);

                    }

                    return RedirectToAction("Index");

                }
                else
                {
                    Session.Abandon();
                    return RedirectToAction("Index");

                }

            }






        }
    }
}