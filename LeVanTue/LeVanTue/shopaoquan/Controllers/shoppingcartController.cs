using shopaoquan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopaoquan.Controllers
{
    public class shoppingcartController : Controller
    {
        // GET: shoppingcart

        ShopAoQuanDBontext db = new ShopAoQuanDBontext();
        public cart GetCart()
        {
            cart cart = Session["Cart"] as cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        // GET: cart
        public ActionResult addtocard(int id)
        {
            var pro = db.Product.SingleOrDefault(s => s.Id == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult ShowtoCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ShowToCart", "ShoppingCart");
            cart cart = Session["Cart"] as cart;
            return View(cart);

        }
        public ActionResult Update_Quantity_Result(FormCollection form)
        {
            cart cart = Session["Cart"] as cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["quantity"]);
            cart.Update_quantity_shopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            cart cart = Session["Cart"] as cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int total_item = 0;
            cart cart = Session["Cart"] as cart;
            if(cart != null)
                total_item = cart.Total_Quantity_in_Cart();
                ViewBag.QuantityCart = total_item;
                return PartialView("BagCart");
            
        }
        public ActionResult Shopping_success()
        {
            return View();
        }
        public ActionResult checkout(FormCollection form)
        {
            try
            {
                cart cart = Session["Cart"] as cart;
                ModelOder order = new ModelOder();
                order.Createdate = DateTime.Now;
                order.Code = form["CodeCustomer"];
                order.DeliveryAddress= form["Address_Delivery"];
                order.Status = 1;
                db.Order.Add(order);
                foreach(var item in cart.Items)
                {
                    ModelOderdeatil orderdetail = new ModelOderdeatil();
                    orderdetail.Orderid = order.Id;
                    orderdetail.Productid = item._shopping_product.Id;
                    orderdetail.Price = item._shopping_product.Price;
                    orderdetail.Quantity = item._shopping_quantity;
                    db.Oderdeati.Add(orderdetail);
                }
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("Shopping_success", "shoppingcart");
            }
            catch
            {
                return Content("Error Checkout");
            }
        }
    }
}






