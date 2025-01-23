using Microsoft.EntityFrameworkCore;
using Training.DAL.DAL;
using Training.BL;
using Training.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Training.MVC.Extensions;
namespace Training.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<TrainingDbContext>(x=>x.UseSqlServer(builder.Configuration.GetConnectionString("MSSql")));
            builder.Services.AddServices();
            builder.Services.AddIdentity<User,IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<TrainingDbContext>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseUserSeed();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Dashboard}/{action=index}/{id?}"
              );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
