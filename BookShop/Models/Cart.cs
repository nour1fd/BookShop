using BookShop.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Models
{
    public class Cart
    {
        private readonly BookShopContext _context;

        public Cart(BookShopContext context)
        {
            this._context = context;
        }
        public string Id { get; set; }
        public List<CartItem> cartItems { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<BookShopContext>();
            string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();

            session.SetString("Id", cartId);

            return new Cart(context) { Id = cartId };
        }
        public List<CartItem> GetAllCartItem()
        {
            return cartItems??(cartItems= _context.CartItems.
                Where(ci=>ci.CartId==Id).Include(ci=>ci.book).ToList());
        }
        public int GETCartPrice() {
            return _context.CartItems.
                    Where(ci => ci.CartId == Id).Select(ci => ci.book.Price * ci.Quantity).Sum();
                }
        public CartItem GetCartItem(Book book) {
            return _context.CartItems.SingleOrDefault(
                ci=>ci.book.Id==book.Id && ci.CartId==Id
                );
        }
        public void AddtoCArt(Book book ,int quntity) {
            var cartItem = GetCartItem(book);
            if (cartItem == null ) {
                cartItem = new CartItem
                {
                    CartId = Id,
                    Quantity = quntity,
                    book = book
                };
            _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quntity;
            }
            _context.SaveChanges();
                }

        public int ReduceQuantity(Book book) {
            var carItem = GetCartItem(book);
            var ReminingQuantity = 0;
            if (carItem != null)
            {
                if (carItem.Quantity > 1)
                {
                    ReminingQuantity = --carItem.Quantity;
                }
                else
                {
                    _context.CartItems.Remove(carItem);
                }
            }
            _context.SaveChanges();
            return ReminingQuantity;    
                
                }


        public int IncreaseQuantity(Book book)
        {
            var carItem = GetCartItem(book);
            var ReminingQuantity = 0;
            if (carItem != null)
            {
                if (carItem.Quantity > 0)
                {
                    ReminingQuantity = ++carItem.Quantity;
                }
            }
            _context.SaveChanges();
            return ReminingQuantity;

        }
        public void RemoveFromCart(Book book)
        {
            var carItem=GetCartItem(book);
            if(carItem != null)
            {
                _context.CartItems.Remove(carItem);
            }
            _context.SaveChanges();
        }

        public void CleaCartItems()
        {
            var cartItem = _context.CartItems.Where(ci => ci.CartId == Id);
            _context.CartItems.RemoveRange(cartItem);
            _context.SaveChanges(true); 
        
        }


    }
}
