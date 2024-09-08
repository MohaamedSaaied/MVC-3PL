using BLL.Interfaces;
using BLL.Repositories;
using DAL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MVC_3PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddScoped<AppDBContext>(); //Allow Dependency Injection

            builder.Services.AddDbContext<AppDBContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
            }); //Allow Dependency Injection

            builder.Services.AddScoped<IDepartmentRepo,DepartmentRepo>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
