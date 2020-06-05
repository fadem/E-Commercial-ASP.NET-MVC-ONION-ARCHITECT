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
    public class CommentController : Controller
    {
        // GET: Administrator/Comment
        CommentService cs = new CommentService();
        public ActionResult Index(Guid id)
        {
            return View(cs.GetDefault(m => m.ProductID == id).ToList());
        }
        public ActionResult Delete(Guid id)
        {
            Comment yorum = cs.GetByID(id);
            cs.Remove(id);
            return RedirectToAction("Index", new { id = yorum.ProductID });
        }
    }
}