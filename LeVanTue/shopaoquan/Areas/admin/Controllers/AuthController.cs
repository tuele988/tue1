using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shopaoquan.Library;
using shopaoquan.Models;

namespace shopaoquan.Areas.admin.Controllers
{
   
    public class AuthController : Controller
    {
        ShopAoQuanDBontext db = new ShopAoQuanDBontext();
        // GET: admin/Auth
        public ActionResult Login()
        {
            if (!Session["UserAdmin"].Equals(""))
            {
                return RedirectToAction("index", "ShopAoQuan");
            }
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection field)
        {
            string rr = "";
            string username = field["username"]; 
            string password = myString.ToMD5(field["password"]);
            ModelUser modelUser = db.User.Where(m=>m.Status==1 && m.Access==1 && (m.UserName == username || m.Email== username)).FirstOrDefault();
            if (modelUser == null)
            {
                rr = "nhap lai user ";
            }
            else
            {
                if (modelUser.Password.Equals(password))
                {
                    Session["UserAdmin"] = modelUser.UserName;
                    Session["UserId"] = modelUser.Id;
                    Session["Fulname"] = modelUser.Fullname;
                    Session["Image"] = modelUser.Img;
                    return RedirectToAction("index", "ShopAoQuan");
                }
                else
                {
                    rr = password;
                }
            }
            ViewBag.Error = "<span class='text-danger'>" + rr + "</span>";
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["UserId"] = "";
            Session["Fulname"] = "";
            Session["Image"] ="";
            return RedirectToAction("Login", "ShopAoQuan");
        }
    }
}