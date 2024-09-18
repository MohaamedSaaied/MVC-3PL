using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_3PL.ViewModel;

namespace MVC_3PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
                    //Mapping : SignUpViewModel To AppUser
                    user = await userManager.FindByEmailAsync(model.Email);

                    if (user is null) 
                    {
                        
                        user = new AppUser()
                        {
                          UserName = model.UserName,
                          Email = model.Email,
                          FirstName=model.FirstName,
                          LastName=model.LastName,
                          IsAgree=model.IsAgree,
                        };
                        var res = await userManager.CreateAsync(user, model.Password);
                        if (res.Succeeded)
                        {
                            return RedirectToAction("SignIn");
                        }
                        foreach (var error in res.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                    ModelState.AddModelError(string.Empty, " E-mail Already Exist !");
                    return View(model);
                }

                ModelState.AddModelError(string.Empty, " Username Already Exist !");
            }
            return View(model);
        } 
    }
}
