using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walter.Api.Core.Dtos;
using Walter.Api.Core.Entities;

namespace Walter.Api.Core.Validations;
public class AddUserValidation : AbstractValidator<AddOrEditUserDto>
{
    public AddUserValidation()
    {
		RuleFor(r => r.Email).NotEmpty().WithMessage("Field must not be empty").EmailAddress().WithMessage("Wrong email format");
		//RuleFor(r => r.Password).NotEmpty().WithMessage("Field must not be empty").MinimumLength(6).WithMessage("Password must be at least 6 characters");
	}
}
