using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Models
{
    public class PersonModelValidator : AbstractValidator<PersonModel>
    {
        public PersonModelValidator()
        {
            RuleFor(reg => reg.FirstName).NotEmpty();
            RuleFor(reg => reg.LastName).NotEmpty();
        }
    }
}