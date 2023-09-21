using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walter.Api.Core.Dtos;
using Walter.Api.Core.Entities;

namespace Walter.Api.Core.Services;
public class UserService
{
	private readonly UserManager<ApiUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly SignInManager<ApiUser> _signInManager;
	private readonly IConfiguration _config;
	private readonly JwtServise _jwtService;
	private readonly IMapper _mapper;

	public UserService(JwtServise jwtService, RoleManager<IdentityRole> roleManager, IConfiguration config,
		UserManager<ApiUser> userManager, SignInManager<ApiUser> signInManager, IMapper mapper)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_config = config;
		_roleManager = roleManager;
		_jwtService = jwtService;
		_mapper = mapper;
	}
	public async Task<ServiceResponse> GetAll()
	{
		var users = await _userManager.Users.ToListAsync();

		return new ServiceResponse
		{
			Success = true,
			Message = "Users are loaded",
			PayLoad = users
		};
	}

	public async Task<ServiceResponse> Create(AddUserDto model)
	{
		var user = _mapper.Map<AddUserDto, ApiUser>(model);

		user.UserName = model.Email;

		var result = await _userManager.CreateAsync(user, model.Password);

		_userManager.AddToRoleAsync(user, model.Role).Wait();

		if (result.Succeeded)
		{
			return new ServiceResponse
			{
				Success = true,
				Message = "User is created"
			};
		}
		else
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not created",
			};
		}
	}

	public async Task<ServiceResponse> Edit(EditUserDto model)
	{
		var user = await _userManager.FindByIdAsync(model.Id);

		if (user == null)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not found",
			};
		}

		if (user.FirstName != model.FirstName)
		{
			user.FirstName = model.FirstName;
		}

		if (user.LastName != model.LastName)
		{
			user.LastName = model.LastName;
		}

		if (user.Email != model.Email)
		{
			user.Email = model.Email;
			user.EmailConfirmed = false;
		}

		if (user.PhoneNumber != model.PhoneNumber)
		{
			user.PhoneNumber = model.PhoneNumber;
			user.PhoneNumberConfirmed = false;
		}

		var role = await _userManager.GetRolesAsync(user);

		if (role == null)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "Cant find role of user"
			};
		}

		await _userManager.RemoveFromRoleAsync(user, role[0]);

		await _userManager.AddToRoleAsync(user, model.Role);

		var result = await _userManager.UpdateAsync(user);

		if (result.Succeeded)
		{
			return new ServiceResponse
			{
				Success = true,
				Message = "User is update"
			};
		}
		else
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not update",
			};
		}
	}

	public async Task<ServiceResponse> Delete(string id)
	{
		var user = await _userManager.FindByIdAsync(id);

		if (user == null)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not found",
			};
		}
		var result = await _userManager.DeleteAsync(user);

		if (result.Succeeded)
		{
			return new ServiceResponse
			{
				Success = true,
				Message = "User is delete"
			};
		}
		else
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not delete",
			};
		}
	}

	public async Task<ServiceResponse> ConfirmEmailAsync(string userId, string token)
	{
		var user = await _userManager.FindByIdAsync(userId);

		if (user == null)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "Not found"
			};
		}

		var decoderToken = WebEncoders.Base64UrlDecode(token);
		var normalToken = Encoding.UTF8.GetString(decoderToken);
		var result = await _userManager.ConfirmEmailAsync(user, normalToken);

		if (result.Succeeded)
		{
			return new ServiceResponse
			{
				Success = true,
				Message = "Confirmed"
			};
		}
		return new ServiceResponse
		{
			Success = false,
			Message = "Not Confirmed",
			Errors = result.Errors.Select(e => e.Description)
		};
	}

	public async Task<ServiceResponse> LoginUserAsync(LoginUserDto model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);
		if (user == null)
		{
			return new ServiceResponse
			{
				Message = "Login or password incorrect.",
				Success = false
			};
		}

		var signinResult = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: true, lockoutOnFailure: true);
		if (signinResult.Succeeded)
		{
			var tokens = await _jwtService.GenerateJwtTokensAsync(user);
			return new ServiceResponse
			{
				AccessToken = tokens.Token,
				RefreshToken = tokens.RefreshToken.Token,
				Message = "User signed in successfully.",
				Success = true
			};
		}

		if (signinResult.IsNotAllowed)
		{
			return new ServiceResponse
			{
				Message = "Confirm your email please.",
				Success = false
			};
		}

		if (signinResult.IsLockedOut)
		{
			return new ServiceResponse
			{
				Message = "User is blocked connect to support.",
				Success = false
			};
		}

		return new ServiceResponse
		{
			Message = "Login or password incorrect.",
			Success = false
		};
	}

	public async Task LogoutUserAsync()
	{
		await _signInManager.SignOutAsync();
	}

	public async Task<ServiceResponse> RefreshTokenAsync(TokenRequestDto model)
	{
		return await _jwtService.VerifyTokenAsync(model);
	}
}
