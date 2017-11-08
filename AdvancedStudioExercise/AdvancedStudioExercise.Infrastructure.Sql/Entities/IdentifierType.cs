using System.Collections.Generic;

namespace AdvancedStudioExercise.Infrastructure.Sql.Entities {
    public class IdentifierType {
        public int Id { get; set; }
        public string Label { get; set; }

        public ICollection<Identifier> Identifiers { get; set; }
    }
}