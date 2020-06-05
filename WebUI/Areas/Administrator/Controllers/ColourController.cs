using Model.Entities;
using Service.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Areas.Administrator.Controllers
{
    [AdminAuthentication]
    public class ColourController : Controller
    {
        // GET: Administrator/Color
        ColourService cs = new ColourService();
        public ActionResult Index()
        {
            return View(cs.GetAll());
        }
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Colour item)
        {
            cs.Add(item);
            return RedirectToAction("Index");
        }
        public ActionResult Update(Guid id)
        {
            return View(cs.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Colour item)
        {

            Colour gelen = cs.GetByID(item.ID);
            gelen.ProductColour = item.ProductColour;
            cs.Update(gelen);

            return RedirectToAction("Index");

        }
        public ActionResult Delete(Guid id)
        {
            cs.Remove(id);
            return RedirectToAction("Index");

        }
    }
}