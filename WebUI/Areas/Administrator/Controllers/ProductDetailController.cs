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
    public class ProductDetailController : Controller
    {
        ProductService pros = new ProductService();
        ProductDetailService pds = new ProductDetailService();
        SizeToService ss = new SizeToService();
        ColourService cs = new ColourService();
        public ActionResult Index(Guid id)
        {
            Product gelen = pros.GetByID(id);
            ViewBag.ProductName = gelen.ProductName;
            ViewBag.ProductID = gelen.ID;

            return View(pds.GetDefault(m => m.ProductID == id).ToList());
        }
        public ActionResult Insert(Guid id)
        {
            Product gelen = pros.GetByID(id);
            ProductDetail gelendetay = pds.GetByID(id);
            ViewBag.SizeToID = new SelectList(ss.GetActive(), "ID", "ProductSize");
            ViewBag.ColourID = new SelectList(cs.GetActive(), "ID", "ProductColour");
            if (gelen != null && gelendetay == null)
            {
                ViewBag.ProductName = gelen.ProductName;
                return View();

            }
            ViewBag.ProductName = gelendetay.Product.ProductName;

            return View();
        }
        [HttpPost]
        public ActionResult Insert(ProductDetail item, Guid id)
        {
            ViewBag.SizeToID = new SelectList(ss.GetActive(), "ID", "ProductSize", item.SizeToID);
            ViewBag.ColourID = new SelectList(cs.GetActive(), "ID", "ProductColour", item.ColourID);
            item.ProductID = id;
            bool result = pds.Add(item);
            if (result)
            {
               
                return RedirectToAction("Index", new {id=item.ProductID });
            }

            return View();
        }
        public ActionResult Update(Guid id)
        {
            ProductDetail item = pds.GetByID(id);
            ViewBag.ProductName = item.Product.ProductName;
            ViewBag.SizeToID = new SelectList(ss.GetActive(), "ID", "ProductSize", item.SizeToID);
            ViewBag.ColourID = new SelectList(cs.GetActive(), "ID", "ProductColour", item.ColourID);
            return View(pds.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(ProductDetail item)
        {
            ViewBag.SizeToID = new SelectList(ss.GetActive(), "ID", "ProductSize", item.SizeToID);
            ViewBag.ColourID = new SelectList(cs.GetActive(), "ID", "ProductColour", item.ColourID);
            ProductDetail gelen = pds.GetByID(item.ID);
            gelen.SizeToID = item.SizeToID;
            gelen.ProductID = item.ProductID;
            gelen.ColourID = item.ColourID;
            gelen.UnitInStock = item.UnitInStock;
            bool result = pds.Update(gelen);
            if (result)
            {
                return RedirectToAction("Index", new { id = item.ProductID });

            }

            return View();
        }
        public ActionResult Delete(Guid id)
        {
            ProductDetail gelen = pds.GetByID(id);
            pds.Remove(id);
            return RedirectToAction("Index", new { id = gelen.ProductID });
        }
    }
}