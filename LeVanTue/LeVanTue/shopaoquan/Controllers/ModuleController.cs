using shopaoquan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopaoquan.Controllers
{
    public class ModuleController : Controller
    {
        ShopAoQuanDBontext db = new ShopAoQuanDBontext();
        // GET: Module
        public ActionResult MainMenu()
        {
            var list = db.Menu.Where(m => m.Status == 1 && m.Parenid == 0).ToList();
            return View("MainMenu", list);
            
        }
        public ActionResult SubMainMenu(int id)
        {
            var row_menu = db.Menu.Find(id);
            ViewBag.row_menu = row_menu;
            var list = db.Menu.Where(m => m.Status == 1 && m.Parenid == id).ToList();
            if (list.Count() > 0)
            {
                return View("SupMainMenu2", list.ToList());
            }
            else
            {
                return View("SupMainMenu1", list);
            }
        }
        public ActionResult Slidershow()
        {
            var list = db.Slider.Where(m => m.Status == 1 && m.Position == "slidershow").OrderByDescending(m => m.Created_at).ToList();
            return View("Slidershow",list);
        }
        public ActionResult Listcategory()
        {
            var list1 = db.Category.Where(m => m.Status == 1 && m.Parentid==0).ToList();
            return View("Listcategory", list1);
        }
        public ActionResult Productbuy()
        {
            var list2 =db.Product.Where(m => m.Status == 1 ).OrderByDescending(m => m.Created_at).Take(6).ToList();
            return View("Productbuy", list2);
        }
        public ActionResult newpost()
        {
            var list2 = db.Post.Where(m => m.Status == 1).OrderByDescending(m => m.Created_at).Take(3).ToList();
            return View("newpost", list2);
        }
        public ActionResult newsproduct()
        {
    
            var list2 = db.Product.Where(m => m.Status == 1).OrderByDescending(m => m.Created_at).Take(6).ToList();
            return View("newsproduct", list2);
        }
    }
}