using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training.BL.ViewModels.Account;
using Training.Core.Entities;
using Training.DAL.DAL;
using Training.DAL.Migrations;
using Training.MVC.Extensions;

namespace Training.MVC.Controllers
{
    public class AccountController(TrainingDbContext _context,RoleManager<IdentityRole> roleManager,UserManager<User> userManager,SignInManager<User> signInManager) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Login()
        {
			if (User.Identity!.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();

        }
        [HttpPost]
		public async Task<IActionResult> Login(LoginVM vm )
        {
			if (User.Identity!.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}
			if (!ModelState.IsValid) {
                return View();
            }
            var user=await userManager.FindByNameAsync(vm.UserName);   
            if(user==null)
            {
                ModelState.AddModelError("UserName", "Username ve ya password sehdir");
            }
            var result = await signInManager.PasswordSignInAsync(user!, vm.Password, vm.RememberMe, false);
            if(!result.Succeeded)
            {
                ModelState.AddModelError("UserName", "username ve ya password sefdir");
            }
			if(!ModelState.IsValid) {
				return View();
			}
            
            return RedirectToAction("Index","Home");

		}
        [HttpGet]
        public async Task<IActionResult> Register()
        {
			if (User.Identity!.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
        }
        [HttpPost]
		public async Task<IActionResult> Register(RegisterVM vm)
		{
			if (User.Identity!.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}
			if (!ModelState.IsValid)
            {
                return View();
            }
            if(await _context.Users.AnyAsync(x=>x.UserName== vm.UserName))
            {
                ModelState.AddModelError("UserName", "Username or password is wrong");
            }
            User user = new()
            {
                UserName = vm.UserName,
                Email = vm.UserName
            };
            await userManager.CreateAsync(user,vm.Password);
            return RedirectToAction(nameof(Login));
			
		}
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

	}
}