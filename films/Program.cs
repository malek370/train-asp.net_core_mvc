using WebApplication2.DAL.data;
using Microsoft.SqlServer;
using Microsoft.EntityFrameworkCore;
using DAL.Repositories.Irepositories;
using DAL.Repositories;
using WebApplication2.BOL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Utilities;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<appDbContext>(
            options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<appDbContext>().AddDefaultTokenProviders();
            builder.Services.AddScoped<ICatRepository, CatRepositry>();
            builder.Services.AddScoped<IEmailSender,EmailSender>();
            builder.Services.AddScoped<IProdRepository, ProdRepository>();
            var app = builder.Build();

            //Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();
			app.Run();
        }
    }
}