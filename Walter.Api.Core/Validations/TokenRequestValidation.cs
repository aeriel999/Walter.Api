using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walter.Api.Core.Dtos;

namespace Walter.Api.Core.Validations;
public class TokenRequestValidation : AbstractValidator<TokenRequestDto>
{
    public TokenRequestValidation()
    {
        RuleFor(r => r.Token).NotEmpty();
        RuleFor(r => r.RefreshToken).NotEmpty();
	}
}
