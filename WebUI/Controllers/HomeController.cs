using Model.Entities;
using Service.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
namespace WebUI.Controllers
{

    public class HomeController : Controller
    {

        CommentService coms = new CommentService();
        ProductService ps = new ProductService();
        ImagePathService imgs = new ImagePathService();
        ProductDetailService pds = new ProductDetailService();
        ColourService cos = new ColourService();
        SizeToService sts = new SizeToService();
        CategoryService cs = new CategoryService();
        SubCategoryService ss = new SubCategoryService();
        ThirdCategoryService ts = new ThirdCategoryService();
        AppUserService aus = new AppUserService();
        ProvinceService prs = new ProvinceService();
        AppUser gelen;
        RegionToService rs = new RegionToService();
        DistrictService ds = new DistrictService();
        RoadService rds = new RoadService();
        StreetService strs = new StreetService();
        BuildService bs = new BuildService();
        OrderService os = new OrderService();
        CustomerNonMember uyeOlmayan = new CustomerNonMember();
        CustomerNonMemberService cnms = new CustomerNonMemberService();
        OrderDetailService ods = new OrderDetailService();
        CampaingService cams = new CampaingService();
        ShoppingCartService scs = new ShoppingCartService();
        ShipService shps = new ShipService();
        public ActionResult GetOrder(string detay)
        {

            ViewBag.ShipID = new SelectList(shps.GetActive(), "ID", "CompanyName");
            ViewBag.ProvinceID = new SelectList(prs.GetActive(), "ID", "Name");

            List<SelectListItem> lists = new List<SelectListItem>() {
                new SelectListItem(){Text = "Seçiniz", Value = "0"  }
            };
            ViewData["RegionToID"] = lists;
            ViewData["DistrictID"] = lists;
            ViewData["RoadID"] = lists;
            ViewData["StreetID"] = lists;
            ViewData["BuildID"] = lists;
            ViewBag.Freight = 5m;
            return View();
        }
        float indirim;

