using System;
using AdvancedStudioExercise.Domain.Enums;
using AdvancedStudioExercise.Domain.Models.Base;

namespace AdvancedStudioExercise.Domain.Models {
    public class IdentifierModel : BaseModel {
        public string Value { get; set; }

        public IdentifierType IdentifierTypeId { get; set; }
        public IdentifierTypeModel IdentifierType { get; set; }

        public Guid PersonId { get; set; }
    }
}