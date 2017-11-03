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
    public class PersonValidations : AbstractValidator<PersonViewModel>
    {
        /// <summary>
        /// Default Constrator.
        /// </summary>
        public PersonValidations()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("The First Name cannot be blank.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("The Last Name cannot be blank.");
            RuleFor(x => x.Identifiers).NotEmpty().WithName("Con not be set null object.");
        }
    }
}
