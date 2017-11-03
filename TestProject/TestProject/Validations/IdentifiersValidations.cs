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
    public class IdentifiersValidations : AbstractValidator<IdentifiersViewModel>
    {
        /// <summary>
        /// Default Constrator.
        /// </summary>
        public IdentifiersValidations()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value cannot be blank.");
        }
    }
}
