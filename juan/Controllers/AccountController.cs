using juan.Models;
using juan.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace juan.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "something went wrong");
                    return View(loginVM);
                }
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
            if (result.IsLockedOut)
            {


                ModelState.AddModelError("", "blocked");
                return View(loginVM);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "something went wrong");
                return View(loginVM);
            }

            await _signInManager.SignInAsync(user, loginVM.RememberMe);


            return RedirectToAction("Index", "Home");

        }
        public IActionResult Register()
        {
            return View();


        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {

            if (!ModelState.IsValid) return View();
            AppUser user = new();
            user.FullName = registerVM.FullName;
            user.Email = registerVM.Email;
            user.UserName = registerVM.Username;
            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                return View(registerVM);
            }







            return RedirectToAction("Index", "Home");



        }

    }
}
