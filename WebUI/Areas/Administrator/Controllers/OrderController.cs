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
    public class OrderController : Controller
    {
        // GET: Administrator/Order
        OrderService os = new OrderService();
        CustomerNonMemberService cnms = new CustomerNonMemberService();
        CustomerNonMember uyeOlmayan = new CustomerNonMember();
        OrderDetailService ods = new OrderDetailService();
        OrderDetail gelenDetay = new OrderDetail();
        ProductService ps = new ProductService();
        CampaingService cams = new CampaingService();
        ProvinceService prs = new ProvinceService();
        RegionToService rs = new RegionToService();
        DistrictService ds = new DistrictService();
        RoadService rds = new RoadService();
        StreetService ss = new StreetService();
        BuildService bs = new BuildService();
        AppUserService aus = new AppUserService();
        public ActionResult Index(Guid id)
        {
            ViewBag.AppUserID = id;
            return View(os.GetDefault(m => m.AppUserID == id).ToList());
        }
        public ActionResult Insert(Guid? id)
        {
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

            if (id == null)
            {
                return View();
            }
            AppUser kadiyif = aus.GetByID((Guid)id);
            ViewBag.AppUserName = kadiyif.Name;
            ViewBag.AppUserID = id;
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Order item, Guid? appUserID, Guid? productID)
        {
            ViewBag.ProvinceID = new SelectList(prs.GetActive(), "ID", "Name", item.ProvinceID);
            ViewBag.RegionToID = new SelectList(rs.GetActive(), "ID", "Name", item.RegionToID);
            ViewBag.DistricID = new SelectList(ds.GetActive(), "ID", "Name", item.DistrictID);
            ViewBag.RoadID = new SelectList(rds.GetActive(), "ID", "Name", item.RoadID);
            ViewBag.StreetID = new SelectList(ss.GetActive(), "ID", "Name", item.StreetID);
            ViewBag.BuildID = new SelectList(bs.GetActive(), "ID", "Name", item.BuildID);
            if (appUserID == null)
            {
                uyeOlmayan.Name = item.Name;
                uyeOlmayan.SurName = item.SurName;
                uyeOlmayan.Phone = item.Phone;
                uyeOlmayan.EmailAddress = item.EmailAddress;
                cnms.Add(uyeOlmayan);
            }
            AppUser kullanici = aus.GetByID((Guid)appUserID);
            ViewBag.Kullanici = kullanici.IsAdministrator;
            item.AppUserID = (Guid)appUserID;
            item.OrderDate = DateTime.Now;
            item.RequiredDate = DateTime.Now.AddDays(7);
            bool result = os.Add(item);
            if (result)
            {
                gelenDetay.Order = item;
                Product urun = ps.GetByID((Guid)productID);
                gelenDetay.Product = urun;
                if (cams.Any(m => m.ProductID == urun.ID))
                {
                    Campaing kampanya = cams.GetByDefault(x => x.ProductID == urun.ID);
                    urun.UnitPrice = urun.UnitPrice - (urun.UnitPrice * (decimal)kampanya.Discount);
                    ps.Update(urun);
                }
                ods.Add(gelenDetay);

                return View();
            }
            ViewBag.Message = "Sipariş Oluşturulamadı, Bilgileri Kontrol Ediniz";

            return View();
        }
        public ActionResult Update(Guid id)
        {
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
            Order siparis = os.GetByID(id);
            AppUser kadiyif = aus.GetByID((Guid)siparis.AppUserID);
            ViewBag.AppUserName = kadiyif.Name;
            ViewBag.AppUserID = id;

            return View(os.GetByID(id));
        }
        public ActionResult Update(Order item , Guid? appUserID, Guid? productID)
        {
            ViewBag.ProvinceID = new SelectList(prs.GetActive(), "ID", "Name", item.ProvinceID);
            ViewBag.RegionToID = new SelectList(rs.GetActive(), "ID", "Name", item.RegionToID);
            ViewBag.DistricID = new SelectList(ds.GetActive(), "ID", "Name", item.DistrictID);
            ViewBag.RoadID = new SelectList(rds.GetActive(), "ID", "Name", item.RoadID);
            ViewBag.StreetID = new SelectList(ss.GetActive(), "ID", "Name", item.StreetID);
            ViewBag.BuildID = new SelectList(bs.GetActive(), "ID", "Name", item.BuildID);
            AppUser kullanici = aus.GetByID((Guid)appUserID);
            ViewBag.Kullanici = kullanici.IsAdministrator;
            item.AppUserID = (Guid)appUserID;
            item.OrderDate = DateTime.Now;
            item.RequiredDate = DateTime.Now.AddDays(7);
            bool result = os.Add(item);
            if (result)
            {
                gelenDetay.Order = item;
                Product urun = ps.GetByID((Guid)productID);
                gelenDetay.Product = urun;
                if (cams.Any(m => m.ProductID == urun.ID))
                {
                    Campaing kampanya = cams.GetByDefault(x => x.ProductID == urun.ID);
                    urun.UnitPrice = urun.UnitPrice - (urun.UnitPrice * (decimal)kampanya.Discount);
                    ps.Update(urun);
                }
                ods.Update(gelenDetay);

                return View();
            }
            ViewBag.Message = "Sipariş Güncellenmedi, Bilgileri Kontrol Ediniz";
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            Order gelen = os.GetByID(id);
            os.Remove(id);
            return RedirectToAction("Index", new { id = gelen.AppUserID });
        }
    }
}