using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopaoquan.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        public loginController()
        {
            if (System.Web.HttpContext.Current.Session["userAdmin"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Login");
            }
        }
    }
}