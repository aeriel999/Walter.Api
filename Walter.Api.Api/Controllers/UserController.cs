using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Walter.Api.Core.Dtos;
using Walter.Api.Core.Entities;
using Walter.Api.Core.Interfaces;
using Walter.Api.Core.Services;
using Walter.Api.Core.Validations;

namespace Walter.Api.Api.Controllers;
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly UserService _userService;

	public UserController(UserService userService)
	{
		_userService = userService;
	}

	[HttpGet("GetAll")]
	public async Task<IActionResult> GetAll()
	{
		var result = await _userService.GetAll();

		return Ok(result.PayLoad);
	}

	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto model)
	{
		var result = await _userService.LoginUserAsync(model);
		return Ok(result);
	}

	[AllowAnonymous]
	[HttpPost("RefreshToken")]
	public async Task<IActionResult> RefreshTokenAsync([FromBody] TokenRequestDto model)
	{
		var res = await _userService.RefreshTokenAsync(model);

		return Ok(res);	
	}


	[HttpPost("AddUser")]
	public async Task<IActionResult> AddUser(AddUserDto model)
	{
		var validator = new AddUserValidation();

		var result = validator.Validate(model);

		if (result.IsValid)
		{
			var newUser = await _userService.Create(model);

			if (newUser.Success)
			{
				return Ok(newUser.Message);
			}
			else
			{
				return Problem(newUser.Message);
			}
		}
		else
		{
			return Problem(result.Errors[0].ToString());
		}
	}

	[HttpPost("EditUser")]
	public async Task<IActionResult> EditUser(EditUserDto model)
	{
		var validator = new EditUserValidation();

		var result = validator.Validate(model);

		if (result.IsValid)
		{
			var newUser = await _userService.Edit(model);

			if (newUser.Success)
			{
				return Ok(newUser.Message);
			}
			else
			{
				return Problem(newUser.Message);
			}
		}
		else
		{
			return Problem(result.Errors[0].ToString());
		}
	}

	[HttpPost("DeleteUser")]
	public async Task<IActionResult> DeleteUser(string id)
	{
		var newUser = await _userService.Delete(id);

		if (newUser.Success)
		{
			return Ok(newUser.Message);
		}
		else
		{
			return Problem(newUser.Message);
		}
	}

	[HttpPost("ConfirmEmail")]
	public async Task<IActionResult> ConfirmEmailAsync(string userId, string token)
	{
		var result = await _userService.ConfirmEmailAsync(userId, token);

		if (result.Success)
		{
			return Ok(result.Message);
		}

		return Problem(result.Message);

	}
}
