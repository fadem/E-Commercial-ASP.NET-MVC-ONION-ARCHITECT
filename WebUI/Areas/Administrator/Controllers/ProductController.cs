using Model.Entities;
using Service.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Areas.Administrator.ViewModels;
using WebUI.Models;

namespace WebUI.Areas.Administrator.Controllers
{
    [AdminAuthentication]
    public class ProductController : Controller
    {
        // GET: Administrator/Product
        ProductService ps = new ProductService();
        ProductDetailService pds = new ProductDetailService();

        CategoryService cats = new CategoryService();
        SubCategoryService scats = new SubCategoryService();
        ThirdCategoryService tcats = new ThirdCategoryService();
        BrandService brs = new BrandService();
        AppUserService aus = new AppUserService();
        Category category = new Category();
        SubCategory subCategory = new SubCategory();
        ThirdCategory thirdCategory = new ThirdCategory();
        public ActionResult Index(Guid id)
        {
            ViewBag.AppUserID = id;
            return View(ps.GetDefault(m => m.AppUserID == id).ToList());

        }
        public ActionResult Insert()
        {
            ViewBag.CategoryID = new SelectList(cats.GetActive(), "ID", "CategoryName", selectedValue: "0");
            List<SelectListItem> lists = new List<SelectListItem>() {
                new SelectListItem(){Text = "Kategori Seçiniz", Value = "0"  }
            };

            ViewData["SubCategoryID"] = lists;
            ViewData["ThirdCategoryID"] = lists;
            ViewBag.BrandID = new SelectList(brs.GetActive(), "ID", "Name");


            return View();
        }
        [HttpPost]
        public ActionResult Insert(Product item, Guid id)
        {
            ViewBag.CategoryID = new SelectList(cats.GetActive(), "ID", "CategoryName", item.CategoryID);
            ViewBag.SubCategoryID = new SelectList(scats.GetDefault(m => m.CategoryID == item.CategoryID), "ID", "SubCategoryName", item.SubCategoryID);
            ViewBag.ThirdCategoryID = new SelectList(tcats.GetDefault(m => m.SubCategoryID == item.SubCategoryID), "ID", "ThirdCategoryName", item.ThirdCategoryID);
            ViewBag.BrandID = new SelectList(brs.GetActive(), "ID", "Name", item.BrandID);
            item.AppUserID = id;
            ViewData["ProductID"] = item.ID;
            bool result = ps.Add(item);
            if (result)
            {
                return RedirectToAction("Insert", "ProductDetail", new { id = item.ID });
            }
            ViewBag.Message = "Ekleme İşlemi Başarısız";
            return View();
        }
        public ActionResult Update(Guid id)
        {
            Product item = ps.GetByID(id);
            ViewBag.CategoryID = new SelectList(cats.GetActive(), "ID", "CategoryName", item.CategoryID);
            ViewBag.SubCategoryID = new SelectList(scats.GetDefault(m => m.CategoryID == item.CategoryID), "ID", "SubCategoryName", item.SubCategoryID);
            ViewBag.ThirdCategoryID = new SelectList(tcats.GetDefault(m => m.SubCategoryID == item.SubCategoryID), "ID", "ThirdCategoryName", item.ThirdCategoryID);
            ViewBag.BrandID = new SelectList(brs.GetActive(), "ID", "Name", item.BrandID);

            return View(ps.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Product item)
        {
            Product gelen = ps.GetByID(item.ID);
            ViewBag.CategoryID = new SelectList(cats.GetActive(), "ID", "CategoryName", gelen.CategoryID);
            ViewBag.SubCategoryID = new SelectList(scats.GetDefault(m => m.CategoryID == gelen.CategoryID), "ID", "SubCategoryName", gelen.SubCategoryID);
            ViewBag.ThirdCategoryID = new SelectList(tcats.GetDefault(m => m.SubCategoryID == gelen.SubCategoryID), "ID", "ThirdCategoryName", gelen.ThirdCategoryID);
            ViewBag.BrandID = new SelectList(brs.GetActive(), "ID", "Name", item.BrandID);
            gelen.ProductName = item.ProductName;
            gelen.UnitPrice = item.UnitPrice;
            gelen.CategoryID = item.CategoryID;
            gelen.SubCategoryID = item.SubCategoryID;
            gelen.ThirdCategoryID = item.ThirdCategoryID;
            gelen.BrandID = item.BrandID;
            bool result = ps.Update(gelen);
            if (result)
            {
                return RedirectToAction("Index", "ProductDetail", new { id = gelen.ID });

            }

            return View();
        }
        public ActionResult Delete(Guid id)
        {
            Product gelen = ps.GetByID(id);
            ps.Remove(id);
            return RedirectToAction("Index", new { id = gelen.AppUserID });
        }
        public JsonResult Categories(Guid? id, string durum)
        {
            if (id != null)
            {
                if (durum == "Sub")
                {
                    var data = new SelectList(scats.GetDefault(m => m.CategoryID == id), "ID", "SubCategoryName").ToList();
                    var ilk = new SelectListItem { Text = "Kategori Seçiniz", Value = "0" };
                    List<SelectListItem> lists = new List<SelectListItem>();
                    lists.Add(ilk);

                    foreach (var item in data)
                    {
                        lists.Add(item);
                    }
                    ViewData["SubCategoryID"] = lists;
                    return Json(ViewData["SubCategoryID"], JsonRequestBehavior.AllowGet);
                }
                else if (durum == "Third")
                {
                    var data = new SelectList(tcats.GetDefault(m => m.SubCategoryID == id), "ID", "ThirdCategoryName").ToList();
                    var ilk = new SelectListItem { Text = "Kategori Seçiniz", Value = "0" };
                    List<SelectListItem> lists = new List<SelectListItem>();
                    lists.Add(ilk);
                    foreach (var item in data)
                    {
                        lists.Add(item);
                    }
                    ViewData["ThirdCategoryID"] = lists;
                    return Json(ViewData["ThirdCategoryID"], JsonRequestBehavior.AllowGet);
                }
            }
            ViewData["SubCategoryID"] = new SelectListItem { Text = "1.Kategori Seçiniz", Value = "0" };
            ViewData["ThirdCategoryID"] = new SelectListItem { Text = "1.Kategori Seçiniz", Value = "0" };

            return Json((SelectListItem)ViewData["SubCategoryID"], JsonRequestBehavior.AllowGet);





        }
    }
}