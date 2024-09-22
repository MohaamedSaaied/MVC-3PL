using AutoMapper;
using BLL;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_3PL.Helper;
using MVC_3PL.ViewModel;

namespace MVC_3PL.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public readonly UserManager<AppUser> userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(string search)
        {
            List<AppUser> users ;
            if (string.IsNullOrEmpty(search)) { 
                users= await userManager.Users.ToListAsync();
            }
            else
            {
                users= await userManager.Users.Where(u=>u.FirstName.Contains(search)).ToListAsync();
            }
            return View(users);
        }

        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var res = await userManager.FindByIdAsync(id);

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
        public async Task<IActionResult> Edit(string id, AppUser model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    var res =await userManager.UpdateAsync(user);
                    if (res.Succeeded)
                    {

                        return RedirectToAction("Index");
                    }
                    return NotFound();
                }
                catch (Exception) {
                    throw;
                }

            }
            return NotFound();

        }


        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {

            if (id is null) return BadRequest();
            var res = await userManager.FindByIdAsync(id);
            if (res is null)
            {
                return NotFound();
            }
            await userManager.DeleteAsync(res);
            return RedirectToAction("Index");
        }



    }
}
