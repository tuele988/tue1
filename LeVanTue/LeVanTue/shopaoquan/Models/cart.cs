using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shopaoquan.Models
{
    public class cartitem
    {
        public ModerProduct _shopping_product { get; set; }
        public int _shopping_quantity { get; set; }
    }
    public class cart
    {
        List<cartitem> items = new List<cartitem>();
        public IEnumerable<cartitem> Items
        {
            get { return items; }
        }
        public void Add(ModerProduct pro, int quantity=1)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.Id == pro.Id);
            if(item == null)
            {
                items.Add(new cartitem
                {
                    _shopping_product = pro,
                    _shopping_quantity=quantity
                }
                ) ;
            }
            else
            {
                item._shopping_quantity += quantity;
            }

        }
        public void Update_quantity_shopping(int id, int _quantity)
        {

            var item = items.Find(s => s._shopping_product.Id == id);
            if(item != null)
            {
                item._shopping_quantity = _quantity;
            }
        }
        public double Total_Money()
        {
            var total = items.Sum(s => s._shopping_product.Price * s._shopping_quantity);
            return (double)total;
        }
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._shopping_product.Id == id);
        }
        public int Total_Quantity_in_Cart()
        {
            return items.Sum(s => s._shopping_quantity);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}