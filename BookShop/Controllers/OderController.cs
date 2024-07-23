using BookShop.Data;
using BookShop.Migrations;
using BookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [Authorize]
    public class OderController : Controller
    {
        private readonly BookShopContext _context;
        private readonly Cart _cart;

        public OderController(BookShopContext context,Cart cart)
        {
            this._context = context;
            this._cart = cart;
        }
        public IActionResult CheckOut()
        {
            return View();
        }
      [HttpPost]
        public IActionResult CheckOut(Order order) {
        var cartItems=_cart.GetAllCartItem();
            _cart.cartItems=cartItems;
            if (_cart.cartItems.Count==0) {
                ModelState.AddModelError("" , "Cart Is EMpty Soory !");
            }
            if (ModelState.IsValid)
            {
                CreatOrder(order);
                _cart.CleaCartItems();
                return View("CheckOutComplete", order);
                
            }
            return View(order);
        }


        public IActionResult CheckOutComplete(Order order) {
        return View(order);
        }

        public void CreatOrder(Order order) {
        order.OrderPlaced=DateTime.Now;
            var cartItems = _cart.cartItems;
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    BookID = item.book.Id,
                    OrderId = item.Id,
                    Price = item.book.Price * item.Quantity

                };
                order.OrdersItems.Add(orderItem);
                order.OrderTotal += orderItem.Price;
            }
            _context.Orders.Add(order); 
            _context.SaveChanges();
        }
    }
}
