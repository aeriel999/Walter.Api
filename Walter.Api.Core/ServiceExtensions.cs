using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walter.Api.Core.AutoMapper;
using Walter.Api.Core.Interfaces;
using Walter.Api.Core.Services;

namespace Walter.Api.Core;
public static class ServiceExtensions
{
	public static void AddCoreServices(this IServiceCollection services)
	{
		services.AddTransient<UserService>();
		services.AddTransient<JwtServise>();
	}

	public static void AddMappings(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(AutoMapperUserProfile));
	}
}
