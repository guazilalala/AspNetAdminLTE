using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wing.AspNetAdminLTE.Authorization;
using Wing.AspNetAdminLTE.Models;

namespace Wing.AspNetAdminLTE.Data
{
    public class SeedData
    {
		public static async Task Initialize(IServiceProvider serviceProvider,string textUserPw)
		{
			using (var context = new ApplicationDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
			{
				var adminId = await EnsureUser(serviceProvider, textUserPw, "admin");
				await EnsureRole(serviceProvider, adminId, OperationConstants.AdministratorsRole);
			};
		}

		private static async Task<string> EnsureUser(IServiceProvider serviceProvider,string testUserPw,string userName)
		{
			var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
			var user = await userManager.FindByNameAsync(userName);

			if (user == null)
			{
				user = new ApplicationUser { UserName = userName };
				await userManager.CreateAsync(user, testUserPw);
			}
			return user.Id;
		}
		
		private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,string uid,string role)
		{
			IdentityResult IR = null;
			var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
			if (!await roleManager.RoleExistsAsync(role))
			{
				IR = await roleManager.CreateAsync(new IdentityRole(role));
			}

			var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
			var user = await userManager.FindByIdAsync(uid);

			IR = await userManager.AddToRoleAsync(user, role);
			return IR;
		}

		public static void SeedDB(ApplicationDbContext context)
		{
		}
    }
}
