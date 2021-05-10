using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shopaoquan.Models;

namespace shopaoquan.Controllers
{
    
    public class CustomerController : Controller
    {
        ShopAoQuanDBontext db = new ShopAoQuanDBontext();
        // GET: Customer
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ModelUser cus)
        {

                db.User.Add(cus);
                db.SaveChanges();
                return RedirectToAction("ShowToCart", "ShoppingCart");
           
        }
    }
}