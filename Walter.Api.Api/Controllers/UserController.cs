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

		if (result.Success)
			return Ok(result);
		else return BadRequest(result.Message);
	}

	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto model)
	{
		var result = await _userService.LoginUserAsync(model);
		return Ok(result);
	}

	[HttpGet("logout")]
	public async Task<IActionResult> LogoutUserAsync(string userID)
	{
		var result = await _userService.LogoutUserAsync(userID);

		if (result.Success)
		{
			return Ok(result);
		}
		return BadRequest(result);
	}

	[HttpGet("GetUser")]
	public async Task<IActionResult> GetUser(string userID)
	{
		var result = await _userService.GetUser(userID);

		if (result.Success)
		{
			return Ok(result);
		}
		return BadRequest(result);
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
			var newUserresult = await _userService.Create(model);

			if (newUserresult.Success)
			{
				return Ok(newUserresult);
			}
			else
			{
				return BadRequest(newUserresult);
			}
		}
		else
		{
			return BadRequest(result.Errors[0].ToString());
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
				return Ok(newUser);
			}
			else
			{
				return BadRequest(newUser);
			}
		}
		else
		{
			return BadRequest(result.Errors[0].ToString());
		}
	}

	[HttpPost("DeleteUser")]
	public async Task<IActionResult> DeleteUser([FromBody] string id)
	{
		var newUser = await _userService.Delete(id);

		if (newUser.Success)
		{
			return Ok(newUser);
		}
		else
		{
			return BadRequest(newUser);
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

		return BadRequest(result.Message);

	}

	[HttpPost("UpdateProfile")]
	public async Task<IActionResult> ProfileUpdate(EditUserDto model)
	{
		//if (model.Email == string.Empty)
		//{
		//	var validator = new UpdatePasswordValidation();

		//	var validationResult = await validator.ValidateAsync(model);

		//	if (validationResult.IsValid)
		//	{
		//		var result = await _userService.UpdatePasswordAsync(model);

		//		if (result.Success)
		//			return RedirectToAction(nameof(SignIn));
		//		else
		//			ViewBag.UpdateInfoError = result.Message;
		//	}
		//	else
		//		ViewBag.UpdateInfoError = validationResult.Errors[0];
		//}
		//else
		//{
		//	var validator = new EditUserValidation();

		//	var validationResult = await validator.ValidateAsync(model);

		//	if (validationResult.IsValid)
		//	{
		//		var result = await _userService.EditUserAsync(model);

		//		if (!result.Success)
		//			ViewBag.UpdatePasswordError = result.Message;
		//	}
		//	else
		//		ViewBag.UpdatePasswordError = validationResult.Errors[0];
		//}

		//return View(model);

		return Ok();
	}

	[HttpPost (("ChangePassword"))]
	public async Task<IActionResult> ChangePassword(ChangePasswordDto info)
	{
		var result = await _userService.UpdatePassword(info);

		if (result.Success)
		{
			return Ok(result);
		}

		return BadRequest(result);
	}



}
