using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core2APIExercise.Data.Entities
{

    /// <summary>
    /// Person Fluent Validator Class
    /// </summary>
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
        }
    }

}
