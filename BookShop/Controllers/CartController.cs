using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly Cart _cart;
        private readonly BookShopContext _context;

        public CartController(BookShopContext context, Cart cart)
        {
            _context = context;
            this._cart = cart;
        }
        public IActionResult Index()
        {
            var item = _cart.GetAllCartItem();
            _cart.cartItems = item;
            return View(_cart);
        }

        public Book GetBookBYID(int id)
        {
            return _context.Book.FirstOrDefault(b => b.Id == id);
        }

        public IActionResult AddToCart(int id) {
            var selectedBook = GetBookBYID(id);
            if (selectedBook != null)
            {

                _cart.AddtoCArt(selectedBook, quntity: 1);
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFrpmCart(int id) {
            var selectedBook = GetBookBYID(id);
            if (selectedBook != null)
            {
                _cart.RemoveFromCart(selectedBook);

            }
            return RedirectToAction("Index");
        }

        public IActionResult REduceQuantity(int id) {
            var selectedBook =GetBookBYID(id);
            if (selectedBook != null)
            {
                _cart.ReduceQuantity(selectedBook);
            }
            return RedirectToAction("Index");
                }
       public IActionResult IncreaseQuantity(int id) {
            var selectedBook =GetBookBYID(id);
            if (selectedBook != null)
            {
                _cart.IncreaseQuantity(selectedBook);
            }
            return RedirectToAction("Index");
                }

        public IActionResult CLearCart() {
            _cart.CleaCartItems();
            return RedirectToAction("Index");   
        }
    }
}
