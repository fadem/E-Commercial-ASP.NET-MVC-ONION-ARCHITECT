using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Administrator.ViewModels
{
    public class Categories
    {
        public Guid CategoryID { get; set; }
        public Guid SubCategpryID { get; set; }
        public List<SelectListItem> birinciKategoriler { get; set; }
        public List<SelectListItem> ikinciKategoriler { get; set; }
    }
}