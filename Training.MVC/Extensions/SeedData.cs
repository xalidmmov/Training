using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Training.BL.Enum;
using Training.Core.Entities;
using Training.DAL.DAL;

namespace Training.MVC.Extensions
{
	public static class SeedData
	{
		public static async void UseUserSeed(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope()) 
			{
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
				var roleManager=scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				if (!roleManager.Roles.Any())
				{
					foreach (Roles role in Enum.GetValues(typeof(Roles))){
						await roleManager.CreateAsync(new IdentityRole(role.ToString()));
					}
				}
				if(!userManager.Users.Any(x=>x.NormalizedUserName=="ADMIN"))
				{
					User Admin = new User
					{
						UserName="Admin",
						Email="Admin"

					};
					await userManager.CreateAsync(Admin,"Admin123.");
					await userManager.AddToRoleAsync(Admin, Roles.Admin.ToString());

				}
			}
		} 
		

		}
	}