        [HttpPost]
        public ActionResult GetOrder(Order item)
        {


            bool result;
            ViewBag.ShipID = new SelectList(shps.GetActive(), "ID", "CompanyName", item.ShipID);

            ViewBag.ProvinceID = new SelectList(prs.GetActive(), "ID", "Name", item.ProvinceID);
            ViewBag.RegionToID = new SelectList(rs.GetActive(), "ID", "Name", item.RegionToID);
            ViewBag.DistrictID = new SelectList(ds.GetActive(), "ID", "Name", item.DistrictID);
            ViewBag.RoadID = new SelectList(rds.GetActive(), "ID", "Name", item.RoadID);
            ViewBag.StreetID = new SelectList(strs.GetActive(), "ID", "Name", item.StreetID);
            ViewBag.BuildID = new SelectList(bs.GetActive(), "ID", "Name", item.BuildID);
            ViewBag.ProvinceID = new SelectList(prs.GetActive(), "ID", "Name");
            List<SelectListItem> lists = new List<SelectListItem>() {
                new SelectListItem(){Text = "Seçiniz", Value = "0"  }
            };
            ViewData["RegionToID"] = lists;
            ViewData["DistrictID"] = lists;
            ViewData["RoadID"] = lists;
            ViewData["StreetID"] = lists;
            ViewData["BuildID"] = lists;
            ViewBag.Freight = 5m;
            List<UrunSepeti> sepet;
            if (Session["oturum"] != null)
            {
                gelen = (AppUser)Session["oturum"];


            }
            else if (Session["admin"] != null)
            {
                gelen = (AppUser)Session["admin"];

            }
            else if (Session["sepet"] != null)
            {



                Campaing kampanya = new Campaing();
                OrderDetail orderDetay = new OrderDetail();
                sepet = (List<UrunSepeti>)Session["sepet"];
                foreach (UrunSepeti urun in sepet)
                {
                    Product good = ps.GetByID(urun.ID);
                    ProductDetail goodDetail = pds.GetByID(urun.ProductDetailID);
                    item.AppUserID=good.AppUserID;
                    uyeOlmayan.Name = item.Name;
                    uyeOlmayan.SurName = item.SurName;
                    uyeOlmayan.Phone = item.Phone;
                    uyeOlmayan.EmailAddress = item.EmailAddress;
                    cnms.Add(uyeOlmayan);
                    item.OrderDate = DateTime.Now;
                    item.RequiredDate = DateTime.Now.AddDays(7);
                    os.Add(item);
                    if (ps.Any(m => m.ID == urun.ID))
                    {
                        if (cams.Any(x => x.ProductID == urun.ID))
                        {
                            kampanya = cams.GetByDefault(x => x.ProductID == urun.ID);
                        }
                        else
                        {
                            kampanya.Discount = 0f;
                        }

                  
                        goodDetail.UnitInStock -= urun.Quantity;
                        pds.Update(goodDetail);
                        orderDetay.ProductID = good.ID;
                        orderDetay.OrderID = item.ID;
                        orderDetay.Quantity = urun.Quantity;
                        orderDetay.Price = (good.UnitPrice - (good.UnitPrice * (decimal)kampanya.Discount)) * orderDetay.Quantity;
                        ods.Add(orderDetay);

                    }
                }
                Session.Remove("sepet");
                TempData["siparis"] = "Siparişiniz alındı";
                return RedirectToAction("Index", "ShoppingCart");


            }
            if (gelen != null)
            {
                List<ShoppingCart> urunler = scs.GetDefault(m => m.AppUserID == gelen.ID && m.Status != Core.Entity.Enum.Status.Deleted);
                Campaing kampanya = new Campaing();
                OrderDetail orderDetay = new OrderDetail();
                foreach (ShoppingCart urun in urunler)
                {

                    Product good = ps.GetByID(urun.ProductID);
                    item.AppUserID = good.AppUserID;
                    item.Name = gelen.Name;
                    item.SurName = gelen.SurName;
                    item.Phone = gelen.Phone;
                    item.EmailAddress = gelen.EmailAddress;
                    item.OrderDate = DateTime.Now;
                    item.RequiredDate = DateTime.Now.AddDays(7);
                    os.Add(item);


                    if (cams.Any(x => x.ProductID == urun.ProductID))
                    {
                        kampanya = cams.GetByDefault(x => x.ProductID == urun.ProductID);
                        indirim = (float)kampanya.Discount;
                    }
                    else
                    {
                        indirim = 0f;

                    }

                    ProductDetail goodDetail = pds.GetByID((Guid)urun.ProductDetailID);
                    goodDetail.UnitInStock -= urun.Quantity;
                    pds.Update(goodDetail);
                    orderDetay.AppUserID = gelen.ID;
                    orderDetay.ProductID = good.ID;
                    orderDetay.OrderID = item.ID;
                    orderDetay.Quantity = urun.Quantity;
                    orderDetay.Price = (good.UnitPrice - (good.UnitPrice * (decimal)indirim)) * orderDetay.Quantity;
                    ods.Add(orderDetay);
                    scs.Remove(urun.ID);

                }






                TempData["siparis"] = "Siparişiniz alındı";

                return RedirectToAction("Index", "ShoppingCart");


            }

            return View();
        }
        public ActionResult InsertComment(Guid id, string memberComment)
        {
            if (Session["oturum"] != null)
            {
                gelen = (AppUser)Session["oturum"];
                ViewBag.AppUserID = gelen.ID;

            }
            else if (Session["admin"] != null)
            {
                gelen = (AppUser)Session["admin"];
                ViewBag.AppUserID = gelen.ID;

            }
            Comment yeni = new Comment();
            yeni.MemberComment = memberComment;
            yeni.ProductID = id;
            yeni.AppUserID = gelen.ID;
            coms.Add(yeni);
            List<Comment> liste = coms.GetDefault(m => m.ProductID == id);
            var data = liste.Select(m => new { name = m.AppUser.Name, Text = m.MemberComment, zaman = m.CreatedDate.ToString() }).OrderByDescending(m => m.zaman).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetComment(Guid id)
        {
            List<Comment> liste = coms.GetDefault(m => m.ProductID == id && m.Status != Core.Entity.Enum.Status.Deleted);
            var data = liste.Select(m => new { name = m.AppUser.Name, Text = m.MemberComment, zaman = m.CreatedDate.ToString() }).OrderByDescending(m => m.zaman).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(Guid? id, string arama, string sirala, string filtre)
        {


            if (Session["oturum"] != null)
            {
                gelen = (AppUser)Session["oturum"];
                ViewBag.AppUserID = gelen.ID;

            }
            else if (Session["admin"] != null)
            {
                gelen = (AppUser)Session["admin"];
                ViewBag.AppUserID = gelen.ID;

            }
            ViewData["category"] = cs.GetDefault(m=> m.Status!= Core.Entity.Enum.Status.Deleted);
            ViewData["subcategory"] = ss.GetDefault(m => m.Status != Core.Entity.Enum.Status.Deleted);
            ViewData["thirdcategory"] = ts.GetDefault(m => m.Status != Core.Entity.Enum.Status.Deleted);
            ViewBag.ProductImage = imgs.GetDefault(m => m.Status != Core.Entity.Enum.Status.Deleted);
            if (sirala == "İsim")
            {
                List<Product> sirList = ps.GetDefault(m => m.Status != Core.Entity.Enum.Status.Deleted);

                return View(sirList.OrderBy(m => m.ProductName).ToList());
            }
            else if (sirala == "Fiyat")
            {
                List<Product> sirList = ps.GetDefault(m => m.Status != Core.Entity.Enum.Status.Deleted);
               
                return View(sirList.OrderBy(m => m.UnitPrice).ToList());

            }
            if (filtre != null)
            {
                List<Product> filtlist = new List<Product>();
                switch (filtre)
                {

                    case "Yeşil":
                        foreach (ProductDetail item in pds.GetDefault(m => m.Colour.ProductColour == "Yeşil" && m.Status != Core.Entity.Enum.Status.Deleted))
                        {
                            Product gelenurun = ps.GetByID(item.ProductID);
                            filtlist.Add(gelenurun);
                        }
                        if (filtlist.Count > 0)
                        {
                            return View(filtlist.ToList());

                        }
                        else
                        {
                            break;
                        }
                    case "Beyaz":
                        foreach (ProductDetail item in pds.GetDefault(m => m.Colour.ProductColour == "Beyaz" && m.Status != Core.Entity.Enum.Status.Deleted))
                        {
                            Product gelenurun = ps.GetByID(item.ProductID);
                            filtlist.Add(gelenurun);
                        }
                        if (filtlist.Count > 0)
                        {
                            return View(filtlist.ToList());

                        }
                        else
                        {
                            break;
                        }
                    case "Gri":
                        foreach (ProductDetail item in pds.GetDefault(m => m.Colour.ProductColour == "Gri" && m.Status != Core.Entity.Enum.Status.Deleted))
                        {
                            Product gelenurun = ps.GetByID(item.ProductID);
                            filtlist.Add(gelenurun);
                        }
                        if (filtlist.Count > 0)
                        {
                            return View(filtlist.ToList());

                        }
                        else
                        {
                            break;
                        }
                    case "Mavi":
                        foreach (ProductDetail item in pds.GetDefault(m => m.Colour.ProductColour == "Mavi" && m.Status != Core.Entity.Enum.Status.Deleted))
                        {
                            Product gelenurun = ps.GetByID(item.ProductID);
                            filtlist.Add(gelenurun);
                        }
                        if (filtlist.Count > 0)
                        {
                            return View(filtlist.ToList());

                        }
                        else
                        {
                            break;
                        }
                    case "Siyah":
                        foreach (ProductDetail item in pds.GetDefault(m => m.Colour.ProductColour == "Siyah" && m.Status != Core.Entity.Enum.Status.Deleted))
                        {
                            Product gelenurun = ps.GetByID(item.ProductID);
                            filtlist.Add(gelenurun);
                        }
                        if (filtlist.Count > 0)
                        {
                            return View(filtlist.ToList());

                        }
                        else
                        {
                            break;
                        }
                    case "Small":
                        foreach (ProductDetail item in pds.GetDefault(m => m.SizeTo.ProductSize == "Small" || m.SizeTo.ProductSize == "S" && m.Status != Core.Entity.Enum.Status.Deleted))
                        {
                            Product gelenurun = ps.GetByID(item.ProductID);
                            filtlist.Add(gelenurun);
                        }
                        if (filtlist.Count > 0)
                        {
                            return View(filtlist.ToList());

                        }
                        else
                        {
                            break;
                        }
                    case "Medium":
                        foreach (ProductDetail item in pds.GetDefault(m => m.SizeTo.ProductSize == "Medium" || m.SizeTo.ProductSize == "M" && m.Status != Core.Entity.Enum.Status.Deleted))
                        {
                            Product gelenurun = ps.GetByID(item.ProductID);
                            filtlist.Add(gelenurun);
                        }
                        if (filtlist.Count > 0)
                        {
                            return View(filtlist.ToList());

                        }
                        else
                        {
                            break;
                        }
                    case "Large":
                        foreach (ProductDetail item in pds.GetDefault(m => m.SizeTo.ProductSize == "Large" || m.SizeTo.ProductSize == "L" && m.Status != Core.Entity.Enum.Status.Deleted))
                        {
                            Product gelenurun = ps.GetByID(item.ProductID);
                            filtlist.Add(gelenurun);
                        }
                        if (filtlist.Count > 0)
                        {
                            return View(filtlist.ToList());

                        }
                        else
                        {
                            break;
                        }
                    case "0-100":

                        foreach (Product item in ps.GetDefault(m => m.UnitPrice > 0 && m.UnitPrice < 100 && m.Status != Core.Entity.Enum.Status.Deleted))
                        {
                            filtlist.Add(item);
                        }
                        if (filtlist.Count > 0)
                        {
                            return View(filtlist.ToList());

                        }
                        else
                        {
                            break;
                        }
                    case "100+":
                        foreach (Product item in ps.GetDefault(m => m.UnitPrice > 100 && m.Status != Core.Entity.Enum.Status.Deleted))
                        {
                            filtlist.Add(item);
                        }
                        if (filtlist.Count > 0)
                        {
                            return View(filtlist.ToList());

                        }
                        else
                        {
                            break;
                        }
                }
            }
            if (arama != null)
            {
                if (ps.GetDefault(m => m.ProductName.Contains(arama)).Count > 0)
                {
                    ViewBag.AramaSonucSayisi = ps.GetDefault(m => m.ProductName.Contains(arama)).Count;
                    return View(ps.GetDefault(m => m.ProductName.Contains(arama) && m.Status != Core.Entity.Enum.Status.Deleted));

                }

                ViewBag.UrunBulunamadi = "Üzgünüz aradığınız ürün sitemizde mevcut değildir";
                return View(ps.GetDefault(m => m.ProductName.Contains(arama) && m.Status != Core.Entity.Enum.Status.Deleted));

            }
            if (id != null)
            {
                if (ps.GetDefault(m => m.CategoryID == id).Count > 0)
                {
                    return View(ps.GetDefault(m => m.CategoryID == id && m.Status != Core.Entity.Enum.Status.Deleted));

                }
                else if (ps.GetDefault(m => m.SubCategoryID == id && m.Status != Core.Entity.Enum.Status.Deleted).Count > 0)
                {
                    return View(ps.GetDefault(m => m.SubCategoryID == id && m.Status != Core.Entity.Enum.Status.Deleted));
                }
                else if (ps.GetDefault(m => m.ThirdCategoryID == id && m.Status != Core.Entity.Enum.Status.Deleted).Count > 0)
                {
                    return View(ps.GetDefault(m => m.ThirdCategoryID == id && m.Status!=Core.Entity.Enum.Status.Deleted));
                }
                else
                {
                    ViewBag.UrunBulunamadi = "Üzgünüz aradığınız kategoride sitemizde ürün mevcut değildir";
                }
            }


            return View(ps.GetDefault(m=> m.Status!=Core.Entity.Enum.Status.Deleted));
        }
        public ActionResult GoodsDetail(Guid id)
        {
            ViewData["image"] = imgs.GetDefault(m => m.ProductID == id);

            List<Colour> renkler = new List<Colour>();
            List<SizeTo> bedenler = new List<SizeTo>();
            foreach (ProductDetail item in pds.GetDefault(m => m.ProductID == id))
            {
                foreach (Colour colorItem in cos.GetDefault(m => m.ID == item.ColourID))
                {
                    renkler.Add(colorItem);
                }
                foreach (SizeTo sizeItem in sts.GetDefault(m => m.ID == item.SizeToID))
                {
                    bedenler.Add(sizeItem);
                }
            }
            ViewBag.ColourID = new SelectList(renkler, "ID", "ProductColour");
            ViewBag.SizeToID = new SelectList(bedenler, "ID", "ProductSize");

            return View(Tuple.Create<Product, List<ProductDetail>, List<Comment>>(ps.GetByID(id), pds.GetDefault(m => m.ProductID == id), coms.GetDefault(m => m.ProductID == id && m.Status != Core.Entity.Enum.Status.Deleted)));
        }

        public ActionResult SendToMail()
        {
            return View();
        }
        MessageService ms = new MessageService();
        [HttpPost]
        public ActionResult SendToMail(Message item)
        {
            if (Session["oturum"] != null)
            {
                gelen = (AppUser)Session["oturum"];
                ViewBag.AppUserID = gelen.ID;

            }
            else if (Session["admin"] != null)
            {
                gelen = (AppUser)Session["admin"];
                ViewBag.AppUserID = gelen.ID;

            }
            if(gelen!=null)
            {
                item.AppUserID = gelen.ID;

            }
            ms.Add(item);
            return View();
        }



    }
}