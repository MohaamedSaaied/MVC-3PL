using BLL;
using BLL.Interfaces;
using BLL.Repositories;
using DAL.Data.Context;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_3PL.Mapping;
using MVC_3PL.Services;

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

            //builder.Services.AddScoped<IDepartmentRepo,DepartmentRepo>();
            //builder.Services.AddScoped<IEmployeeReop, EmployeeRepo>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();






            //Life Time
            //builder.Services.AddScoped();   //Life Time Per Request, Object unreachable
            //builder.Services.AddTransient();//Life Time Per Operation
            // builder.Services.AddSingleton();//Life Time Per Application


            //builder.Services.AddScoped<IScopedService,ScopedService>();
            //builder.Services.AddTransient<ITransientService,TransientService>();
            //builder.Services.AddSingleton<IScopedService,ScopedService>();





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
