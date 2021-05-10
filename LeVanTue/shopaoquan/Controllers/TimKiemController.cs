using shopaoquan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopaoquan.Controllers
{
    public class TimKiemController : Controller
    {
        ShopAoQuanDBontext db = new ShopAoQuanDBontext();
        // GET: TimKiem
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string key)
        {
            _ = ViewBag.Message;
            return View();
        }
        public List<ModerProduct> SearchProduct(string key)
        {
            return db.Product.SqlQuery("select * from Product where Name like '%"+key+"%'").ToList();
        }
    }
}