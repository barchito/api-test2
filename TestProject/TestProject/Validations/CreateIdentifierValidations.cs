using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Entites;
using TestProject.ViewModel;

namespace TestProject.Validations
{
    /// <summary>
    /// FluentValidation for Person
    /// </summary>
    public class CreateIdentifierValidations : AbstractValidator<CreateIdentifierViewModel>
    {
        /// <summary>
        /// Default Constrator.
        /// </summary>
        public CreateIdentifierValidations()
        {
            RuleFor(x => x.PersonId).NotEmpty().WithMessage("Person Id cannot be blank."); 
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value cannot be blank.");
        }
    }
}
