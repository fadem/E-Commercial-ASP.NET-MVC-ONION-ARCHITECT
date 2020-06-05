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
    public class AddressController : Controller
    {
        // GET: Administrator/Address
        AddressService ads = new AddressService();
        ProvinceService ps = new ProvinceService();
        RegionToService rs = new RegionToService();
        DistrictService ds = new DistrictService();
        RoadService rds = new RoadService();
        StreetService ss = new StreetService();
        BuildService bs = new BuildService();
        AppUserService aus = new AppUserService();
        public ActionResult Index()
        {
            List<Address> aa = ads.GetAll();

            return View(ads.GetAll());
        }
        public ActionResult Insert(Guid? id)
        {

            ViewBag.ProvinceID = new SelectList(ps.GetActive(), "ID", "Name");
            List<SelectListItem> lists = new List<SelectListItem>() {
                new SelectListItem(){Text = "Seçiniz", Value = "0"  }
            };
            ViewData["RegionToID"] = lists;
            ViewData["DistrictID"] = lists;
            ViewData["RoadID"] = lists;
            ViewData["StreetID"] = lists;
            ViewData["BuildID"] = lists;
            AppUser kadiyif = aus.GetByID((Guid)id);
            ViewBag.AppUserID = (Guid)id;
            ViewBag.AppUserName = kadiyif.Name;


            return View();
        }
        [HttpPost]
        public ActionResult Insert(Address item, Guid? id)
        {

            item.AppUserID = (Guid)id;
            bool result = ads.Add(item);
            if (result)
                return RedirectToAction("Index", new { id = item.AppUserID });
            else
                ViewBag.Message = "Kayıt esnasında bir problem oluştu";
            return View();
        }
        public ActionResult Update(Guid id)
        {
            Address item = ads.GetByID(id);
            ViewBag.ProvinceID = new SelectList(ps.GetActive(), "ID", "Name", item.ProvinceID);
            ViewBag.RegionToID = new SelectList(rs.GetActive(), "ID", "Name", item.RegionToID);
            ViewBag.DistrictID = new SelectList(ds.GetActive(), "ID", "Name", item.DistrictID);
            ViewBag.RoadID = new SelectList(rds.GetActive(), "ID", "Name", item.RoadID);
            ViewBag.StreetID = new SelectList(ss.GetActive(), "ID", "Name", item.StreetID);
            ViewBag.BuildID = new SelectList(bs.GetActive(), "ID", "Name", item.BuildID);

            return View(ads.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Address item)
        {
            ViewBag.ProvinceID = new SelectList(ps.GetActive(), "ID", "Name", item.ProvinceID);
            ViewBag.RegionToID = new SelectList(rs.GetActive(), "ID", "Name", item.RegionToID);
            ViewBag.DistrictID = new SelectList(ds.GetActive(), "ID", "Name", item.DistrictID);
            ViewBag.RoadID = new SelectList(rds.GetActive(), "ID", "Name", item.RoadID);
            ViewBag.StreetID = new SelectList(ss.GetActive(), "ID", "Name", item.StreetID);
            ViewBag.BuildID = new SelectList(bs.GetActive(), "ID", "Name", item.BuildID);

            Address gelen = ads.GetByID(item.ID);
            gelen.AppUserID = item.AppUserID;
            gelen.ProvinceID = item.ProvinceID;
            gelen.RegionToID = item.RegionToID;
            gelen.DistrictID = item.DistrictID;
            gelen.RoadID = item.RoadID;
            gelen.StreetID = item.StreetID;
            gelen.BuildID = item.BuildID;
            AppUser kullanici = aus.GetByID(gelen.AppUserID);
           
            bool result = ads.Update(gelen);
            if (result)
            {
                if (kullanici.IsAdministrator == true)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Address", "MyPage", new { id = kullanici.ID });
            }
                
            

                ViewBag.Message = "Güncelleme esnasında bir problem oluştu";
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            ads.Remove(id);
            Address gelen = ads.GetByID(id);
            AppUser kullanici = aus.GetByID(gelen.AppUserID);
            if (kullanici.IsAdministrator == true)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Address", "MyPage", new { id = kullanici.ID ,area="" });
        }
        public JsonResult Categories(Guid? id, string durum)
        {
            if (id != null)
            {
                if (durum == "Reg")
                {
                    var data = new SelectList(rs.GetDefault(m => m.ProvinceID == id), "ID", "Name").ToList();
                    var ilk = new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true };
                    List<SelectListItem> lists = new List<SelectListItem>();
                    lists.Add(ilk);
                    foreach (var item in data)
                    {
                        lists.Add(item);
                    }
                    ViewData["RegionID"] = lists;
                    return Json(ViewData["RegionID"], JsonRequestBehavior.AllowGet);
                }
                else if (durum == "Dis")
                {
                    var data = new SelectList(ds.GetDefault(m => m.RegionToID == id), "ID", "Name").ToList();
                    var ilk = new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true };
                    List<SelectListItem> lists = new List<SelectListItem>();
                    lists.Add(ilk);
                    foreach (var item in data)
                    {
                        lists.Add(item);
                    }
                    ViewData["DistrictID"] = lists;
                    return Json(ViewData["DistrictID"], JsonRequestBehavior.AllowGet);
                }
                else if (durum == "Roa")
                {
                    var data = new SelectList(rds.GetDefault(m => m.DistrictID == id), "ID", "Name").ToList();
                    var ilk = new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true };
                    List<SelectListItem> lists = new List<SelectListItem>();
                    lists.Add(ilk);
                    foreach (var item in data)
                    {
                        lists.Add(item);
                    }
                    ViewData["RoadID"] = lists;
                    return Json(ViewData["RoadID"], JsonRequestBehavior.AllowGet);
                }
                else if (durum == "Str")
                {
                    var data = new SelectList(ss.GetDefault(m => m.RoadID == id), "ID", "Name").ToList();
                    var ilk = new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true };
                    List<SelectListItem> lists = new List<SelectListItem>();
                    lists.Add(ilk);
                    foreach (var item in data)
                    {
                        lists.Add(item);
                    }
                    ViewData["StreetID"] = lists;
                    return Json(ViewData["StreetID"], JsonRequestBehavior.AllowGet);
                }
                else if (durum == "Bui")
                {
                    var data = new SelectList(bs.GetDefault(m => m.StreetID == id), "ID", "Name").ToList();
                    var ilk = new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true };
                    List<SelectListItem> lists = new List<SelectListItem>();
                    lists.Add(ilk);
                    foreach (var item in data)
                    {
                        lists.Add(item);
                    }
                    ViewData["BuildID"] = lists;
                    return Json(ViewData["BuildID"], JsonRequestBehavior.AllowGet);
                }

            }
            var indexfirst = new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true };
            List<SelectListItem> liste = new List<SelectListItem>();
            liste.Add(indexfirst);
            ViewData["RegionID"] = liste;
            ViewData["DistrictID"] = liste;
            ViewData["RoadID"] = liste;
            ViewData["StreetID"] = liste;
            ViewData["BuildID"] = liste;
            return Json(ViewData["RegionID"], JsonRequestBehavior.AllowGet);
        }
    }
}