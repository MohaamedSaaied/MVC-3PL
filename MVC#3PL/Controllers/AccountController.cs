using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_3PL.Helper;
using MVC_3PL.ViewModel;

namespace MVC_3PL.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManger;


        public AccountController(UserManager<AppUser> userManager
            ,SignInManager<AppUser> signInManger)
        {
            this.userManager = userManager;
            this.signInManger = signInManger;
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
                            return RedirectToAction("Login");
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
   
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(input.Email);
                if (user is not null)
                {
                    if (await userManager.CheckPasswordAsync(user, input.Password))
                    {
                        var result = await signInManger.PasswordSignInAsync(user, input.Password,input.RememberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");

                    } 
                }
                        ModelState.AddModelError("", "Incorrect Email Or Password");
                        return View(input);
            }  
            return View(input);
        }
        public new async Task<IActionResult> SignOut()
        {
            await signInManger.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(input.Email);

                if (user is not null)
                { 
                    var token= await userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account", new {Email=input.Email,Token=token},Request.Scheme);
                    var email = new Email
                    {
                        Body = url,
                        Subject = "Reset Password",
                        To = input.Email
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            return View(input);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }


        public IActionResult ResetPassword( string Email,string Token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(input.Email);

                if (user is not null)
                {
                    var res = await userManager.ResetPasswordAsync(user,input.Token,input.Password);
                    if (res.Succeeded) 
                    {
                        return RedirectToAction(nameof(Login));
                    }
                    foreach(var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(input);
        }









    }
}

