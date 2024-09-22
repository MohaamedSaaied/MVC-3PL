using AutoMapper;
using BLL;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_3PL.Helper;
using MVC_3PL.ViewModel;

namespace MVC_3PL.Controllers
{
    public class RoleController : Controller
    {
        public readonly RoleManager<IdentityRole> roleManager;
        public readonly UserManager<AppUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<AppUser>userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles=await roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var res = await roleManager.CreateAsync(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var res = await roleManager.FindByIdAsync(id);

            if (res == null)
            {
                return NotFound();
            }
            return View(viewName, res);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await roleManager.FindByIdAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    user.Name = model.Name;
                    var res = await roleManager.UpdateAsync(user);
                    if (res.Succeeded)
                    {

                        return RedirectToAction("Index");
                    }
                    return NotFound();
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return NotFound();

        }


        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {

            if (id is null) return BadRequest();
            var res = await roleManager.FindByIdAsync(id);
            if (res is null)
            {
                return NotFound();
            }
            await roleManager.DeleteAsync(res);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddRemoveUsers(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if(role is null)
            {
                return NotFound();
            }
            var users= await userManager.Users.ToListAsync();
            var usersInRole = new List<UserRoleViewModel>();
            foreach(var user in users)
            {
                var userRole = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await userManager.IsInRoleAsync(user, role.Name)) 
                {
                    userRole.IsSelected = true;
                }
                else
                {
                    userRole.IsSelected = false;
                }
                usersInRole.Add(userRole);
            }
            return View(usersInRole);
        }



    }
    
}
