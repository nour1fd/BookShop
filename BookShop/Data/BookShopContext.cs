using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookShop.Data
{
    public class BookShopContext :IdentityDbContext<DefaultUser>
    {
        public BookShopContext (DbContextOptions<BookShopContext> options)
            : base(options)
        {
        }

        public DbSet<BookShop.Models.Book> Book { get; set; } = default!;
        public DbSet<BookShop.Models.CartItem> CartItems { get; set; } = default!;
        public DbSet<BookShop.Models.Order> Orders { get; set; } = default!;
        public DbSet<BookShop.Models.OrderItem> OrdersItems { get; set; } = default!;
    }
}
