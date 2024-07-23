using BookShop.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookShopContext(serviceProvider.GetRequiredService<DbContextOptions<BookShopContext>>()))
            {
                if (context.Book.Any())    // Check if database contains any books
                {
                    return;     // Database contains books already
                }

                context.Book.AddRange(
                    new Book
                    {
                        Title = "Bröderna Lejonhjärta",
                        Description = "Nono",

                        Language = "Swedish",
                        ISBN = "9789129688313",
                        DatePunished = DateTime.Parse("2013-9-26"),
                        Price = 139,
                        Author = "Astrid Lindgren",
                        ImgUrl = "/images/lejonhjärta.jpg"
                    },

                    new Book
                    {
                        Title = "The Fellowship of the Ring",
                        Description = "Nono",

                        Language = "English",
                        ISBN = "9780261102354",
                        DatePunished = DateTime.Parse("1991-7-4"),
                        Price = 100,
                        Author = "J. R. R. Tolkien",
                        ImgUrl = "/images/lotr.jpg"
                    },

                    new Book
                    {
                        Title = "Mystic River",
                        Description = "Nono",

                        Language = "English",
                        ISBN = "9780062068408",
                        DatePunished = DateTime.Parse("2011-6-1"),
                        Price = 91,
                        Author = "Dennis Lehane",
                        ImgUrl = "/images/mystic-river.jpg"
                    },

                    new Book
                    {
                        Title = "Of Mice and Men",
                        Description = "Nono",

                        Language = "English",
                        ISBN = "9780062068408",
                        DatePunished = DateTime.Parse("1994-1-2"),
                        Price = 166,
                        Author = "John Steinbeck",
                        ImgUrl = "/images/mystic-river.jpg"
                    },

                    new Book
                    {
                        Title = "The Old Man and the Sea",
                        Description = "Nono",

                        Language = "English",
                        ISBN = "9780062068408",
                        DatePunished = DateTime.Parse("1994-8-18"),
                        Price = 84,
                        Author = "Ernest Hemingway",
                        ImgUrl = "/images/old-man-and-the-sea.jpg"
                    },

                    new Book
                    {
                        Title = "The Road",
                        Description="Nono",
                        Language = "English",
                        ISBN = "9780307386458",
                        DatePunished = DateTime.Parse("2007-5-1"),
                        Price = 95,
                        Author = "Cormac McCarthy",
                        ImgUrl = "/images/the-road.jpg"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
