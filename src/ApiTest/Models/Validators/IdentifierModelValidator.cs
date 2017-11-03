using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Models
{
    public class IdentifierModelValidator : AbstractValidator<IdentifierModel>
    {
        public IdentifierModelValidator()
        {
            RuleFor(reg => reg.Type).IsInEnum();
        }
    }
}