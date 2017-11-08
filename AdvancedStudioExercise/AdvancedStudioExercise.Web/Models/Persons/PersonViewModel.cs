using System;
using System.Collections.Generic;
using AdvancedStudioExercise.Web.Models.Identifiers;

namespace AdvancedStudioExercise.Web.Models.Persons {
    public class BasePersonViewModel {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AddPersonViewModel : BasePersonViewModel {
        public IEnumerable<AddPersonIdentifierViewModel> Identifiers { get; set; }
    }

    public class PersonViewModel : BasePersonViewModel {
        public IEnumerable<IdentifierViewModel> Identifiers { get; set; }
        public Guid Id { get; set; }
    }
}