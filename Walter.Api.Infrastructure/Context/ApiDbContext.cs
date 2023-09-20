using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walter.Api.Core.Entities;
using Walter.Api.Infrastructure.Initializers;

namespace Walter.Api.Infrastructure.Context;
public class ApiDbContext : IdentityDbContext
{
	public ApiDbContext() : base() { }
	public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
	public DbSet<ApiUser> ApiUsers { get; set; }
	public DbSet<IdentityRole> ApiRoles { get; set; }
	public DbSet<RefreshToken> RefreshTokens { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		//builder.SeedUsers();
		//builder.SeedRoles();
	}
}
