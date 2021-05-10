using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace shopaoquan.Areas.admin.Controllers
{
    public class ShopAoQuanController : BaseController
    {
        // GET: admin/ShopAoQuan
        public ActionResult Index()
        {
            if (Session["UserAdmin"].Equals(""))
            {
                return Redirect("~/admin/login");
            }
            return View();
        }
    }
}