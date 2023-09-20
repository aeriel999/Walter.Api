using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walter.Api.Core.Entities;
using Walter.Api.Infrastructure.Context;

namespace Walter.Api.Infrastructure.Initializers;
public static class ApiUsersInitializer
{
	//public static void SeedUsers(this ModelBuilder modelBuilder)
	//{
	//	modelBuilder.Entity<ApiUser>().HasData(new ApiUser[]
	//		{
	//			new ApiUser
	//			{
	//				FirstName = "Bob",
	//				UserName = "admin@email.com",
	//				Email = "admin@email.com",
	//				EmailConfirmed = true,
	//				PhoneNumber = "+xx(xxx)xxx-xx-xx",
	//				PhoneNumberConfirmed = true
	//			},
	//			new ApiUser
	//			{
	//				FirstName = "Alice",
	//				 UserName = "user1@email.com",
	//				Email = "user1@email.com",
	//				EmailConfirmed = true,
	//				PhoneNumber = "+xx(xxx)xxx-xx-xx",
	//				PhoneNumberConfirmed = true
	//			}
	//	});
	//}

	//public static void SeedRoles(this ModelBuilder modelBuilder)
	//{
	//	modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole[]
	//		{
	//			 new IdentityRole()
	//				{
	//					Name = "Administrator",
	//					NormalizedName = "ADMINISTRATOR",
	//				},
	//				new IdentityRole()
	//				{
	//					Name = "User",
	//					NormalizedName = "USER"
	//				}
	//		});
	//}

	public static async Task SeedUsersAndRoles(IApplicationBuilder applicationBuilder)
	{
		using (var serviseScope = applicationBuilder.ApplicationServices.CreateScope())
		{
			var context = serviseScope.ServiceProvider.GetService<ApiDbContext>();

			UserManager<ApiUser> userManager = serviseScope.ServiceProvider.GetRequiredService<UserManager<ApiUser>>();

			if (userManager.FindByEmailAsync("admin@email.com").Result == null)
			{
				ApiUser admin = new ApiUser()
				{
					FirstName = "Bob",
					UserName = "admin@email.com",
					Email = "admin@email.com",
					EmailConfirmed = true,
					PhoneNumber = "+xx(xxx)xxx-xx-xx",
					PhoneNumberConfirmed = true
				};

				ApiUser user = new ApiUser()
				{
					FirstName = "Alice",
					UserName = "user1@email.com",
					Email = "user1@email.com",
					EmailConfirmed = true,
					PhoneNumber = "+xx(xxx)xxx-xx-xx",
					PhoneNumberConfirmed = true
				};

				context.Roles.AddRange(
					new IdentityRole()
					{
						Name = "Administrator",
						NormalizedName = "ADMINISTRATOR",
					},
					new IdentityRole()
					{
						Name = "User",
						NormalizedName = "USER",
					});

				await context.SaveChangesAsync();

				IdentityResult adminResult = userManager.CreateAsync(admin, "Admin+1111").Result;

				IdentityResult user1Result = userManager.CreateAsync(user, "User+1111").Result;

				if (adminResult.Succeeded)
				{
					userManager.AddToRoleAsync(admin, "Administrator").Wait();
				}
				if (user1Result.Succeeded)
				{
					userManager.AddToRoleAsync(user, "User").Wait();
				}
			}
		}
	}
}

