
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoroukAcademy.ShaTask.Presentation.ViewModels;

namespace ShoroukAcademy.ShaTask.Presentation.Controllers
{
    public class UserController : Controller
    {
        UserManager<IdentityUser> UserManager;
        SignInManager<IdentityUser> SignInManager;
        RoleManager<IdentityRole> RoleManager;
        public UserController(UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> _SignInManager,
              RoleManager<IdentityRole> _RoleManager)
        {
            UserManager = userManager;
            SignInManager = _SignInManager;
            RoleManager = _RoleManager;
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View();
            else
            {
                var result =
                await SignInManager.PasswordSignInAsync(model.UserName, model.Password,
                        model.RememberMe, true);

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your Account Is Locked Out Try After 1 Minute");
                    return View();
                }
                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Invalid User Name Or password");
                    return View();
                }
                else
                {

                    return RedirectToAction("Index", "Cashiers");
                }
            }

        }

        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "InvoiceDetails");
        }



        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
