using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walter.Api.Core.Dtos;

namespace Walter.Api.Core.Validations;
public class EditUserValidation : AbstractValidator<EditUserDto>
{
    public EditUserValidation()
    {
		RuleFor(r => r.Email).NotEmpty().WithMessage("Field must not be empty").EmailAddress().WithMessage("Wrong email format");

	}
}
