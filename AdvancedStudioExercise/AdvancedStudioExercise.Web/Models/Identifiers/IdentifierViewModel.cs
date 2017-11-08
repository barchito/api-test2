using System;
using AdvancedStudioExercise.Domain.Enums;

namespace AdvancedStudioExercise.Web.Models.Identifiers {

    public class AddPersonIdentifierViewModel {
        public string Value { get; set; }
        public IdentifierType Type { get; set; }
    }

    public class AddIdentifierViewModel : AddPersonIdentifierViewModel {
        public Guid PersonId { get; set; }
    }

    public class IdentifierViewModel : AddPersonIdentifierViewModel {
        public Guid Id { get; set; }
    }
}