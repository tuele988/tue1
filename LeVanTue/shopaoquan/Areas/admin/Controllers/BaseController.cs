using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopaoquan.Areas.admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: admin/Base
        public BaseController()
        {
            if (System.Web.HttpContext.Current.Session["userAdmin"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/admin/Login");
            }
        }
    }
}