using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Training.BL.Enum;
using Training.Core.Entities;
using Training.DAL.DAL;

namespace Training.MVC.Extensions
{
	public class SeedData(TrainingDbContext _context,RoleManager<IdentityRole> roleManager,UserManager<User> userManager,SignInManager<User> signInManager)
	{
		public async Task SeedDataAsync()
		{
			if(! await _context.Roles.AnyAsync())
			{
				foreach(var item in Enum.GetValues(typeof(Roles))) {
					await roleManager.CreateAsync(new IdentityRole(item.ToString()));
				
				}
			}
			if(!await _context.Users.AnyAsync(x=>x.UserName=="Admin"))
			{
				User user = new()
				{
					UserName = "Admin",
					Email = "Admin"
				};
				await userManager.CreateAsync(user,"Admin123.");
				await userManager.AddToRoleAsync(user,Roles.Admin.ToString());
			}
		}
	}
}
