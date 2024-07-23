using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Identity;
using BookShop.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
namespace BookShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BookShopContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookShopContext") ?? throw new InvalidOperationException("Connection string 'BookShopContext' not found.")));

            builder.Services.AddDefaultIdentity<DefaultUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<BookShopContext>();
            builder.Services.AddRazorPages();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            builder.Services.AddScoped<SeedData, SeedData>(); //can be placed among other "AddScoped" - above: var app = builder.Build();
            builder.Services.AddTransient<IEmailSender, EmailSender>();                               //            builder.Services.AddTransient<IEmailSender, EmailSender>();

            builder.Services.AddScoped<Cart>(sp=>Cart.GetCart(sp));
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(op=>
            {
                op.Cookie.HttpOnly = true;
                op.Cookie.IsEssential   = true;
                //op.IdleTimeout = TimeSpan.FromSeconds(10);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();

            SeedDatabase();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseSession();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Store}/{action=Index}/{id?}");


            app.Run();
            async void SeedDatabase() //can be placed at the very bottom under app.Run()
            {
                
                using (var scope = app.Services.CreateScope())
                {
                    var service = scope.ServiceProvider;
                    await UserRoleInitializer.InitializeAsync(service);   
                    SeedData.Initialize(service);

                }
            }
        }
    }
}
