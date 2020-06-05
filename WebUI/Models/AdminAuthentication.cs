using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Models
{
    public class AdminAuthentication : AuthorizeAttribute
    {
        AppUser user = new AppUser();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["oturum"]!=null)
            {
                user = (AppUser)httpContext.Session["oturum"];
            }
                
            else if(httpContext.Session["admin"]!=null)
            {
              user = (AppUser)httpContext.Session["admin"];

            }
           
            if ( user.IsAdministrator || user.IsPageAdmin)
            {
                return true;
            }
            httpContext.Response.Redirect("/Home/Index");

            return false;
        }
    }
}