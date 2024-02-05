using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walter.Api.Core.Dtos;
public class ChangePasswordDto
{
	public string Id { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string ConfirmNewPassword { get; set; } = string.Empty;
	public string NewPassword { get; set; } = string.Empty;
}
