using System;
using AdvancedStudioExercise.Infrastructure.Sql.Entities.Base;

namespace AdvancedStudioExercise.Infrastructure.Sql.Entities {
    public class Identifier : EntityBase {
        public string Value { get; set; }

        public int IdentifierTypeId { get; set; }
        public IdentifierType IdentifierType { get; set; }

        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}