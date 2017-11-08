using System.Collections.Generic;
using AdvancedStudioExercise.Domain.Models.Base;

namespace AdvancedStudioExercise.Domain.Models {
    public class PersonModel : BaseModel {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<IdentifierModel> Identifiers { get; set; }
    }
}