using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace Core2APIExercise.Data.Entities
{
    /// <summary>
    /// Identifier Fluent Validator Class
    /// </summary>
    public class IdentifiersValidator : AbstractValidator<Identifier>
    {
        public IdentifiersValidator()
        {
            RuleFor(x => x.IdentifierType).NotEmpty().WithMessage("Type is required");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value is required");
        }
    }
}
